using EcoStruxureConfigurator.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValdayHelper
{
    public partial class MainWindow : Form
    {
        Settings settings = null;
        ILogger Logger;
        List<ValdayClass> classes = null;
        List<Tag> tags = null;
        string PathModbusFile;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            settings = new Settings();
            Logger = new LoggerToRichBox(log);
            Logger.Clear();
            ReadClasses();
            textBoxPrefix.Text = Properties.Settings.Default.Prefix;
            textBoxStation.Text = Properties.Settings.Default.Station;
        }

        private void ReadClasses()
        {

            ReadClasses readerClasses = new ReadClasses();
            classes = readerClasses.OpenObjects(@"Classes.xlsx");

            if (classes.Count != 0)
            {
                Logger.WriteLine("Файл с Classes прочитан успешно! Найдено классов: " + classes.Count);

                foreach (var classs in classes)
                {
                    Logger.WriteLine(classs.Name);
                }
            }
        }

        private void ReadExcelModbus()
        {
            ReadModbus readerExcelModus = new ReadModbus();
            tags = readerExcelModus.OpenModbus(PathModbusFile);

            if (tags.Count != 0)
                Logger.WriteLine("Файл ModbusRegs прочитан успешно! Найдено тэгов: " + tags.Count);

            foreach (var tag in tags)
            {
                string x3 = tag.Addr3X > 0 ? "   3X:" + tag.Addr3X.ToString() : "";
                string x4 = tag.Addr4X > 0 ? "   4X:" + tag.Addr4X.ToString() : "";
                Logger.WriteLine(tag.Name + x3 + x4);
            }
        }

        private void ButtonOpenModbus_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openExcelFileDialog = new OpenFileDialog
                {
                    FileName = "IO.xls",
                    Filter = "Excel files|*.xls;*.xlsx;*.xlsm",
                    FilterIndex = 0,
                    RestoreDirectory = true
                };
                DialogResult dialogResult = openExcelFileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    PathModbusFile = openExcelFileDialog.FileName;
                    Text = PathModbusFile;
                    Properties.Settings.Default.LastFileModbus = PathModbusFile;
                    Properties.Settings.Default.Save();

                    ReadExcelModbus();

                }
                else
                {
                    throw new Exception("Файл не выбран!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка открытия ModbusRegs!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextBoxPrefix_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Prefix = textBoxPrefix.Text;
            Properties.Settings.Default.Save();
        }

        private void TextBoxStation_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Station = textBoxStation.Text;
            Properties.Settings.Default.Save();
        }

        private void ButtonGen_Click(object sender, EventArgs e)
        {
            string dir = Path.GetDirectoryName(PathModbusFile);
            string fileName = Path.GetFileNameWithoutExtension(PathModbusFile);
            settings.Prefix = textBoxPrefix.Text;
            settings.Station = textBoxStation.Text;
            GenValdayTags.WriteNewExcel(settings, dir + @"\" + fileName + @"---Tags.xlsx", tags, classes);
        }

        private void ButtonWeintekTags_Click(object sender, EventArgs e)
        {
            string dir = Path.GetDirectoryName(PathModbusFile);
            string fileName = Path.GetFileNameWithoutExtension(PathModbusFile);
            settings.Prefix = textBoxPrefix.Text;
            settings.Station = textBoxStation.Text;
            Weintek.WriteNewExcelTags(settings, dir + @"\" + fileName + @"---WeintekTags.xlsx", tags, classes);
        }

        private void ButtonWeintekAlarms_Click(object sender, EventArgs e)
        {
            string dir = Path.GetDirectoryName(PathModbusFile);
            string fileName = Path.GetFileNameWithoutExtension(PathModbusFile);
            settings.Prefix = textBoxPrefix.Text;
            settings.Station = textBoxStation.Text;
            Weintek.WriteNewExcelAlarms(settings, dir + @"\" + fileName + @"---WeintekAlarms.xlsx", tags, classes);
        }
    }
}
