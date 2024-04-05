using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValdayHelper
{
    public class ReadClasses
    {
        public List<ValdayClass> OpenObjects(string path)
        {
            if (path == null)
                throw new ArgumentNullException("Неверный путь к Classes");

            FileInfo existingFile = new FileInfo(path);
            if (!existingFile.Exists)
            {
                throw new ArgumentNullException("Не найден файл Classes");
            }
            ExcelPackage package = new ExcelPackage(existingFile);
            try
            {
                //get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Classes"];
                var classes = GetClasses(worksheet);
                return classes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "    Ошибка парсинга Classes.xlsx");
                return null;
            }
        }

        private List<ValdayClass> GetClasses(ExcelWorksheet worksheet)
        {
            List<ValdayClass> classes = new List<ValdayClass>();
            int rowCount = worksheet.Dimension.End.Row;     //get row count

            List<TagInValdayClass> tagInClasses = new List<TagInValdayClass>();
            for (int i = 2; i <= rowCount; i++)
            {
                string className = worksheet.Cells[i, 1].Value?.ToString();

                if (String.IsNullOrEmpty(className))
                    continue;

                if (classes.Count == 0)
                {
                    classes.Add(new ValdayClass(className));
                }

                if (classes.FindAll(x => x.Name == className).Count == 0)
                {
                    classes.Add(new ValdayClass(className));
                }

                string tagName = worksheet.Cells[i, 2].Value?.ToString();
                int register = Int32.Parse(worksheet.Cells[i, 4].Value?.ToString());
                int bit = Int32.TryParse(worksheet.Cells[i, 5].Value?.ToString(), out int result) ? result : 16;
                string type = worksheet.Cells[i, 3].Value?.ToString();
                classes[classes.Count - 1].AddTag(tagName, type, register, bit);

            }
            return classes;
        }
    }
}
