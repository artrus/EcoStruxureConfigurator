using EcoStruxureConfigurator.Object;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static EcoStruxureConfigurator.XML.XML_classes;

namespace EcoStruxureConfigurator.XML
{
    public class XML_generator
    {

        public void CreateIO(string filename, List<TagIO> tags, List<Module> modules)
        {
            ObjectSet xml = new ObjectSet();
            CreateHeader(xml);
            List<OI> exportedObjects = new List<OI>();
            xml.ExportedObjects = exportedObjects;

            foreach (var module in modules)
            {
                var moduleTags = tags.FindAll(x => x.Module.ID == module.ID);   //get list tags by module ID

                exportedObjects.Add(new OI(module.Name, module.ModuleInfo.XMLType));
                List<OI> IOPoints = new List<OI>();
                foreach (var tag in moduleTags)
                {
                    IOPoints.Add(new OI(tag.Name, tag.TagInfo.XML_Type, tag.Description));
                    IOPoints[IOPoints.Count - 1].PIList = new List<PI>();
                    PI ChannelNumber = new PI(tag.TagInfo.XML_Direct, Convert.ToString(tag.Channel));
                    IOPoints[IOPoints.Count - 1].PIList.Add(ChannelNumber);
                    if (tag.Module.Type.Contains("AO") && tag.TagInfo.Dir == TagInfoIO.Direct.Output)
                        AddResolution(IOPoints);    //add engineir convert
                    else if (tag.Module.Type.Contains("UI") && tag.TagInfo.XML_Type.Contains("TemperatureInput"))
                        AddSensor(IOPoints);
                }
                exportedObjects[exportedObjects.Count - 1].OIList = IOPoints;
                exportedObjects[exportedObjects.Count - 1].PIList = new List<PI>();
                PI ModuleID = new PI("ModuleID", Convert.ToString(module.ID));
                exportedObjects[exportedObjects.Count - 1].PIList.Add(ModuleID);

                //add ContentType
                PI ContentType = new PI("ContentType");
                Reference reference = new Reference("~/System/Content Types/IO " + module.Type, "0", "1", "0", "10");
                ContentType.reference = reference;
                exportedObjects[exportedObjects.Count - 1].PIList.Add(ContentType);
            }
            FinishXML(xml, filename);
        }

        private void AddResolution(List<OI> IOPoints)
        {
            PI ElecTopOfScale = new PI("ElecTopOfScale", "10");
            PI EngTopOfScale = new PI("EngTopOfScale", "100");
            PI Resolution = new PI("Resolution", "0.1");
            IOPoints[IOPoints.Count - 1].PIList.Add(ElecTopOfScale);
            IOPoints[IOPoints.Count - 1].PIList.Add(EngTopOfScale);
            IOPoints[IOPoints.Count - 1].PIList.Add(Resolution);
        }

        private void AddSensor(List<OI> IOPoints)
        {
            PI LowerReliabilityLevel = new PI("LowerReliabilityLevel", "-50");
            PI ThermistorType = new PI("ThermistorType", "2");
            PI UpperReliabilityLevel = new PI("UpperReliabilityLevel", "100");
            IOPoints[IOPoints.Count - 1].PIList.Add(LowerReliabilityLevel);
            IOPoints[IOPoints.Count - 1].PIList.Add(ThermistorType);
            IOPoints[IOPoints.Count - 1].PIList.Add(UpperReliabilityLevel);
        }

        public void CreateModbusIO(string filename, List<TagModbus> tags)
        {
            ObjectSet xml = new ObjectSet();
            CreateHeader(xml);
            List<OI> exportedObjects = new List<OI>();
            xml.ExportedObjects = exportedObjects;

            //create root "exportedObjects"
            AddModbusFolder(exportedObjects, "IO");
            var insideFolderIO = new List<OI>();
            exportedObjects[0].OIList = insideFolderIO;

            var tagsBinary = tags.FindAll(x => x.TagInfo.Type == TagInfoBase.BinaryAnalog.Binary);
            AddFolderWithTag(insideFolderIO, "Binary", tagsBinary);

            var tagsAnalog = tags.FindAll(x => x.TagInfo.Type == TagInfoBase.BinaryAnalog.Analog);
            AddFolderWithTag(insideFolderIO, "Analog", tagsAnalog);

            FinishXML(xml, filename);
        }

        public void CreateModbusObjects(string filename, List<TagModbus> tags)
        {
            ObjectSet xml = new ObjectSet();
            CreateHeader(xml);
            List<OI> exportedObjects = new List<OI>();
            xml.ExportedObjects = exportedObjects;

            var systems = GetUniqPath(tags, 0);

            foreach (var system in systems)
            {
                AddModbusFolder(exportedObjects, system);
                List<OI> insideFolderSystem = new List<OI>();
                exportedObjects[exportedObjects.Count - 1].OIList = insideFolderSystem;

                var objects = GetUniqPath(tags.FindAll(x => x.Path[0] == system), 1);
                foreach (var obj in objects)
                {
                    AddModbusFolder(insideFolderSystem, obj);
                    var insideFolderObject = new List<OI>();
                    insideFolderSystem[insideFolderSystem.Count - 1].OIList = insideFolderObject;

                    var SP = tags.FindAll(x => x.Path[0].Equals(system) && x.Path[1].Equals(obj) && x.Path[2].Equals("SP"));
                    AddFolderWithTag(insideFolderObject, "SP", SP);

                    var ST = tags.FindAll(x => x.Path[0].Equals(system) && x.Path[1].Equals(obj) && x.Path[2].Equals("ST"));
                    AddFolderWithTag(insideFolderObject, "ST", ST);
                }
            }
            FinishXML(xml, filename);
        }

