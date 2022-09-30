using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoStruxureConfigurator
{
    internal class Settings
    {
        public enum TYPE_PARAM { ROWS }
        public Dictionary<string, string> DictionaryTypesModules { get; set; }
        public Dictionary<string, string> DictionaryTypesIO { get; set; }

        public int ROW_MODULE_NAME;
        public int ROW_MODULE_TYPE;
        public int ROW_MODULE_ID;

        public int ROW_IO_ID;
        public int ROW_IO_TYPE;
        public int ROW_IO_NAME;
        public int ROW_IO_DESCR;
        public int ROW_IO_SYSTEM;


        private IniFile iniFile;
        private const string NAMESECTIONROWS = "ROWS";


        public void ReadSetting(string path)
        {
            openINI(path);
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
                    setDefault();
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
            ROW_MODULE_NAME = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_MODULE_NAME"));
            ROW_MODULE_TYPE = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_MODULE_TYPE"));
            ROW_MODULE_ID = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_MODULE_ID"));
            ROW_IO_ID = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_IO_ID"));
            ROW_IO_TYPE = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_IO_TYPE"));
            ROW_IO_NAME = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_IO_NAME"));
            ROW_IO_DESCR = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_IO_DESCR"));
            ROW_IO_SYSTEM = Int32.Parse(iniFile.ReadINI(NAMESECTIONROWS, "ROW_IO_SYSTEM"));
        }

        private void saveDefault()
        {
            iniFile.Write(NAMESECTIONROWS, "ROW_MODULE_NAME", ROW_MODULE_NAME.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_MODULE_TYPE", ROW_MODULE_TYPE.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_MODULE_ID", ROW_MODULE_ID.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_IO_ID", ROW_IO_ID.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_IO_TYPE", ROW_IO_TYPE.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_IO_NAME", ROW_IO_NAME.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_IO_DESCR", ROW_IO_DESCR.ToString());
            iniFile.Write(NAMESECTIONROWS, "ROW_IO_SYSTEM", ROW_IO_SYSTEM.ToString());
        }

        private void setDefault()
        {
            DictionaryTypesModules = setDefaultTypesModules();
            DictionaryTypesIO = setDefaultTypesIO();
            setDefaultRows();
        }

        private Dictionary<string, string> setDefaultTypesModules()
        {
            return new Dictionary<string, string>() { { "AO-8", "io.AO8" },
                                                      { "AO-V-8", "io.AO8" },
                                                      { "DI-16", "io.DI16" },
                                                      { "DO-FA-12", "io.DOFA12" },
                                                      { "DO-FC-8", "io.DOFC8" },
                                                      { "RTD-DI-16", "io.RTDDI16" },
                                                      { "UI-8.AO-4", "io.UI8AO4" },
                                                      { "UI-8.AO-V-4", "io.UI8AO4" },
                                                      { "UI-8.DO-FC-4", "io.UI8DOFC4" },
                                                      { "UI-16", "io.UI16" },
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
            ROW_MODULE_NAME = 1;
            ROW_MODULE_TYPE = 2;
            ROW_MODULE_ID = 3;
            ROW_IO_ID = 0;
            ROW_IO_TYPE = 4;
            ROW_IO_NAME = 6;
            ROW_IO_DESCR = 7;
            ROW_IO_SYSTEM = 8;
        }

    }
}
