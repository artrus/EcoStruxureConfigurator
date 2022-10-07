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

        /* public void CreateMBIO(string filename, List<TagMB> tagsMB)
         {
             ObjectSet xml = new ObjectSet();
             CreateHeader(xml);
             List<OI> exportedObjects = new List<OI>();
             xml.ExportedObjects = exportedObjects;
             exportedObjects.Add(new OI("IO", "modbus.folder.SlaveFolder"));
             List<OI> foldersInIO = new List<OI>();
             foldersInIO.Add(new OI("Binary", "modbus.folder.SlaveFolder"));
             foldersInIO.Add(new OI("Analog", "modbus.folder.SlaveFolder"));
             exportedObjects[0].OIList = foldersInIO;

             List<OI> regsIOBinary = new List<OI>();
             List<OI> regsIOAnalog = new List<OI>();
             foldersInIO[0].OIList = regsIOBinary;
             foldersInIO[1].OIList = regsIOAnalog;

             {
                 PI ContentType = new PI("ContentType");
                 Reference reference = new Reference("~/System/Content Types/Modbus Regs", "0", "1", "0", "10");
                 ContentType.reference = reference;
                 foldersInIO[0].PIList = new List<PI>();
                 foldersInIO[1].PIList = new List<PI>();
                 foldersInIO[0].PIList.Add(ContentType);
                 foldersInIO[1].PIList.Add(ContentType);
             }
             foreach (TagMB tagMB in tagsMB)
             {
                 if (tagMB.RegisterType == TagMB.REGTYPE.DEFAULT)
                 {
                     if (tagMB.IsReserve)
                     {
                         regsIOBinary.Add(new OI(tagMB.Name, tagMB.Type, tagMB.Descr));
                         PI registerNumber = new PI("RegisterNumber", Convert.ToString(tagMB.RegisterNumber));
                         List<PI> PIlist = new List<PI>();
                         PIlist.Add(registerNumber);
                         regsIOBinary[regsIOBinary.Count - 1].PIList = PIlist;
                     }
                     else
                     {
                         regsIOBinary.Add(new OI(tagMB.Name, tagMB.Type, tagMB.Descr));
                         PI registerNumber = new PI("RegisterNumber", Convert.ToString(tagMB.RegisterNumber));
                         PI value = new PI("Value");
                         string obj = "../../../../../IO Bus/" + tagMB.Source + "/" + tagMB.Name;
                         string property;
                         if (tagMB.Dir == TagBase.DIR.In)
                         {
                             property = "Value";
                         }
                         else if (tagMB.Dir == TagBase.DIR.Out)
                         {
                             property = "RequestedValue";
                         }
                         else
                         {
                             property = "Value";
                         }
                         Reference reference = new Reference(obj, "0", locked: null, "0", "10", property);
                         value.reference = reference;
                         List<PI> PIlist = new List<PI>();
                         PIlist.Add(registerNumber);
                         PIlist.Add(value);
                         regsIOBinary[regsIOBinary.Count - 1].PIList = PIlist;
                     }
                 }
                 else
                 {
                     if (tagMB.IsReserve)
                     {
                         regsIOAnalog.Add(new OI(tagMB.Name, tagMB.Type, tagMB.Descr));
                         PI registerNumber = new PI("RegisterNumber", Convert.ToString(tagMB.RegisterNumber));
                         List<PI> PIlist = new List<PI>();
                         PIlist.Add(registerNumber);
                         regsIOAnalog[regsIOAnalog.Count - 1].PIList = PIlist;
                     }
                     else
                     {
                         regsIOAnalog.Add(new OI(tagMB.Name, tagMB.Type, tagMB.Descr));
                         PI registerNumber = new PI("RegisterNumber", Convert.ToString(tagMB.RegisterNumber));
                         PI value = new PI("Value");
                         string obj = "../../../../../IO Bus/" + tagMB.Source + "/" + tagMB.Name;
                         string property;
                         if (tagMB.Dir == TagBase.DIR.In)
                         {
                             property = "Value";
                         }
                         else if (tagMB.Dir == TagBase.DIR.Out)
                         {
                             property = "RequestedValue";
                         }
                         else
                         {
                             property = "Value";
                         }
                         Reference reference = new Reference(obj, "0", locked: null, "0", "10", property);
                         value.reference = reference;
                         List<PI> PIlist = new List<PI>();
                         PIlist.Add(registerNumber);
                         PIlist.Add(value);
                         regsIOAnalog[regsIOAnalog.Count - 1].PIList = PIlist;
                     }
                 }
             }
             FinishXML(xml, filename);
         }

         public void CreateConvertors(string filename, TagList tags)
         {
             ObjectSet xml = new ObjectSet();
             CreateHeader(xml);
             List<OI> exportedObjects = new List<OI>();
             xml.ExportedObjects = exportedObjects;
             exportedObjects.Add(new OI("Convertors", "system.base.Folder"));
             List<OI> foldersInConvertors = new List<OI>();
             exportedObjects[0].OIList = foldersInConvertors;
             int cnt = 0;
             foreach (TagIO tag in tags.TagsIO)
             {
                 if (tag.Descr.Contains("PT1000") || tag.Descr.Contains("pt1000") || tag.Descr.Contains("Pt1000"))
                 {
                     foldersInConvertors.Add(new OI("PT1000 " + tag.Name, "system.base.Folder"));

                     List<OI> foldersInOut = new List<OI>();
                     foldersInOut.Add(new OI("InOut", "system.base.Folder"));

                     foldersInConvertors[foldersInConvertors.Count - 1].OIList = foldersInOut; ;
                     List<OI> InOut = new List<OI>();
                     foldersInOut[foldersInOut.Count - 1].OIList = InOut;
                     InOut.Add(new OI("In", "server.point.AV", "Вход"));
                     Reference reference = new Reference("../../../../IO Bus/" + tag.Module.Name + "/" + tag.Name, deltaFilter: "0", locked: null, retransmit: "0", transferRate: "10", property: "Value");
                     PI pi = new PI("Value");
                     pi.reference = reference;
                     List<PI> piList = new List<PI>();
                     piList.Add(pi);
                     InOut[InOut.Count - 1].PIList = piList;
                     InOut.Add(new OI("Out", "server.point.AV", "Выход"));
                     cnt++;
                 }
             }

             FinishXML(xml, filename);

         }
 */


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
    }
}