        private List<string> GetUniqPath(List<TagModbus> tags, int level)
        {
            List<string> systems = new List<string>();
            foreach (TagModbus tag in tags)
            {
                if (!systems.Exists(x => x.Equals(tag.Path[level])))
                    systems.Add(tag.Path[level]);
            }
            return systems;
        }

        private void CreateHeader(ObjectSet xml)
        {
            XML_MetaInformation meta = new XML_MetaInformation();
            XML_TagValueString ExportModeVal = new XML_TagValueString();
            XML_TagValueString RuntimeVersionVal = new XML_TagValueString();
            XML_TagValueString SourceVersionVal = new XML_TagValueString();
            XML_TagValueString ServerFullPathVal = new XML_TagValueString();
            xml.ExportMode = "Special";
            xml.Note = "TypesFirst";
            xml.Version = "3.2.3.59";
            ExportModeVal.Value = "Special";
            RuntimeVersionVal.Value = "3.2.3.59";
            SourceVersionVal.Value = "3.2.3.59";
            ServerFullPathVal.Value = "/SHAUOV1";
            xml.MetaInformation = meta;
            meta.ExportMode = ExportModeVal;
            meta.RuntimeVersion = RuntimeVersionVal;
            meta.SourceVersion = SourceVersionVal;
            meta.ServerFullPath = ServerFullPathVal;
        }

        private void FinishXML(ObjectSet xml, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObjectSet));
            TextWriter writer = new StreamWriter(filename);
            serializer.Serialize(writer, xml);
            writer.Close();
            StreamReader reader = new StreamReader(filename);
            string xml_file = reader.ReadToEnd();
            reader.Close();
            xml_file = xml_file.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" ", "");
            //xml_file = xml_file.Replace("OI1_", "OI");        
            //xml_file = xml_file.Replace("</OI1_ >\r\n", "");    
            TextWriter wr = new StreamWriter(filename);
            wr.Write(xml_file);
            wr.Close();
        }

        private void AddFolderWithTag(List<OI> root, string folderName, List<TagModbus> tags)
        {
            AddModbusFolder(root, folderName, "Modbus Regs");

            var inside = new List<OI>();
            root[root.Count - 1].OIList = inside;

            foreach (var tag in tags)
            {
                AddModbusTag(inside, tag);
            }
        }

        private void AddModbusFolder(List<OI> root, string name, string contentName = null)
        {
            root.Add(new OI(name, "modbus.folder.SlaveFolder"));
            if (contentName != null)
            {
                root[root.Count - 1].PIList = new List<PI>
                {
                    AddContentType("Modbus Regs")
                };
            }
        }

        private PI AddContentType(string type)
        {
            PI ContentType = new PI("ContentType");
            Reference reference = new Reference("~/System/Content Types/" + type, "0", "1", "0", "10");
            ContentType.reference = reference;
            return ContentType;
        }

        private PI AddReference(string path)
        {
            PI Ref = new PI("Value");
            Reference reference = new Reference(path, "0", "1", "0", "10", "Value");
            Ref.reference = reference;
            return Ref;
        }

        private void AddModbusTag(List<OI> root, TagModbus tag)
        {
            root.Add(new OI(tag.Name, tag.TagInfo.XML_Type, tag.Description));
            List<PI> PIlist = new List<PI>();
            PI registerNumber = new PI("RegisterNumber", tag.Addr.ToString());
            PIlist.Add(registerNumber);

            if (tag.TagReference != null)
                PIlist.Add(AddReference(tag.TagReference.Path));

            if (tag.TagInfo.XML_RegisterType != TagInfoModbus.RegType.DEFAULT)
            {
                PI registerType = new PI("RegisterType", ((int)tag.TagInfo.XML_RegisterType).ToString());
                PIlist.Add(registerType);
            }

            string name = tag.Name;
            int startIndex = name.IndexOf('[');
            int endIndex = name.IndexOf(']');
            if (!(startIndex == -1 || endIndex == -1))
            {
                int lenght = endIndex - startIndex;
                string g = name.Substring(startIndex + 1, lenght - 1);
                AddGain(PIlist, g);
            }
            /*if ()
                addGain();
*/
            root[root.Count - 1].PIList = PIlist;
        }

        private void AddGain(List<PI> pIlist, string k)
        {
            double v = Double.Parse(k);
            string gain = v.ToString().Replace(",", ".");
            PI Gain = new PI("Gain", gain);
            pIlist.Add(Gain);
        }

    }
}
