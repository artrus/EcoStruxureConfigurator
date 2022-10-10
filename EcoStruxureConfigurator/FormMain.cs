using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using EcoStruxureConfigurator.Logger;
using EcoStruxureConfigurator.XML;

namespace EcoStruxureConfigurator
{
    public partial class FormMain : Form
    {
        private Settings Settings;
        ILogger Logger;
        private string PathIOFile;

        private List<TagIO> TagsIO;
        private List<TagModbus> TagsModbus;
        private List<Module> Modules;        

        public FormMain()
        {
            ReadSettings();
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            CreateMenu();
            CreateStatusBar();
            Logger = new LoggerToRichBox(log);

            PathIOFile = ReadLastFileIO();
            FileInfo fileInfo = new FileInfo(PathIOFile);
            if (fileInfo.Exists)
            {
                ReadExcelIO();
                Text = PathIOFile;
            }
            else
                Text = "";
        }

        private void CreateMenu()
        {
            MainMenu mainMenu = new MainMenu();

            MenuItem menuItemFile = new MenuItem();
            MenuItem menuItemFileOpenIO = new MenuItem();
            MenuItem menuItemTools = new MenuItem();
            MenuItem menuItemToolsSettings = new MenuItem();

            menuItemFile.Text = "File";
            menuItemFileOpenIO.Text = "Open IO";
            menuItemFileOpenIO.Click += new System.EventHandler(this.MenuItemFileOpenIO_Click);
            menuItemFile.MenuItems.Add(menuItemFileOpenIO);

            menuItemTools.Text = "Tools";
            menuItemToolsSettings.Text = "Settings";
            menuItemToolsSettings.Click += new System.EventHandler(this.MenuItemToolsSettings_Click);
            menuItemTools.MenuItems.Add(menuItemToolsSettings);

            mainMenu.MenuItems.Add(menuItemFile);
            mainMenu.MenuItems.Add(menuItemTools);

            Menu = mainMenu;
        }

        private void MenuItemFileOpenIO_Click(object sender, System.EventArgs e)
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
                    PathIOFile = openExcelFileDialog.FileName;
                    Text = PathIOFile;
                    Properties.Settings.Default.LastFileIO = PathIOFile;
                    Properties.Settings.Default.Save();

                    ReadExcelIO();

                }
                else
                {
                    //throw new Exception("Файл не выбран!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuItemToolsSettings_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("Open");
        }

        private void CreateStatusBar ()
        {
            StatusBar mainStatusBar = new StatusBar();

            StatusBarPanel statusPanel = new StatusBarPanel();
            StatusBarPanel datetimePanel = new StatusBarPanel();

            // Set first panel properties and add to StatusBar  
            statusPanel.BorderStyle = StatusBarPanelBorderStyle.Sunken;
            statusPanel.Text = "Application started. ";
            statusPanel.ToolTipText = "Last Activity";
            statusPanel.AutoSize = StatusBarPanelAutoSize.Spring;
            mainStatusBar.Panels.Add(statusPanel);

            // Set second panel properties and add to StatusBar  

            datetimePanel.BorderStyle = StatusBarPanelBorderStyle.Raised;
            datetimePanel.ToolTipText = "DateTime: " + System.DateTime.Today.ToString();

            datetimePanel.Text = System.DateTime.Today.ToLongDateString();
            datetimePanel.AutoSize = StatusBarPanelAutoSize.Contents;
            mainStatusBar.Panels.Add(datetimePanel);


            mainStatusBar.ShowPanels = true;
            Controls.Add(mainStatusBar);
        }
        
        private void ReadSettings()
        {
            Settings settings = new Settings();
            try
            {
                settings.ReadSetting(@"config.ini");
                this.Settings = settings;
            }
            catch
            {
                System.Environment.Exit(0);
            }
        }

        private string ReadLastFileIO()
        {
            return Properties.Settings.Default.LastFileIO;
        }

        private void ReadExcelIO ()
        {
            Logger.Clear();
            Logger.WriteLine("Начинается чтение файла IO");
            ReadIO readerIO = new ReadIO(Settings);
            TagsIO = readerIO.OpenIO(PathIOFile);
            TagsModbus = ParserTags.GetTagsIOModbus(TagsIO, Settings);
            
            if (TagsIO.Count != 0)
            {
                Logger.WriteLine("Файл с IO прочитан успешно! Найдено тэгов: " + TagsIO.Count);

                Modules = ParserTags.GetModules(TagsIO);

                Logger.WriteLine("Найдено модулей: " + Modules.Count);

                foreach (var module in Modules)
                {
                    Logger.WriteLine("ID=" + module.ID + "   " + "Name=" + module.Name + "   " + "Type=" + module.Type);
                }
            }
        }

        private void BtnGenIO_Click(object sender, EventArgs e)
        {
            string dir = Path.GetDirectoryName(PathIOFile);
            string fileName = Path.GetFileNameWithoutExtension(PathIOFile);

            XML_generator generator = new XML_generator(Settings);
            generator.CreateIO(dir + @"\" + fileName + @"---IO.xml", TagsIO, Modules);
        }

        private void BtnGenModbus_Click(object sender, EventArgs e)
        {
            string dir = Path.GetDirectoryName(PathIOFile);
            string fileName = Path.GetFileNameWithoutExtension(PathIOFile);
           
           // WriteIO.WriteExcel(Settings, dir + @"\" + fileName + @"---Modbus.xlsx", TagsModbus);

            /* foreach (var tag in tagsModbus)
                 logger.WriteLine(tag.Name + " " + tag.Description + " " + tag.Register + " " + tag.TagInfo.TypeName);*/

            XML_generator generator = new XML_generator(Settings);
            generator.CreateModbusIO(dir + @"\" + fileName + @"---ModbusIO.xml", TagsModbus);
        }
    }
}
