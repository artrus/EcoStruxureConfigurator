using EcoStruxureConfigurator.Object;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoStruxureConfigurator
{
    public class ReadIO
    {
        private readonly Settings Settings;

        public ReadIO(Settings settings)
        {
            Settings = settings;
        }

        public List<TagIO> OpenIO(string path)
        {
            FileInfo existingFile = new FileInfo(path);
            if (!existingFile.Exists)
            {
                return new List<TagIO>();
            }
            ExcelPackage package = new ExcelPackage(existingFile);
            try
            {
                //get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[Settings.NAME_LIST_IO];
                return GetTagsIO(worksheet);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "    Ошибка парсинга IO");
                return null;
            }
        }

        private List<TagIO> GetTagsIO(ExcelWorksheet worksheet)
        {
            List<TagIO> tags = new List<TagIO>();
            int rowCount = worksheet.Dimension.End.Row;     //get row count

            int id = 0;
            string moduleName;
            string moduleType;
            Module module = null; ;
            for (int i = 2; i <= rowCount; i++)
            {
                string name = worksheet.Cells[i, Settings.ROW_IO_NAME].Value?.ToString();
                string descr = worksheet.Cells[i, Settings.ROW_IO_DESCR].Value?.ToString();
                string channel = worksheet.Cells[i, Settings.ROW_IO_CHANNEL].Value?.ToString();
                string system = worksheet.Cells[i, Settings.ROW_IO_SYSTEM].Value?.ToString();

                try
                {
                    id = Int32.Parse(worksheet.Cells[i, Settings.ROW_IO_ID].Value?.ToString());
                }
                catch
                { }

                if (worksheet.Cells[i, Settings.ROW_IO_TYPE_MODULE].Value?.ToString() != null)  //if find new module in IO
                {
                    moduleType = worksheet.Cells[i, Settings.ROW_IO_TYPE_MODULE].Value?.ToString();
                    moduleName = worksheet.Cells[i, Settings.ROW_IO_NAME_MODULE].Value?.ToString();
                    module = new Module(id, moduleName, moduleType, Settings.GetModuleInfoByType(moduleType));
                }
                string tagType = worksheet.Cells[i, Settings.ROW_IO_TYPE_IO].Value?.ToString();
                tags.Add(new TagIO(name, descr, system, " ", module, Int32.Parse(channel), Settings.GetTagIOInfoByType(tagType)));
            }
            return tags;
        }

        public List<ObjectMatch> ReadMatching(string path)
        {
            FileInfo existingFile = new FileInfo(path);
            if (!existingFile.Exists)
            {
                throw new FileNotFoundException(path);
            }
            ExcelPackage package = new ExcelPackage(existingFile);
            try
            {
                //get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[Settings.NAME_LIST_OBJECTS];
                return GetObjectMatching(worksheet);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "    Ошибка парсинга Object Matching");
                return null;
            }
        }

        private List<ObjectMatch> GetObjectMatching(ExcelWorksheet worksheet)
        {
            List<ObjectMatch> objectMatches = new List<ObjectMatch>();
            int rowCount = worksheet.Dimension.End.Row;

            for (int row = 2; row < rowCount; row++)
            {
                string name = worksheet.Cells[row, 1].Value?.ToString();
                string psevdoName = worksheet.Cells[row, 2].Value?.ToString();
                if (string.IsNullOrEmpty(name))
                    continue;

                var objMatch = new ObjectMatch(name, psevdoName);

                int columnCount = worksheet.Dimension.End.Column;

                for (int column = 3; column < columnCount; column += 3)
                {
                    string type = worksheet.Cells[row, column].Value?.ToString();
                    string descrEng = worksheet.Cells[row, column + 1].Value?.ToString();
                    string descrRus = worksheet.Cells[row, column + 2].Value?.ToString();
                    if (string.IsNullOrEmpty(type))
                        continue;
                    ObjectBase objectBase = Settings.GetObjectByName(type);
                    objMatch.AddObject(descrEng, descrRus, objectBase);
                }
                objectMatches.Add(objMatch);

            }

            return objectMatches;
        }

    }
}
