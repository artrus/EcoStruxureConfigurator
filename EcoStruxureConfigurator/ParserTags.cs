using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator
{
    public class ParserTags
    {
        public static List<Module> GetModules(List<TagIO> tags)
        {
            List<Module> modules = new List<Module>();
            if (tags == null || tags.Count == 0)
                throw new Exception("Функция GetModules(List<TagIO> принимает либо null, либо Count = 0");

            foreach (var tag in tags)
            {
                if (!modules.Exists(x => x.ID == tag.Module.ID))
                {
                    modules.Add(tag.Module);
                }
            }
            return modules;
        }

        public static List<TagModbus> GetTagsIOModbus(List<TagIO> tagsIO, Settings settings)
        {
            if (tagsIO == null || tagsIO.Count == 0)
                throw new Exception("Функция GetTagsIOModbus(List<TagIO> принимает либо null, либо Count = 0");

            List<TagModbus> tagsBinary = GetTagsBinaryIOModbus(tagsIO, settings);
            List<TagModbus> tagsAnalog = GetTagsAnalogIOModbus(tagsIO, settings);

            List<TagModbus> tags = new List<TagModbus>();
            tags.AddRange(tagsBinary);
            tags.AddRange(tagsAnalog);

            return tags;
        }

        public static List<TagModbus> GetTagsBinaryIOModbus(List<TagIO> tagsIO, Settings settings)
        {
            var modules = GetModules(tagsIO);
            var tagsBinary = tagsIO.FindAll(x => x.TagInfo.Type == TagInfoBase.BinaryAnalog.Binary);

            if (tagsBinary == null || tagsBinary.Count == 0 || modules.Count == 0)
                return new List<TagModbus>();

            var tagsModbus = new List<TagModbus>();
            int addrReg = settings.MODBUS_ADDR_BLOCK_IO;
            foreach (var module in modules)
            {
                if ((module.ModuleInfo.TypeInputs == ModuleInfo.TypeChannels.BOOL) || (module.ModuleInfo.TypeInputs == ModuleInfo.TypeChannels.ALL))
                {
                    for (int i = 1; i <= module.ModuleInfo.CntInputs; i++)
                    {
                        if (tagsBinary.Exists(x => (x.Module.ID == module.ID) && (x.Channel == i) && (x.TagInfo.Dir == TagInfoIO.Direct.Input)))
                        {
                            TagIO tag = tagsBinary.Find(x => (x.Module.ID == module.ID) && (x.Channel == i));
                            tagsModbus.Add(new TagModbus(tag.Name, tag.Description, tag.System, addrReg, settings.GetTagModbusInfoByTypeName("BOOL")));
                        }
                        else
                        {
                            string name = "Резерв вход " + module.Type + " " + module.Name + ":" + i;
                            tagsModbus.Add(new TagModbus(name, "-", "Резервы", addrReg, settings.GetTagModbusInfoByTypeName("BOOL")));
                        }
                        addrReg += settings.GetTagModbusInfoByTypeName("BOOL").Size;
                    }
                }

                if ((module.ModuleInfo.TypeOutputs == ModuleInfo.TypeChannels.BOOL) || (module.ModuleInfo.TypeOutputs == ModuleInfo.TypeChannels.ALL))
                {
                    for (int i = 1; i <= module.ModuleInfo.CntOutputs; i++)
                    {
                        if (tagsBinary.Exists(x => (x.Module.ID == module.ID) && (x.Channel == i) && (x.TagInfo.Dir == TagInfoIO.Direct.Output)))
                        {
                            TagIO tag = tagsBinary.Find(x => (x.Module.ID == module.ID) && (x.Channel == i));
                            tagsModbus.Add(new TagModbus(tag.Name, tag.Description, tag.System, addrReg, settings.GetTagModbusInfoByTypeName("BOOL")));
                        }
                        else
                        {
                            string name = "Резерв выход " + module.Type + " " + module.Name + ":" + i;
                            tagsModbus.Add(new TagModbus(name, "-", "Резервы", addrReg, settings.GetTagModbusInfoByTypeName("BOOL")));
                        }
                        addrReg += settings.GetTagModbusInfoByTypeName("BOOL").Size;
                    }
                }
            }
            return tagsModbus;
        }

        public static List<TagModbus> GetTagsAnalogIOModbus(List<TagIO> tagsIO, Settings settings)
        {
            var modules = GetModules(tagsIO);
            var tagsAnalog = tagsIO.FindAll(x => x.TagInfo.Type == TagInfoBase.BinaryAnalog.Analog);

            if (tagsAnalog == null || tagsAnalog.Count == 0 || modules.Count == 0)
                return new List<TagModbus>();

            var tagsModbus = new List<TagModbus>();
            int addrReg = settings.MODBUS_ADDR_BLOCK_IO;
            foreach (var module in modules)
            {
                if ((module.ModuleInfo.TypeInputs == ModuleInfo.TypeChannels.BOOL) || (module.ModuleInfo.TypeInputs == ModuleInfo.TypeChannels.ALL))
                {
                    for (int i = 1; i <= module.ModuleInfo.CntInputs; i++)
                    {
                        if (tagsAnalog.Exists(x => (x.Module.ID == module.ID) && (x.Channel == i) && (x.TagInfo.Dir == TagInfoIO.Direct.Input)))
                        {
                            TagIO tag = tagsAnalog.Find(x => (x.Module.ID == module.ID) && (x.Channel == i));
                            tagsModbus.Add(new TagModbus(tag.Name, tag.Description, tag.System, addrReg, settings.GetTagModbusInfoByTypeName("REAL")));
                        }
                        else
                        {
                            string name = "Резерв вход " + module.Type + " " + module.Name + ":" + i;
                            tagsModbus.Add(new TagModbus(name, "-", "Резервы", addrReg, settings.GetTagModbusInfoByTypeName("REAL")));
                        }
                        addrReg += settings.GetTagModbusInfoByTypeName("REAL").Size;
                    }
                }

                if ((module.ModuleInfo.TypeOutputs == ModuleInfo.TypeChannels.REAL) || (module.ModuleInfo.TypeOutputs == ModuleInfo.TypeChannels.ALL))
                {
                    for (int i = 1; i <= module.ModuleInfo.CntOutputs; i++)
                    {
                        if (tagsAnalog.Exists(x => (x.Module.ID == module.ID) && (x.Channel == i) && (x.TagInfo.Dir == TagInfoIO.Direct.Output)))
                        {
                            TagIO tag = tagsAnalog.Find(x => (x.Module.ID == module.ID) && (x.Channel == i));
                            tagsModbus.Add(new TagModbus(tag.Name, tag.Description, tag.System, addrReg, settings.GetTagModbusInfoByTypeName("REAL")));
                        }
                        else
                        {
                            string name = "Резерв выход " + module.Type + " " + module.Name + ":" + i;
                            tagsModbus.Add(new TagModbus(name, "-", "Резервы", addrReg, settings.GetTagModbusInfoByTypeName("REAL")));
                        }
                        addrReg += settings.GetTagModbusInfoByTypeName("REAL").Size;
                    }
                }
            }
            return tagsModbus;
        }
    }
}
