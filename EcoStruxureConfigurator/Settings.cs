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
        public Dictionary<string, string> DictionaryTypesIO { get; set; }

        public int ROW_MODULE_NAME;
        public int ROW_MODULE_TYPE;
        public int ROW_MODULE_ID;

        public int ROW_IO_ID;
        public int ROW_IO_NAME_MODULE;
        public int ROW_IO_TYPE_MODULE;
        public int ROW_IO_CHANNEL;
        public int ROW_IO_NAME;
        public int ROW_IO_DESCR;
        public int ROW_IO_SYSTEM;

        public string NAME_LIST_IO;
        public string NAME_LIST_MODULES;

        private IniFile iniFile;
        private const string NAMESECTIONROWS = "ROWS";


        public void ReadSetting(string path)
        {
            openINI(path);
            DictionaryTypesModules = setDefaultTypesModules();
            DictionaryTypesIO = setDefaultTypesIO();
        }

        public ModuleInfo getModuleInfoByType(string type)
        {
            return DictionaryTypesModules[type];
        }

        private void openINI(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("Создан новый файл конфигурации по умолчанию", "Не найден файл config.ini", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    File.Create(path).Close();
                    iniFile = new IniFile(path);
                    setDefaultConfig();
                    saveDefault();
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
                    readParams(new IniFile(path));
                }
                catch
                {
                    var result = MessageBox.Show("Ошибка в конфигурациии. Создать заново?", "Ошибка в файле config.ini", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.No)
                        throw new ArgumentException();
                    else
                    {
                        File.Delete(path);
                        openINI(path);
                    }
                }

            }
        }

        private void readParams(IniFile iniFile)
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
            ROW_IO_NAME = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_IO_NAME"));
            ROW_IO_DESCR = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_IO_DESCR"));
            ROW_IO_SYSTEM = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_IO_SYSTEM"));
        }

        private void saveDefault()
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
            iniFile.Write(NAMESECTIONROWS, "ROW_IO_NAME", ROW_IO_NAME.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_IO_DESCR", ROW_IO_DESCR.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_IO_SYSTEM", ROW_IO_SYSTEM.ToString());


        }

        private void setDefaultConfig()
        {
            setDefaultRows();
        }

        private Dictionary<string, ModuleInfo> setDefaultTypesModules()
        {
            return new Dictionary<string, ModuleInfo>() { { "AO-8", new ModuleInfo("io.AO8", 0, 8, ModuleInfo.typeChannels.NONE, ModuleInfo.typeChannels.REAL)},
                                                      { "AO-V-8", new ModuleInfo("io.AO8", 0, 8, ModuleInfo.typeChannels.NONE, ModuleInfo.typeChannels.REAL) },
                                                      { "DI-16", new ModuleInfo("io.DI16", 16, 0, ModuleInfo.typeChannels.BOOL, ModuleInfo.typeChannels.NONE) },
                                                      { "DO-FA-12", new ModuleInfo("io.DOFA12", 0, 12, ModuleInfo.typeChannels.NONE, ModuleInfo.typeChannels.BOOL) },
                                                      { "DO-FC-8", new ModuleInfo("io.DOFC8", 0, 8, ModuleInfo.typeChannels.NONE, ModuleInfo.typeChannels.BOOL) },
                                                      { "RTD-DI-16", new ModuleInfo("io.RTDDI16", 16, 0, ModuleInfo.typeChannels.ALL, ModuleInfo.typeChannels.NONE) },
                                                      { "UI-8.AO-4", new ModuleInfo("io.UI8AO4", 8, 4, ModuleInfo.typeChannels.ALL, ModuleInfo.typeChannels.REAL) },
                                                      { "UI-8.AO-V-4", new ModuleInfo("io.UI8AO4", 8, 4, ModuleInfo.typeChannels.ALL, ModuleInfo.typeChannels.REAL) },
                                                      { "UI-8.DO-FC-4", new ModuleInfo("io.UI8DOFC4", 8, 4, ModuleInfo.typeChannels.ALL, ModuleInfo.typeChannels.BOOL) },
                                                      { "UI-16", new ModuleInfo("io.UI16", 16, 0, ModuleInfo.typeChannels.ALL, ModuleInfo.typeChannels.NONE) },
                                                    };
        }

        private Dictionary<string, string> setDefaultTypesIO()
        {
            return new Dictionary<string, string>() {   { "Вход цифровой", "io.point.DigitalInput" },
                                                        { "Вход счётчик", "io.point.CounterInput" },
                                                        { "Вход напряжения", "io.point.VoltageInput" },
                                                        { "Вход токовый", "io.point.CurrentInput" },
                                                        { "Вход контролируемый", "io.point.SupervisedInput" },
                                                        { "Вход резистивный", "io.point.ResistiveInput" },
                                                        { "Вход температурный", "io.point.TemperatureInput" },
                                                        { "Вход 2-проводной R", "io.point.2WireRTDResistiveInput" },
                                                        { "Вход 2-проводной TE", "io.point.2WireRTDTemperatureInput" },
                                                        { "Вход 3-проводной R", "io.point.3WireRTDResistiveInput" },
                                                        { "Вход 3-проводной TE", "io.point.3WireRTDTemperatureInput" },
                                                        { "Выход цифровой", "io.point.DigitalOutput" },
                                                        { "Выход импульс", "io.point.DigitalPulsedOutput" },
                                                        { "Выход напряжения", "io.point.VoltageOutput" },
                                                        { "Выход токовый", "io.point.CurrentOutput" },
                                                        { "Выход 3 состояния", "io.point.TristateOutput" },
                                                        { "Выход импульс 3 состояний", "io.point.TristatePulsedOutput" },
                                                    };
        }

        private void setDefaultRows()
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
            ROW_IO_NAME = 7;
            ROW_IO_DESCR = 8;
            ROW_IO_SYSTEM = 9;
        }

    }
}
