using EcoStruxureConfigurator.Tags;
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
        public enum TYPE_PARAM { ROWS }
        public Dictionary<string, ModuleInfo> DictionaryTypesModules { get; set; }
        public Dictionary<string, TagIOInfo> DictionaryTypesIO { get; set; }

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

        public string NAME_LIST_IO;
        public string NAME_LIST_MODULES;

        private IniFile iniFile;
        private const string NAMESECTIONROWS = "ROWS";


        public void ReadSetting(string path)
        {
            OpenINI(path);
            DictionaryTypesModules = SetDefaultTypesModules();
            DictionaryTypesIO = SetDefaultTypesIO();
        }

        public ModuleInfo GetModuleInfoByType(string type)
        {
            return DictionaryTypesModules[type];
        }

        public TagIOInfo GetTagIOInfoByType(string type)
        {
            return DictionaryTypesIO[type];
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
                    ReadParams(new IniFile(path));
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
            NAME_LIST_MODULES = iniFile.ReadINI(NAMESECTIONROWS, "NAME_LIST_MODULES");
            NAME_LIST_IO = iniFile.ReadINI(NAMESECTIONROWS, "NAME_LIST_IO");
            ROW_MODULE_NAME = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_MODULE_NAME"));
            ROW_MODULE_TYPE = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_MODULE_TYPE"));
            ROW_MODULE_ID = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_MODULE_ID"));
            ROW_IO_ID = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_IO_ID"));
            ROW_IO_NAME_MODULE = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_IO_NAME_MODULE"));
            ROW_IO_TYPE_MODULE = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_IO_TYPE_MODULE"));
            ROW_IO_CHANNEL = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_IO_CHANNEL"));
            ROW_IO_TYPE_IO = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_IO_TYPE_IO"));
            ROW_IO_NAME = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_IO_NAME"));
            ROW_IO_DESCR = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_IO_DESCR"));
            ROW_IO_SYSTEM = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_IO_SYSTEM"));
        }

        private void SaveDefault()
        {
            iniFile.Write(NAMESECTIONROWS, "NAME_LIST_MODULES", NAME_LIST_MODULES);
            iniFile.Write(NAMESECTIONROWS, "NAME_LIST_IO", NAME_LIST_IO);
            iniFile.Write(NAMESECTIONROWS, "ROW_MODULE_NAME", ROW_MODULE_NAME.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_MODULE_TYPE", ROW_MODULE_TYPE.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_MODULE_ID", ROW_MODULE_ID.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_IO_ID", ROW_IO_ID.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_IO_NAME_MODULE", ROW_IO_NAME_MODULE.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_IO_TYPE_MODULE", ROW_IO_TYPE_MODULE.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_IO_CHANNEL", ROW_IO_CHANNEL.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_IO_TYPE_IO", ROW_IO_TYPE_IO.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_IO_NAME", ROW_IO_NAME.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_IO_DESCR", ROW_IO_DESCR.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_IO_SYSTEM", ROW_IO_SYSTEM.ToString());
        }

        private void SetDefaultConfig()
        {
            SetDefaultRows();
        }

        private Dictionary<string, ModuleInfo> SetDefaultTypesModules()
        {
            return new Dictionary<string, ModuleInfo>() { { "AO-8", new ModuleInfo("io.AO8", 0, 8, ModuleInfo.TypeChannels.NONE, ModuleInfo.TypeChannels.REAL)},
                                                      { "AO-V-8", new ModuleInfo("io.AO8", 0, 8, ModuleInfo.TypeChannels.NONE, ModuleInfo.TypeChannels.REAL) },
                                                      { "DI-16", new ModuleInfo("io.DI16", 16, 0, ModuleInfo.TypeChannels.BOOL, ModuleInfo.TypeChannels.NONE) },
                                                      { "DO-FA-12", new ModuleInfo("io.DOFA12", 0, 12, ModuleInfo.TypeChannels.NONE, ModuleInfo.TypeChannels.BOOL) },
                                                      { "DO-FC-8", new ModuleInfo("io.DOFC8", 0, 8, ModuleInfo.TypeChannels.NONE, ModuleInfo.TypeChannels.BOOL) },
                                                      { "RTD-DI-16", new ModuleInfo("io.RTDDI16", 16, 0, ModuleInfo.TypeChannels.ALL, ModuleInfo.TypeChannels.NONE) },
                                                      { "UI-8.AO-4", new ModuleInfo("io.UI8AO4", 8, 4, ModuleInfo.TypeChannels.ALL, ModuleInfo.TypeChannels.REAL) },
                                                      { "UI-8.AO-V-4", new ModuleInfo("io.UI8AO4", 8, 4, ModuleInfo.TypeChannels.ALL, ModuleInfo.TypeChannels.REAL) },
                                                      { "UI-8.DO-FC-4", new ModuleInfo("io.UI8DOFC4", 8, 4, ModuleInfo.TypeChannels.ALL, ModuleInfo.TypeChannels.BOOL) },
                                                      { "UI-16", new ModuleInfo("io.UI16", 16, 0, ModuleInfo.TypeChannels.ALL, ModuleInfo.TypeChannels.NONE) },
                                                    };
        }

        private Dictionary<string, TagIOInfo> SetDefaultTypesIO()
        {
            return new Dictionary<string, TagIOInfo>() {  { "Вход цифровой", new TagIOInfo("io.point.DigitalInput") },
                                                        { "Вход счётчик", new TagIOInfo("io.point.CounterInput") },
                                                        { "Вход напряжения", new TagIOInfo("io.point.VoltageInput" )},
                                                        { "Вход токовый", new TagIOInfo("io.point.CurrentInput") },
                                                        { "Вход контролируемый", new TagIOInfo("io.point.SupervisedInput") },
                                                        { "Вход резистивный", new TagIOInfo("io.point.ResistiveInput") },
                                                        { "Вход температурный", new TagIOInfo("io.point.TemperatureInput") },
                                                        { "Вход 2-проводной R", new TagIOInfo("io.point.2WireRTDResistiveInput") },
                                                        { "Вход 2-проводной TE", new TagIOInfo("io.point.2WireRTDTemperatureInput") },
                                                        { "Вход 3-проводной R", new TagIOInfo("io.point.3WireRTDResistiveInput") },
                                                        { "Вход 3-проводной TE", new TagIOInfo("io.point.3WireRTDTemperatureInput" )},
                                                        { "Выход цифровой", new TagIOInfo("io.point.DigitalOutput" )},
                                                        { "Выход импульс", new TagIOInfo("io.point.DigitalPulsedOutput" )},
                                                        { "Выход напряжения", new TagIOInfo("io.point.VoltageOutput" )},
                                                        { "Выход токовый", new TagIOInfo("io.point.CurrentOutput" )},
                                                        { "Выход 3 состояния", new TagIOInfo("io.point.TristateOutput" )},
                                                        { "Выход импульс 3 состояний", new TagIOInfo("io.point.TristatePulsedOutput" )},
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
        }

    }
}
