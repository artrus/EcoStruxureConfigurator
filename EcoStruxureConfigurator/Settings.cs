using EcoStruxureConfigurator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoStruxureConfigurator
{
    public class Settings
    {
        public int ROW_MODULE_NAME;
        public int ROW_MODULE_TYPE;
        public int ROW_MODULE_ID;

        public int ROW_IO_ID;
        public int ROW_IO_NAME_MODULE;
        public int ROW_IO_TYPE_MODULE;
        public int ROW_IO_CHANNEL;
        public int ROW_IO_TYPE_IO;
        public int ROW_IO_NAME;
        public int ROW_IO_DESCR;
        public int ROW_IO_SYSTEM;

        public int ROW_MODBUS_TYPE;
        public int ROW_MODBUS_NAME;
        public int ROW_MODBUS_DESCR;
        public int ROW_MODBUS_SYSTEM;
        public int ROW_MODBUS_ADDR;

        public int ROW_OBJECT_IO_INPUT;
        public int ROW_OBJECT_CONTROL_IN;
        public int ROW_OBJECT_SP;
        public int ROW_OBJECT_ST;
        public int ROW_OBJECT_CONTROL_OUT;
        public int ROW_OBJECT_IO_OUTPUT;
        public int ROW_OBJECT_DESCR_IN;
        public int ROW_OBJECT_DESCR_OUT;
        public int ROW_OBJECT_TYPE_IN;
        public int ROW_OBJECT_TYPE_OUT;

        public string NAME_LIST_IO;
        public string NAME_LIST_MODULES;
        public string NAME_LIST_MODBUS_BINARY;
        public string NAME_LIST_MODBUS_ANALOG;
        public string NAME_LIST_OBJECTS;

        public int MODBUS_ADDR_BLOCK_IO;
        public int MODBUS_ADDR_BLOCK_ST;
        public int MODBUS_ADDR_BLOCK_SP;

        public bool MODBUS_ENABLE_LIGHT_REZERV;
        public bool CREATE_NEW_EXCEL_FILE;

        public string SEPPrefix;

        private IniFile iniFile;
        private const string NAME_SECTION_ROWS_IO = "ROWS_IO";
        private const string NAME_SECTION_ROWS_OBJECTS = "ROWS_OBJECTS";
        private const string NAME_SECTION_ROWS_MODBUS = "ROWS_MODBUS";
        private const string NAME_SECTION_GENERATOR = "GEN";
        
        private Dictionary<string, ModuleInfo> DictionaryTypesModules { get; set; }
        private Dictionary<string, TagInfoIO> DictionaryTypesIO { get; set; }
        private Dictionary<string, TagInfoModbus> DictionaryTypesTagsModbus { get; set; }
        private Dictionary<string, ObjectBase> DictionaryObjects { get; set; }


        public void ReadSetting(string path)
        {
            OpenINI(path);
            DictionaryTypesModules = SetDefaultTypesModules();
            DictionaryTypesIO = SetDefaultTypesIO();
            DictionaryTypesTagsModbus = SetDefaultTypesTagsModbus();
        }

        public ModuleInfo GetModuleInfoByType(string type)
        {
            return DictionaryTypesModules[type];
        }

        public TagInfoIO GetTagIOInfoByType(string type)
        {
            return DictionaryTypesIO[type];
        }

        public TagInfoModbus GetTagModbusInfoByTypeName(string type)
        {
            return DictionaryTypesTagsModbus[type];
        }

        public ObjectBase GetObjectByName(string name)
        {
            return DictionaryObjects[name];
        }

        public void AddObjects(List<ObjectBase> objects)
        {
            if (DictionaryObjects == null)
                DictionaryObjects = new Dictionary<string, ObjectBase>();
            foreach (ObjectBase obj in objects)
                DictionaryObjects.Add(obj.Type, obj);
        }

        private void OpenINI(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("Создан новый файл конфигурации по умолчанию", "Не найден файл config.ini", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    File.Create(path).Close();
                    iniFile = new IniFile(path);
                    SetDefaultConfig();
                    SaveDefault();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    iniFile = new IniFile(path);
                    ReadParams(iniFile);
                }
                catch
                {
                    var result = MessageBox.Show("Ошибка в конфигурациии. Создать заново?", "Ошибка в файле config.ini", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.No)
                        throw new ArgumentException();
                    else
                    {
                        File.Delete(path);
                        OpenINI(path);
                    }
                }

            }
        }

        private void ReadParams(IniFile iniFile)
        {
            NAME_LIST_MODULES = iniFile.ReadINI(NAME_SECTION_ROWS_IO, "NAME_LIST_MODULES");
            NAME_LIST_IO = iniFile.ReadINI(NAME_SECTION_ROWS_IO, "NAME_LIST_IO");
            NAME_LIST_OBJECTS = iniFile.ReadINI(NAME_SECTION_ROWS_IO, "NAME_LIST_OBJECTS");
            ROW_MODULE_NAME = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_IO, "ROW_MODULE_NAME"));
            ROW_MODULE_TYPE = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_IO, "ROW_MODULE_TYPE"));
            ROW_MODULE_ID = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_IO, "ROW_MODULE_ID"));
            ROW_IO_ID = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_IO, "ROW_IO_ID"));
            ROW_IO_NAME_MODULE = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_IO, "ROW_IO_NAME_MODULE"));
            ROW_IO_TYPE_MODULE = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_IO, "ROW_IO_TYPE_MODULE"));
            ROW_IO_CHANNEL = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_IO, "ROW_IO_CHANNEL"));
            ROW_IO_TYPE_IO = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_IO, "ROW_IO_TYPE_IO"));
            ROW_IO_NAME = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_IO, "ROW_IO_NAME"));
            ROW_IO_DESCR = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_IO, "ROW_IO_DESCR"));
            ROW_IO_SYSTEM = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_IO, "ROW_IO_SYSTEM"));

            ROW_OBJECT_IO_INPUT = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_IO_INPUT"));
            ROW_OBJECT_CONTROL_IN = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_CONTROL_IN"));
            ROW_OBJECT_SP = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_CONTROL_SP"));
            ROW_OBJECT_ST = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_CONTROL_ST"));
            ROW_OBJECT_CONTROL_OUT = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_CONTROL_OUT"));
            ROW_OBJECT_IO_OUTPUT = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_IO_OUTPUT"));
            ROW_OBJECT_TYPE_IN = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_TYPE_IN"));
            ROW_OBJECT_DESCR_IN = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_DESCR_IN"));
            ROW_OBJECT_DESCR_OUT = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_DESCR_OUT"));
            ROW_OBJECT_TYPE_OUT = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_TYPE_OUT"));



            NAME_LIST_MODBUS_BINARY = iniFile.ReadINI(NAME_SECTION_ROWS_MODBUS, "NAME_LIST_MODBUS_BINARY");
            NAME_LIST_MODBUS_ANALOG = iniFile.ReadINI(NAME_SECTION_ROWS_MODBUS, "NAME_LIST_MODBUS_ANALOG");
            ROW_MODBUS_TYPE = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_MODBUS, "ROW_MODBUS_TYPE"));
            ROW_MODBUS_NAME = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_MODBUS, "ROW_MODBUS_NAME"));
            ROW_MODBUS_DESCR = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_MODBUS, "ROW_MODBUS_DESCR"));
            ROW_MODBUS_SYSTEM = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_MODBUS, "ROW_MODBUS_SYSTEM"));
            ROW_MODBUS_ADDR = Int32.Parse(iniFile.ReadINI(NAME_SECTION_ROWS_MODBUS, "ROW_MODBUS_ADDR"));

            MODBUS_ADDR_BLOCK_IO = Int32.Parse(iniFile.ReadINI(NAME_SECTION_GENERATOR, "MODBUS_ADDR_BLOCK_IO"));
            MODBUS_ADDR_BLOCK_ST = Int32.Parse(iniFile.ReadINI(NAME_SECTION_GENERATOR, "MODBUS_ADDR_BLOCK_ST"));
            MODBUS_ADDR_BLOCK_SP = Int32.Parse(iniFile.ReadINI(NAME_SECTION_GENERATOR, "MODBUS_ADDR_BLOCK_SP"));

            MODBUS_ENABLE_LIGHT_REZERV = Boolean.Parse(iniFile.ReadINI(NAME_SECTION_GENERATOR, "MODBUS_ENABLE_LIGHT_REZERV"));

            SEPPrefix = iniFile.ReadINI(NAME_SECTION_GENERATOR, "SEPPrefix");
        }

        public void SaveFile()
        {
            SaveDefault();
        }
        private void SaveDefault()
        {
            iniFile.Write(NAME_SECTION_ROWS_IO, "NAME_LIST_MODULES", NAME_LIST_MODULES);
            iniFile.Write(NAME_SECTION_ROWS_IO, "NAME_LIST_IO", NAME_LIST_IO);
            iniFile.Write(NAME_SECTION_ROWS_IO, "NAME_LIST_OBJECTS", NAME_LIST_OBJECTS);
            iniFile.Write(NAME_SECTION_ROWS_IO, "ROW_MODULE_NAME", ROW_MODULE_NAME.ToString());
            iniFile.Write(NAME_SECTION_ROWS_IO, "ROW_MODULE_TYPE", ROW_MODULE_TYPE.ToString());
            iniFile.Write(NAME_SECTION_ROWS_IO, "ROW_MODULE_ID", ROW_MODULE_ID.ToString());
            iniFile.Write(NAME_SECTION_ROWS_IO, "ROW_IO_ID", ROW_IO_ID.ToString());
            iniFile.Write(NAME_SECTION_ROWS_IO, "ROW_IO_NAME_MODULE", ROW_IO_NAME_MODULE.ToString());
            iniFile.Write(NAME_SECTION_ROWS_IO, "ROW_IO_TYPE_MODULE", ROW_IO_TYPE_MODULE.ToString());
            iniFile.Write(NAME_SECTION_ROWS_IO, "ROW_IO_CHANNEL", ROW_IO_CHANNEL.ToString());
            iniFile.Write(NAME_SECTION_ROWS_IO, "ROW_IO_TYPE_IO", ROW_IO_TYPE_IO.ToString());
            iniFile.Write(NAME_SECTION_ROWS_IO, "ROW_IO_NAME", ROW_IO_NAME.ToString());
            iniFile.Write(NAME_SECTION_ROWS_IO, "ROW_IO_DESCR", ROW_IO_DESCR.ToString());
            iniFile.Write(NAME_SECTION_ROWS_IO, "ROW_IO_SYSTEM", ROW_IO_SYSTEM.ToString());

            iniFile.Write(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_IO_INPUT", ROW_OBJECT_IO_INPUT.ToString());
            iniFile.Write(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_CONTROL_IN", ROW_OBJECT_CONTROL_IN.ToString());
            iniFile.Write(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_CONTROL_SP", ROW_OBJECT_SP.ToString());
            iniFile.Write(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_CONTROL_ST", ROW_OBJECT_ST.ToString());
            iniFile.Write(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_CONTROL_OUT", ROW_OBJECT_CONTROL_OUT.ToString());
            iniFile.Write(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_IO_OUTPUT", ROW_OBJECT_IO_OUTPUT.ToString());
            iniFile.Write(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_TYPE_IN", ROW_OBJECT_TYPE_IN.ToString());
            iniFile.Write(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_DESCR_IN", ROW_OBJECT_DESCR_IN.ToString());
            iniFile.Write(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_DESCR_OUT", ROW_OBJECT_DESCR_OUT.ToString());
            iniFile.Write(NAME_SECTION_ROWS_OBJECTS, "ROW_OBJECT_TYPE_OUT", ROW_OBJECT_TYPE_OUT.ToString());

            iniFile.Write(NAME_SECTION_ROWS_MODBUS, "NAME_LIST_MODBUS_BINARY", NAME_LIST_MODBUS_BINARY);
            iniFile.Write(NAME_SECTION_ROWS_MODBUS, "NAME_LIST_MODBUS_ANALOG", NAME_LIST_MODBUS_ANALOG);
            
            iniFile.Write(NAME_SECTION_ROWS_MODBUS, "ROW_MODBUS_TYPE", ROW_MODBUS_TYPE.ToString());
            iniFile.Write(NAME_SECTION_ROWS_MODBUS, "ROW_MODBUS_NAME", ROW_MODBUS_NAME.ToString());
            iniFile.Write(NAME_SECTION_ROWS_MODBUS, "ROW_MODBUS_DESCR", ROW_MODBUS_DESCR.ToString());
            iniFile.Write(NAME_SECTION_ROWS_MODBUS, "ROW_MODBUS_SYSTEM", ROW_MODBUS_SYSTEM.ToString());
            iniFile.Write(NAME_SECTION_ROWS_MODBUS, "ROW_MODBUS_ADDR", ROW_MODBUS_ADDR.ToString());

            iniFile.Write(NAME_SECTION_GENERATOR, "MODBUS_ADDR_BLOCK_IO", MODBUS_ADDR_BLOCK_IO.ToString());
            iniFile.Write(NAME_SECTION_GENERATOR, "MODBUS_ADDR_BLOCK_ST", MODBUS_ADDR_BLOCK_ST.ToString());
            iniFile.Write(NAME_SECTION_GENERATOR, "MODBUS_ADDR_BLOCK_SP", MODBUS_ADDR_BLOCK_SP.ToString());

            iniFile.Write(NAME_SECTION_GENERATOR, "MODBUS_ENABLE_LIGHT_REZERV", MODBUS_ENABLE_LIGHT_REZERV.ToString());
            iniFile.Write(NAME_SECTION_GENERATOR, "SEPPrefix", SEPPrefix);
        }

        private void SetDefaultConfig()
        {
            SetDefaultRows();
        }

        private Dictionary<string, ModuleInfo> SetDefaultTypesModules()
        {
            return new Dictionary<string, ModuleInfo>() { { "AO-8", new ModuleInfo("io.AO8", 0, 8, ModuleInfo.TypeChannels.NONE, ModuleInfo.TypeChannels.REAL)},
                                                          { "AO-V-8", new ModuleInfo("io.AOV8", 0, 8, ModuleInfo.TypeChannels.NONE, ModuleInfo.TypeChannels.REAL) },
                                                          { "DI-16", new ModuleInfo("io.DI16", 16, 0, ModuleInfo.TypeChannels.ALL, ModuleInfo.TypeChannels.NONE) },
                                                          { "DO-FA-12", new ModuleInfo("io.DOFA12", 0, 12, ModuleInfo.TypeChannels.NONE, ModuleInfo.TypeChannels.BOOL) },
                                                          { "DO-FC-8", new ModuleInfo("io.DOFC8", 0, 8, ModuleInfo.TypeChannels.NONE, ModuleInfo.TypeChannels.BOOL) },
                                                          { "RTD-DI-16", new ModuleInfo("io.RTDDI16", 16, 0, ModuleInfo.TypeChannels.ALL, ModuleInfo.TypeChannels.NONE) },
                                                          { "UI-8.AO-4", new ModuleInfo("io.UI8AO4", 8, 4, ModuleInfo.TypeChannels.ALL, ModuleInfo.TypeChannels.REAL) },
                                                          { "UI-8.AO-V-4", new ModuleInfo("io.UI8AO4", 8, 4, ModuleInfo.TypeChannels.ALL, ModuleInfo.TypeChannels.REAL) },
                                                          { "UI-8.DO-FC-4", new ModuleInfo("io.UI8DOFC4", 8, 4, ModuleInfo.TypeChannels.ALL, ModuleInfo.TypeChannels.BOOL) },
                                                          { "UI-16", new ModuleInfo("io.UI16", 16, 0, ModuleInfo.TypeChannels.ALL, ModuleInfo.TypeChannels.NONE) },
                                                          { "Ua", new ModuleInfo("io.UI16", 20, 0, ModuleInfo.TypeChannels.ALL, ModuleInfo.TypeChannels.NONE) },    //AS-B
                                                          { "Ub", new ModuleInfo("io.UI16", 18, 0, ModuleInfo.TypeChannels.ALL, ModuleInfo.TypeChannels.NONE) },    //AS-B
                                                          { "DO", new ModuleInfo("io.UI16", 8, 0, ModuleInfo.TypeChannels.ALL, ModuleInfo.TypeChannels.NONE) },    //AS-B
                                                    };
        }

        public void SetSEPPrefix(string text)
        {
            SEPPrefix = text;
        }

        private Dictionary<string, TagInfoIO> SetDefaultTypesIO()
        {
            return new Dictionary<string, TagInfoIO>() {    { "Вход цифровой", new TagInfoIO("BOOL", "io.point.DigitalInput", TagInfoBase.BinaryAnalog.Binary) },
                                                            { "Вход счётчик", new TagInfoIO("INT", "io.point.CounterInput", TagInfoBase.BinaryAnalog.Analog) },
                                                            { "Вход напряжения", new TagInfoIO("REAL", "io.point.VoltageInput", TagInfoBase.BinaryAnalog.Analog )},
                                                            { "Вход токовый", new TagInfoIO("REAL", "io.point.CurrentInput", TagInfoBase.BinaryAnalog.Analog) },
                                                            { "Вход контролируемый", new TagInfoIO("REAL", "io.point.SupervisedInput", TagInfoBase.BinaryAnalog.Analog) },
                                                            { "Вход резистивный", new TagInfoIO("REAL", "io.point.ResistiveInput", TagInfoBase.BinaryAnalog.Analog) },
                                                            { "Вход температурный", new TagInfoIO("REAL", "io.point.TemperatureInput", TagInfoBase.BinaryAnalog.Analog) },
                                                            { "Вход 2-проводной R", new TagInfoIO("REAL", "io.point.2WireRTDResistiveInput", TagInfoBase.BinaryAnalog.Analog) },
                                                            { "Вход 2-проводной TE", new TagInfoIO("REAL", "io.point.2WireRTDTemperatureInput", TagInfoBase.BinaryAnalog.Analog) },
                                                            { "Вход 3-проводной R", new TagInfoIO("REAL", "io.point.3WireRTDResistiveInput", TagInfoBase.BinaryAnalog.Analog) },
                                                            { "Вход 3-проводной TE", new TagInfoIO("REAL", "io.point.3WireRTDTemperatureInput", TagInfoBase.BinaryAnalog.Analog )},
                                                            { "Выход цифровой", new TagInfoIO("BOOL", "io.point.DigitalOutput", TagInfoBase.BinaryAnalog.Binary )},
                                                            { "Выход импульс", new TagInfoIO("INT", "io.point.DigitalPulsedOutput", TagInfoBase.BinaryAnalog.Analog )},
                                                            { "Выход напряжения", new TagInfoIO("REAL", "io.point.VoltageOutput", TagInfoBase.BinaryAnalog.Analog )},
                                                            { "Выход токовый", new TagInfoIO("REAL", "io.point.CurrentOutput", TagInfoBase.BinaryAnalog.Analog )},
                                                            { "Выход 3 состояния", new TagInfoIO("INT", "io.point.TristateOutput", TagInfoBase.BinaryAnalog.Analog )},
                                                            { "Выход импульс 3 состояний", new TagInfoIO("INT", "io.point.TristatePulsedOutput", TagInfoBase.BinaryAnalog.Analog )},
                                                    };
        }

        private Dictionary<string, TagInfoModbus> SetDefaultTypesTagsModbus()
        {
            return new Dictionary<string, TagInfoModbus>() {    { "BOOL", new TagInfoModbus("BOOL", 1,  "modbus.point.BinaryValue", TagInfoModbus.RegType.DEFAULT, TagInfoBase.BinaryAnalog.Binary) },
                                                                { "INT", new TagInfoModbus("INT", 1, "modbus.point.AnalogValue", TagInfoModbus.RegType.DEFAULT, TagInfoBase.BinaryAnalog.Analog) },
                                                                { "REAL", new TagInfoModbus("REAL", 2, "modbus.point.AnalogValue", TagInfoModbus.RegType.REAL, TagInfoBase.BinaryAnalog.Analog )}
                                                            };
        }

        private void SetDefaultRows()
        {
            NAME_LIST_MODULES = "Modules";
            NAME_LIST_IO = "IO";
            ROW_MODULE_NAME = 2;
            ROW_MODULE_TYPE = 3;
            ROW_MODULE_ID = 4;
            ROW_IO_ID = 1;
            ROW_IO_NAME_MODULE = 2;
            ROW_IO_TYPE_MODULE = 3;
            ROW_IO_CHANNEL = 4;
            ROW_IO_TYPE_IO = 5;
            ROW_IO_NAME = 7;
            ROW_IO_DESCR = 8;
            ROW_IO_SYSTEM = 9;

            ROW_OBJECT_IO_INPUT = 1;
            ROW_OBJECT_CONTROL_IN = 2;
            ROW_OBJECT_SP = 3;
            ROW_OBJECT_TYPE_IN = 4;
            ROW_OBJECT_DESCR_IN = 5;
            ROW_OBJECT_DESCR_OUT = 7;
            ROW_OBJECT_TYPE_OUT = 8;
            ROW_OBJECT_ST = 9;
            ROW_OBJECT_CONTROL_OUT = 10;
            ROW_OBJECT_IO_OUTPUT = 11;



            NAME_LIST_MODBUS_BINARY = "Modbus Binary";
            NAME_LIST_MODBUS_ANALOG = "Modbus Analog";
            NAME_LIST_OBJECTS = "Objects";
            ROW_MODBUS_TYPE = 1;
            ROW_MODBUS_NAME = 2;
            ROW_MODBUS_DESCR = 3;
            ROW_MODBUS_SYSTEM = 4;
            ROW_MODBUS_ADDR = 5;

            MODBUS_ENABLE_LIGHT_REZERV = true;
            CREATE_NEW_EXCEL_FILE = false;

            SEPPrefix = "Ventilation.";

            MODBUS_ADDR_BLOCK_IO = 0;
            MODBUS_ADDR_BLOCK_ST = 500;
            MODBUS_ADDR_BLOCK_SP = 2000;
        }

    }
}
