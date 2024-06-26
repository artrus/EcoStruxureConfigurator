﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using EcoStruxureConfigurator.Excel;
using EcoStruxureConfigurator.Logger;
using EcoStruxureConfigurator.Object;
using EcoStruxureConfigurator.XML;


namespace EcoStruxureConfigurator
{
    public partial class FormMain : Form
    {
        private Settings Settings;
        ILogger Logger;
        private string PathIOFile;
        private string PathObjectsFile;

        private List<TagIO> TagsIO;
        //private List<TagModbus> TagsModbusIO;
        private List<TagModbus> TagsChangedModbusObjects, TagsAllModbusObjects;
        private List<Module> Modules;
        private List<ObjectMatch> ObjectMatches;

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
            Logger.Clear();

            PathObjectsFile = ReadLastFileObjects();
            if (PathObjectsFile.Length != 0)
            {
                FileInfo fileInfoObjects = new FileInfo(PathObjectsFile);

                if (fileInfoObjects.Exists)
                {
                    ReadExcelObjects();
                    LblObjectFile.Text = fileInfoObjects.FullName;
                }
                else
                    LblObjectFile.Text = "Необходимо выбрать файл Objects.xlsx";

                PathIOFile = ReadLastFileIO();

                textBox_SEPPrefix.Text = Settings.SEPPrefix;
            }



        }

