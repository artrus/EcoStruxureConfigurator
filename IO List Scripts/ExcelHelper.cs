using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IO_List_Scripts
{
    internal class ExcelHelper
    {
        public void Replace(string path, List<ReplaceText> replaceTexts)
        {
            string newFile = Dublicate(path, "---Replace.xlsx");

            ExcelPackage replaceFile = new ExcelPackage(new FileInfo(newFile));
            try
            {
                ExcelWorksheet worksheet = replaceFile.Workbook.Worksheets["Sheet1"];
                int count = ReplaceTags(worksheet, replaceTexts);
                MessageBox.Show($"Найдено и заменено {count} тегов");

                replaceFile.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "    Ошибка замены");
            }

        }

        private string Dublicate(string path, string prefix)
        {
            FileInfo inputFileInfo = new FileInfo(path);

            if (inputFileInfo.Exists)
            {
                string dir = Path.GetDirectoryName(path);
                string fileName = Path.GetFileNameWithoutExtension(path);
                string newFile = dir + @"\" + fileName + prefix;
                File.Copy(path, newFile, true);
                return newFile;
            }
            else
                return null;
        }

        private int ReplaceTags(ExcelWorksheet worksheet, List<ReplaceText> replaceTexts)
        {
            int rowCount = worksheet.Dimension.End.Row;
            int columnCount = worksheet.Dimension.End.Column;
            int count = 0;
            for (int i = 1; i <= columnCount; i++)
            {
                for (int j = 1; j <= rowCount; j++)
                {
                    string cell = worksheet.Cells[j, i].Value?.ToString();
                    if (String.IsNullOrEmpty(cell))
                        continue;

                    foreach (var replaceText in replaceTexts)
                    {
                        if (cell.Contains(replaceText.From))
                        {
                            string newText = cell.Replace(replaceText.From, replaceText.To);
                            worksheet.Cells[j, i].Value = newText;
                            count++;
                            continue;
                        }
                    }
                }
            }
            return count;
        }


        public List<ReplaceText> ReadReplaceTexts(string path)
        {
            List<ReplaceText> replaceTexts = new List<ReplaceText>();
            ExcelPackage replaceFile = new ExcelPackage(new FileInfo(path));
            try
            {
                ExcelWorksheet worksheet = replaceFile.Workbook.Worksheets[1];
                replaceTexts = ParceReplaceTexts(worksheet);
                MessageBox.Show($"Найдено {replaceTexts.Count} строк для замен");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "    Ошибка замены");
            }
            return replaceTexts;
        }

        private List<ReplaceText> ParceReplaceTexts(ExcelWorksheet worksheet)
        {
            List<ReplaceText> replaceTexts = new List<ReplaceText>();
            int rowCount = worksheet.Dimension.End.Row;
            for (int i = 1; i <= rowCount; i++)
            {
                string findText = worksheet.Cells[i, 1].Value?.ToString();
                string replaceText = worksheet.Cells[i, 2].Value?.ToString();

                if (String.IsNullOrEmpty(findText))
                    continue;

                replaceTexts.Add(new ReplaceText(findText, replaceText));
            }
            return replaceTexts;
        }

        internal void doFirstText(string path, List<string> doFirstList)
        {
            string newFile = Dublicate(path, "---DoFirst.xlsx");

            ExcelPackage replaceFile = new ExcelPackage(new FileInfo(newFile));
            try
            {
                ExcelWorksheet worksheet = replaceFile.Workbook.Worksheets["Sheet1"];
                int count = doFirst(worksheet, doFirstList);
                MessageBox.Show($"Перенесено {count} тегов");

                replaceFile.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "    Ошибка перестановки");
            }
        }

        private int doFirst(ExcelWorksheet worksheet, List<string> doFirstList)
        {
            int rowCount = worksheet.Dimension.End.Row;
            int columnCount = worksheet.Dimension.End.Column;
            int count = 0;
            for (int i = 1; i <= columnCount; i++)
            {
                for (int j = 1; j <= rowCount; j++)
                {
                    string cell = worksheet.Cells[j, i].Value?.ToString();
                    if (String.IsNullOrEmpty(cell))
                        continue;

                    foreach (var text in doFirstList)
                    {
                        if (cell.Contains(text))
                        {
                            string deleteText = cell.Replace(text, "").Replace("  ", " ");
                            string newText = text + " " + deleteText;
                            worksheet.Cells[j, i].Value = newText;
                            count++;
                            continue;
                        }
                    }
                }
            }
            return count;
        }


        internal void DeleteSpacesEnd(string path)
        {
            string newFile = Dublicate(path, "---DeleteSpacesEnd.xlsx");

            ExcelPackage replaceFile = new ExcelPackage(new FileInfo(newFile));
            try
            {
                ExcelWorksheet worksheet = replaceFile.Workbook.Worksheets["Sheet1"];
                int count = DeleteSpaces(worksheet);
                MessageBox.Show($"Удалено {count} пробелов");

                replaceFile.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "    Ошибка удаления пробелов");
            }
        }

        private int DeleteSpaces(ExcelWorksheet worksheet)
        {
            int rowCount = worksheet.Dimension.End.Row;
            int columnCount = worksheet.Dimension.End.Column;
            int count = 0;
            for (int i = 1; i <= columnCount; i++)
            {
                for (int j = 1; j <= rowCount; j++)
                {
                    string cell = worksheet.Cells[j, i].Value?.ToString();
                    if (String.IsNullOrEmpty(cell))
                        continue;

                    if (cell.EndsWith("  "))
                    {
                        string newText = cell.Remove(cell.Length - 2, 2);
                        worksheet.Cells[j, i].Value = newText;
                        count++;
                    }

                    if (cell.EndsWith(" "))
                    {
                        string newText = cell.Remove(cell.Length - 1, 1);
                        worksheet.Cells[j, i].Value = newText;
                        count++;
                    }

                }
            }

            return count;
        }
    }
}
