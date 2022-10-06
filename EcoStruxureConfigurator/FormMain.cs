using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EcoStruxureConfigurator
{
    public partial class FormMain : Form
    {
        private Settings settings;
        private string inputExcelFile;
        
        public FormMain()
        {
            readSettings();
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            createMenu();
            createStatusBar();
            Text = inputExcelFile = readLastFileIO();
            if (Text != null)
                readExcelIO(inputExcelFile);
        }

        private void createMenu()
        {
            MainMenu mainMenu = new MainMenu();

            MenuItem menuItemFile = new MenuItem();
            MenuItem menuItemFileOpenIO = new MenuItem();
            MenuItem menuItemTools = new MenuItem();
            MenuItem menuItemToolsSettings = new MenuItem();

            menuItemFile.Text = "File";
            menuItemFileOpenIO.Text = "Open IO";
            menuItemFileOpenIO.Click += new System.EventHandler(this.menuItemFileOpenIO_Click);
            menuItemFile.MenuItems.Add(menuItemFileOpenIO);

            menuItemTools.Text = "Tools";
            menuItemToolsSettings.Text = "Settings";
            menuItemToolsSettings.Click += new System.EventHandler(this.menuItemToolsSettings_Click);
            menuItemTools.MenuItems.Add(menuItemToolsSettings);

            mainMenu.MenuItems.Add(menuItemFile);
            mainMenu.MenuItems.Add(menuItemTools);

            Menu = mainMenu;
        }

        private void menuItemFileOpenIO_Click(object sender, System.EventArgs e)
        {
            try
            {
                OpenFileDialog openExcelFileDialog = new OpenFileDialog();
                openExcelFileDialog.FileName = "IO.xls";
                openExcelFileDialog.Filter = "Excel files|*.xls;*.xlsx;*.xlsm";
                openExcelFileDialog.FilterIndex = 0;
                openExcelFileDialog.RestoreDirectory = true;
                DialogResult dialogResult = openExcelFileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    inputExcelFile = openExcelFileDialog.FileName;
                    Text = inputExcelFile;
                    Properties.Settings.Default.LastFileIO = inputExcelFile;
                    Properties.Settings.Default.Save();

                    readExcelIO(inputExcelFile);

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

        private void menuItemToolsSettings_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("Open");
        }

        private void createStatusBar ()
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
        
        private void readSettings()
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

        private string readLastFileIO()
        {
            return Properties.Settings.Default.LastFileIO;
        }

        private void readExcelIO (string path)
        {
            log.Clear();
            ReadIO readerIO = new ReadIO(settings);
            List<TagIO> tagsIO = readerIO.OpenIO(inputExcelFile);
            if (tagsIO.Count != 0)
                log.AppendText("Файл с IO прочитан успешно! Найдено тэгов: " + tagsIO.Count);
        }
    }
}
