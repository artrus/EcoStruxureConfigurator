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
        private Settings settings;
        ILogger logger;
        private string pathIOFile;

        private List<TagIO> tagsIO;
        private List<Module> modules;        

        public FormMain()
        {
            ReadSettings();
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            CreateMenu();
            CreateStatusBar();
            logger = new LoggerToRichBox(log);

            pathIOFile = ReadLastFileIO();
            FileInfo fileInfo = new FileInfo(pathIOFile);
            if (fileInfo.Exists)
            {
                ReadExcelIO();
                Text = pathIOFile;
            }
            else
            {
                Text = "";
            }
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
                    pathIOFile = openExcelFileDialog.FileName;
                    Text = pathIOFile;
                    Properties.Settings.Default.LastFileIO = pathIOFile;
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
                this.settings = settings;
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
            logger.Clear();
            logger.WriteLine("Начинается чтение файла IO");
            ReadIO readerIO = new ReadIO(settings);
            tagsIO = readerIO.OpenIO(pathIOFile);
            if (tagsIO.Count != 0)
            {
                logger.WriteLine("Файл с IO прочитан успешно! Найдено тэгов: " + tagsIO.Count);

                modules = ParserTags.GetModules(tagsIO);

                logger.WriteLine("Найдено модулей: " + modules.Count);

                foreach (var module in modules)
                {
                    logger.WriteLine("ID=" + module.ID + "   " + "Name=" + module.Name + "   " + "Type=" + module.Type);
                }
            }
        }

        private void BtnGenIO_Click(object sender, EventArgs e)
        {
            string dir = Path.GetDirectoryName(pathIOFile);
            string fileName = Path.GetFileNameWithoutExtension(pathIOFile);

            XML_generator generator = new XML_generator(settings);
            generator.CreateIO(dir + @"\" + fileName + @"---IO.xml", tagsIO, modules);
        }
    }
}
