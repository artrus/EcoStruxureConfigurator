using FinderModbusRegs;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IO_List_Scripts
{
    public class FinderInExcel
    {

        public List<ModbusRegs> Find(string path, List<string> matchNames)
        {
            var regs = new List<ModbusRegs>();
            
            var directoryInfo = new DirectoryInfo(path);

            foreach (var file in directoryInfo.GetFiles().OrderBy(f => f.Name))
            {
                if (file.Extension.Contains("xls"))
                {
                    var excelPackage = new ExcelPackage(new FileInfo(file.FullName));
                    try
                    {
                        Console.WriteLine($"Read {file.FullName}");
                        var worksheet = excelPackage.Workbook.Worksheets[1];
                        var findRegs = FindMatches(worksheet, matchNames, file.Name);
                        regs.AddRange(findRegs);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "    Ошибка чтения Excel");
                    }
                }
            }
            return regs;
        }

        private List<ModbusRegs> FindMatches(ExcelWorksheet worksheet, List<string> matchNames, string fileName)
        {
            var regs = new List<ModbusRegs>();
            int rowCount = worksheet.Dimension.End.Row;
            int columnCount = worksheet.Dimension.End.Column;
            for (int i = 2; i <= rowCount; i++)
            {
                for (int j = 1; j <= columnCount; j++)
                {
                    string cell = worksheet.Cells[i, j].Value?.ToString();

                    if (String.IsNullOrEmpty(cell))
                        continue;

                    if (matchNames.Exists(x => cell.Contains(x)))
                    {
                        string name = worksheet.Cells[i, 1].Value?.ToString();
                        string description = worksheet.Cells[i, 2].Value?.ToString();
                        string function = worksheet.Cells[i, 5].Value?.ToString();
                        string address = worksheet.Cells[i, 6].Value?.ToString();
                        regs.Add(new ModbusRegs(fileName, name, description, function, address));
                        continue;
                    }
                }
            }
            return regs;
        }

        /*   public void Replace(string path, List<ReplaceText> replaceTexts)
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
           }*/
    }
}
