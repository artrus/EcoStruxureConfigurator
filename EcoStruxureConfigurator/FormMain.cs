using System;
using System.Windows.Forms;

namespace EcoStruxureConfigurator
{
    public partial class FormMain : Form
    {
        Settings settings;
        string inputExcelFile;

        public FormMain()
        {
            readSettings();
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            createMenu();
            Text = inputExcelFile = readLastFileIO();
        }

        private void createMenu()
        {
            MainMenu mainMenu = new MainMenu();

            MenuItem menuItemFile = new MenuItem();
            MenuItem menuItemFileOpen = new MenuItem();
            MenuItem menuItemTools = new MenuItem();
            MenuItem menuItemToolsSettings = new MenuItem();

            menuItemFile.Text = "File";
            menuItemFileOpen.Text = "Open";
            menuItemFileOpen.Click += new System.EventHandler(this.menuItemFileOpen_Click);
            menuItemFile.MenuItems.Add(menuItemFileOpen);

            menuItemTools.Text = "Tools";
            menuItemToolsSettings.Text = "Settings";
            menuItemToolsSettings.Click += new System.EventHandler(this.menuItemToolsSettings_Click);
            menuItemTools.MenuItems.Add(menuItemToolsSettings);

            mainMenu.MenuItems.Add(menuItemFile);
            mainMenu.MenuItems.Add(menuItemTools);

            Menu = mainMenu;
        }

        private void menuItemFileOpen_Click(object sender, System.EventArgs e)
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
                }
                else
                {
                    throw new Exception("Файл не выбран!");
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
    }
}
