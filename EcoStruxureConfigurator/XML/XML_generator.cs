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
        private readonly Settings Settings;

        public XML_generator(Settings settings)
        {
            Settings = settings;
        }

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
                    PI ChannelNumber = new PI(tag.TagInfo.XML_Direct, Convert.ToString(tag.Channel));
                    IOPoints[IOPoints.Count - 1].PIList = new List<PI>();
                    IOPoints[IOPoints.Count - 1].PIList.Add(ChannelNumber);
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

        public void CreateModbusIO(string filename, List<TagModbus> tags)
        {
            ObjectSet xml = new ObjectSet();
            CreateHeader(xml);
            List<OI> exportedObjects = new List<OI>();
            xml.ExportedObjects = exportedObjects;

            //create root "exportedObjects"
            addModbusFolder(exportedObjects, "IO");
            var insideFolderIO = new List<OI>();
            exportedObjects[0].OIList = insideFolderIO;

            var tagsBinary = tags.FindAll(x => x.TagInfo.Type == TagInfoBase.BinaryAnalog.Binary);
            addFolderWithTag(insideFolderIO, "Binary", tagsBinary);

            var tagsAnalog = tags.FindAll(x => x.TagInfo.Type == TagInfoBase.BinaryAnalog.Analog);
            addFolderWithTag(insideFolderIO, "Analog", tagsAnalog);

            FinishXML(xml, filename);
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

        private void addFolderWithTag(List<OI> root, string folderName, List<TagModbus> tags)
        {
            addModbusFolder(root, folderName, "Modbus Regs");

            var inside = new List<OI>();
            root[root.Count - 1].OIList = inside;

            foreach (var tag in tags)
            {                
                addModbusTag(inside, tag);
            }
        }

        private void addModbusFolder(List<OI> root, string name, string contentName = null)
        {
            root.Add(new OI(name, "modbus.folder.SlaveFolder"));
            if (contentName != null)
            {
                root[root.Count - 1].PIList = new List<PI>();
                root[root.Count - 1].PIList.Add(addContentType("Modbus Regs"));
            }
        }

        private PI addContentType(string type)
        {
            PI ContentType = new PI("ContentType");
            Reference reference = new Reference("~/System/Content Types/" + type, "0", "1", "0", "10");
            ContentType.reference = reference;
            return ContentType;
        }

        private PI addReferenceIO(string moduleName, string tagName)
        {
            PI Ref = new PI("Value");
            string path = "~/IO Bus/" + moduleName + "/" + tagName;
            Reference reference = new Reference(path, "0", "1", "0", "10", "Value");
            Ref.reference = reference;
            return Ref;
        }

        private void addModbusTag(List<OI> root, TagModbus tag)
        {
            root.Add(new OI(tag.Name, tag.TagInfo.XML_Type));
            List<PI> PIlist = new List<PI>();
            PI registerNumber = new PI("RegisterNumber", tag.Addr.ToString());
            PIlist.Add(registerNumber);

            //PIlist.Add(addReference("../../../../../IO Bus/A5/Контроль пускателя вентилятора ВТ-1.6С"));
            //PIlist.Add(addReferenceIO());

            if (tag.TagInfo.XML_RegisterType != TagInfoModbus.RegType.DEFAULT)
            {
                PI registerType = new PI("RegisterType", ((int)tag.TagInfo.XML_RegisterType).ToString());
                PIlist.Add(registerType);
            }

            root[root.Count - 1].PIList = PIlist;
        }
    }
}