        private void CreateMenu()
        {
            MainMenu mainMenu = new MainMenu();

            MenuItem menuItemFile = new MenuItem();
            MenuItem menuItemFileOpenIO = new MenuItem();
            MenuItem menuItemFileOpenObjects = new MenuItem();
            MenuItem menuItemTools = new MenuItem();
            MenuItem menuItemToolsSettings = new MenuItem();

            menuItemFile.Text = "File";
            menuItemFileOpenIO.Text = "Open IO";
            menuItemFileOpenIO.Click += new System.EventHandler(this.MenuItemFileOpenIO_Click);
            menuItemFileOpenObjects.Text = "Open Objects";
            menuItemFileOpenObjects.Click += new System.EventHandler(this.MenuItemFileOpenObjects_Click);
            menuItemFile.MenuItems.Add(menuItemFileOpenIO);
            menuItemFile.MenuItems.Add(menuItemFileOpenObjects);

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
                MessageBox.Show(ex.Message, "Ошибка открытия IO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuItemFileOpenObjects_Click(object sender, System.EventArgs e)
        {
            try
            {
                OpenFileDialog openExcelFileDialog = new OpenFileDialog
                {
                    FileName = "Objects.xls",
                    Filter = "Excel files|*.xls;*.xlsx;*.xlsm",
                    FilterIndex = 0,
                    RestoreDirectory = true
                };
                DialogResult dialogResult = openExcelFileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    PathObjectsFile = openExcelFileDialog.FileName;
                    Text = PathObjectsFile;
                    Properties.Settings.Default.LastFileObjects = PathObjectsFile;
                    Properties.Settings.Default.Save();

                    ReadExcelObjects();

                }
                else
                {
                    //throw new Exception("Файл не выбран!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка открытия Objects!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuItemToolsSettings_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("Open");
        }

        private void CreateStatusBar()
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

        private string ReadLastFileObjects()
        {
            return Properties.Settings.Default.LastFileObjects;
        }

        private void ReadExcelObjects()
        {
            Logger.WriteLine("Начинается чтение файла Objects");
            ReadObjects readerObjects = new ReadObjects(Settings);
            var objects = readerObjects.OpenObjects(PathObjectsFile);
            if (objects.Count > 0)
            {
                Settings.AddObjects(objects);
                Logger.WriteLine("Файл Objects прочитан, найдено " + objects.Count + " объектов");
                foreach (var obj in objects)
                    Logger.WriteLine("Объект " + obj.Type + "   Найдено SP=" + obj.GetAllSP().Count + " ST=" + obj.GetAllST().Count);
                Logger.NewLine(); ;
            }
        }

        private void ReadExcelIO()
        {
            Logger.WriteLine("Начинается чтение файла IO");
            ReadIO readerIO = new ReadIO(Settings);
            TagsIO = readerIO.OpenIO(PathIOFile);
            //TagsModbusIO = ParserTags.GetTagsIOModbus(TagsIO, Settings);

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

            try
            {
                ObjectMatches = readerIO.ReadMatching(PathIOFile);
                TagsAllModbusObjects = ParserTags.GetTagsModbusByObjects(ObjectMatches, Settings);
                chListSysGen.Items.Clear();

                foreach (var match in ObjectMatches)
                {
                    chListSysGen.Items.Add(match.SystemNameRus);
                    chListSysGen.SetItemChecked(chListSysGen.Items.Count - 1, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно распознать файил IO\n\r" + ex.Message);
            }


        }

        private void BtnGenXML_Click(object sender, EventArgs e)
        {
            string dir = Path.GetDirectoryName(PathIOFile);
            string fileName = Path.GetFileNameWithoutExtension(PathIOFile);

            XML_generator generator = new XML_generator();
            generator.CreateIO(dir + @"\" + fileName + @"---IO.xml", TagsIO, Modules);
            //generator.CreateModbusIO(dir + @"\" + fileName + @"---ModbusIO.xml", TagsModbusIO);

            List<string> cheakedSystems = new List<string>();

            for (int i = 0; i < chListSysGen.Items.Count; i++)
            {
                if (chListSysGen.GetItemChecked(i))
                {
                    cheakedSystems.Add((string)chListSysGen.Items[i]);
                }
            }

            List<ObjectMatch> objectMatches = new List<ObjectMatch>();
            foreach (var match in cheakedSystems)
            {
                if (ObjectMatches.Exists(x => x.SystemNameRus == match))
                    objectMatches.Add(ObjectMatches.Find(x => x.SystemNameRus == match));
            }
            Logger.WriteLine("Выбрано " + objectMatches.Count + " объектов");
            TagsChangedModbusObjects = ParserTags.GetTagsModbusByObjects(objectMatches, Settings);
            generator.CreateModbusObjects(dir + @"\" + fileName + @"---ModbusObjects.xml", TagsChangedModbusObjects);
        }

        private void BtnGenExcel_Click(object sender, EventArgs e)
        {
            string dir = Path.GetDirectoryName(PathIOFile);
            string fileName = Path.GetFileNameWithoutExtension(PathIOFile);
            List<TagModbus> tagsModbus = new List<TagModbus>();
            //tagsModbus.AddRange(TagsModbusIO);
            tagsModbus.AddRange(TagsAllModbusObjects);
            ModbusTableGen.WriteNewExcel(Settings, dir + @"\" + fileName + @"---Modbus.xlsx", tagsModbus);

        }

        private void BtnGenWeintek_Click(object sender, EventArgs e)
        {
            string dir = Path.GetDirectoryName(PathIOFile);
            string fileName = Path.GetFileNameWithoutExtension(PathIOFile);
            List<TagModbus> tagsModbus = new List<TagModbus>();
            tagsModbus.AddRange(TagsAllModbusObjects);
            WeintekGen.WriteNewExcelTags(Settings, dir + @"\" + fileName + @"---WeintekTags.xlsx", tagsModbus);

        }

        private void BtnGenWeintekAlarms_Click(object sender, EventArgs e)
        {
            string dir = Path.GetDirectoryName(PathIOFile);
            string fileName = Path.GetFileNameWithoutExtension(PathIOFile);
            List<TagModbus> tagsModbus = new List<TagModbus>();
            tagsModbus.AddRange(TagsAllModbusObjects);
            WeintekGen.WriteNewExcelAlarms(Settings, dir + @"\" + fileName + @"---WeintekAlarms.xlsx", tagsModbus);

        }


        private void BtnGenSEP_Click(object sender, EventArgs e)
        {
            string dir = Path.GetDirectoryName(PathIOFile);
            string fileName = Path.GetFileNameWithoutExtension(PathIOFile);
            List<TagModbus> tagsModbus = new List<TagModbus>();
            tagsModbus.AddRange(TagsAllModbusObjects);
            SepGen.WriteNewExcel(Settings, dir + @"\" + fileName + @"---SepTagsEng.xlsx", tagsModbus, SepGen.Language.ENG);
            SepGen.WriteNewExcel(Settings, dir + @"\" + fileName + @"---SepTagsRus.xlsx", tagsModbus, SepGen.Language.RUS);
        }

        private void TextBox_SEPPrefix_TextChanged(object sender, EventArgs e)
        {
            Settings.SetSEPPrefix(textBox_SEPPrefix.Text);
        }

        private void BtnParseIO_Click(object sender, EventArgs e)
        {
            bool fileExist = File.Exists(PathIOFile);
            if (fileExist)
            {
                FileInfo fileInfoIO = new FileInfo(PathIOFile);
                if (fileInfoIO.Exists)
                {
                    ReadExcelIO();
                    Text = PathIOFile;
                }
                else
                    Text = "";

            }


        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.SaveFile();
        }

    }
}
