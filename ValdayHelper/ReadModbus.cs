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
    public class ReadModbus
    {
        public List<Tag> OpenModbus(string path)
        {
            if (path == null)
                throw new ArgumentNullException("Неверный путь к ModbusRegs.xlsx");

            FileInfo existingFile = new FileInfo(path);
            if (!existingFile.Exists)
            {
                throw new ArgumentNullException("Не найден файл ModbusRegs.xlsx");
            }
            ExcelPackage package = new ExcelPackage(existingFile);
            try
            {
                //get the first worksheet in the workbook
                ExcelWorksheets worksheets = package.Workbook.Worksheets;
                var tags = GetModbus(worksheets);
                return tags;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "    Ошибка парсинга ModbusRegs.xlsx");
                return null;
            }
        }

        private List<Tag> GetModbus(ExcelWorksheets worksheets)
        {
            List<Tag> tags = new List<Tag>();

            ExcelWorksheet worksheet = worksheets["Input Registers"];
            int rowCount = worksheet.Dimension.End.Row;     //get row count

            for (int i = 3; i <= rowCount; i++)
            {
                string tagName = worksheet.Cells[i, 5].Value?.ToString();
                if (!String.IsNullOrEmpty(tagName))
                {
                    string description = worksheet.Cells[i, 2].Value?.ToString();
                    string classs = worksheet.Cells[i, 3].Value?.ToString();
                    int addr3x = Int32.TryParse(worksheet.Cells[i, 1].Value?.ToString(), out int result) ? result : -1;
                    Tag tag = new Tag(tagName, description, classs)
                    {
                        Addr3X = addr3x
                    };
                    tags.Add(tag);
                }

            }

            worksheet = worksheets["Holding Registers"];
            rowCount = worksheet.Dimension.End.Row;     //get row count

            for (int i = 3; i <= rowCount; i++)
            {
                string tagName = worksheet.Cells[i, 5].Value?.ToString();
                if ((tags.FindAll(x => x.Name == tagName).Count == 0) && !String.IsNullOrEmpty(tagName))
                {
                    string description = worksheet.Cells[i, 2].Value?.ToString();
                    string classs = worksheet.Cells[i, 3].Value?.ToString();
                    int addr4x = Int32.TryParse(worksheet.Cells[i, 1].Value?.ToString(), out int result) ? result : -1;
                    Tag tag = new Tag(tagName, description, classs)
                    {
                        Addr4X = addr4x
                    };
                    tags.Add(tag);
                }
                else if (tags.FindAll(x => x.Name == tagName).Count > 1)
                    throw new Exception("Найдены более 2х копий одной переменной. Тег: " + tagName);
                else if (tags.FindAll(x => x.Name == tagName).Count == 1)
                {
                    Tag tag = tags.Find(x => x.Name == tagName);
                    int addr = Int32.TryParse(worksheet.Cells[i, 1].Value?.ToString(), out int result) ? result : -1;
                    tag.Addr4X = addr;
                }



            }
            return tags;
        }
    }
}
