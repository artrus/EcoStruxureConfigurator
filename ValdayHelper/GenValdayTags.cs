using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValdayHelper
{
    public class GenValdayTags
    {
        public static void WriteNewExcel(Settings settings, string path, List<Tag> tags, List<ValdayClass> classes)
        {
            FileInfo existingFile = new FileInfo(path);
            if (existingFile.Exists)
                File.Delete(path);

            ExcelPackage package = new ExcelPackage(existingFile);

            try
            {
                ExcelWorksheet wsTags = package.Workbook.Worksheets.Add("Tags");
                WriteTags(settings, wsTags, tags, classes);

                ExcelWorksheet wsModbus = package.Workbook.Worksheets.Add("Modbus");
                WriteModbus(settings, wsModbus, tags, classes);

                ExcelWorksheet wsAlarms = package.Workbook.Worksheets.Add("Alarms");
                WriteAlarms(settings, wsAlarms, tags, classes);

                ExcelWorksheet wsTrends = package.Workbook.Worksheets.Add("Trends");
                WriteTrends(settings, wsTrends, tags, classes);

                package.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "    Ошибка создания Excel Tags");
            }
        }

        public static void WriteTags(Settings settings, ExcelWorksheet ws, List<Tag> tags, List<ValdayClass> classes)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                ws.Cells[i + 2, 1].Value = tags[i].Name;
                ws.Cells[i + 2, 2].Value = "0";
                ws.Cells[i + 2, 3].Value = tags[i].Type;
                ws.Cells[i + 2, 4].Value = settings.Prefix + tags[i].Description;
            }
            SetStyle(ws);
        }

        public static void WriteAlarms(Settings settings, ExcelWorksheet ws, List<Tag> tags, List<ValdayClass> classes)
        {
            int i = 1;
            foreach (Tag tag in tags)
            {
                if (tag.Type.Contains("cQF"))
                {
                    ws.Cells[i, 1].Value = tag.Name + ".Dstb";
                    ws.Cells[i, 2].Value = "Hi";
                    ws.Cells[i, 3].Value = "0.50";

                    string message = settings.Prefix + "Автомат {StrGetElement(";
                    message += tag.Name;
                    message += ".Text, \":\", 1)} ({StrGetElement(";
                    message += tag.Name;
                    message += ".Text, \":\", 2)}) отключен";
                    ws.Cells[i, 4].Value = message;
                    i++;
                }

                else if (tag.Type.Contains("cBI"))
                {
                    ws.Cells[i, 1].Value = tag.Name + ".Dstb";
                    ws.Cells[i, 2].Value = "Hi";
                    ws.Cells[i, 3].Value = "0.50";
                    ws.Cells[i, 4].Value = settings.Prefix + tag.Description;
                    i++;
                }

                else if (tag.Type.Contains("cBOFB"))
                {
                    ws.Cells[i, 1].Value = tag.Name + ".Dstb";
                    ws.Cells[i, 2].Value = "Hi";
                    ws.Cells[i, 3].Value = "0.50";
                    ws.Cells[i, 4].Value = settings.Prefix + "Авария управления " + tag.Description;
                    i++;
                }
                else if (tag.Type.Contains("cAI"))
                {
                    ws.Cells[i, 1].Value = tag.Name + ".Dstb";
                    ws.Cells[i, 2].Value = "Hi";
                    ws.Cells[i, 3].Value = "0.50";

                    string message = settings.Prefix + "Датчик ";
                    message += "\"" + tag.Description + "\"";
                    message += " {if(";
                    message += ws.Cells[i, 1].Value = tag.Name;
                    message += ".Rlb = 1,\"КЗ\",\"\")}{if(";
                    message += ws.Cells[i, 1].Value = tag.Name;
                    message += ".Rlb = 2,\"обрыв цепи\",\"\")}{if(";
                    message += ws.Cells[i, 1].Value = tag.Name;
                    message += ".Rlb = 3,\"ниже нормы\",\"\")}{if(";
                    message += ws.Cells[i, 1].Value = tag.Name;
                    message += ".Rlb=4,\"выше нормы\",\"\")}{if(";
                    message += ws.Cells[i, 1].Value = tag.Name;
                    message += ".Rlb=0,\"в норме\",\"\")}";
                    ws.Cells[i, 4].Value = message;
                    i++;
                }
                else if (tag.Type.Contains("cOZKValve"))
                {
                    ws.Cells[i, 1].Value = tag.Name + ".Jammed";
                    ws.Cells[i, 2].Value = "Hi";
                    ws.Cells[i, 3].Value = "0.50";
                    ws.Cells[i, 4].Value = settings.Prefix + tag.Description + " заклинил";
                    i++;
                    ws.Cells[i, 1].Value = tag.Name + ".FaultOpened";
                    ws.Cells[i, 2].Value = "Hi";
                    ws.Cells[i, 3].Value = "0.50";
                    ws.Cells[i, 4].Value = settings.Prefix + tag.Description + " авария концевика \"открыт\"";
                    i++;
                    ws.Cells[i, 1].Value = tag.Name + ".FaultClosed";
                    ws.Cells[i, 2].Value = "Hi";
                    ws.Cells[i, 3].Value = "0.50";
                    ws.Cells[i, 4].Value = settings.Prefix + tag.Description + " авария концевика \"закрыт\"";
                    i++;
                }
            }
            SetStyle(ws);
        }

        public static void WriteTrends(Settings settings, ExcelWorksheet ws, List<Tag> tags, List<ValdayClass> classes)
        {
            int i = 1;
            foreach (Tag tag in tags)
            {
                if (tag.Type.Contains("cAI"))
                {
                    ws.Cells[i, 1].Value = tag.Name + ".PrVal";
                    i++;
                }

            }
            SetStyle(ws);
        }


        public static void WriteModbus(Settings settings, ExcelWorksheet ws, List<Tag> tags, List<ValdayClass> classes)
        {
            int i = 2;
            foreach (Tag tag in tags)
            {
                if (classes.FindAll(x => x.Name == tag.Type).Any())
                {
                    ValdayClass valdayClass = classes.Find(x => x.Name == tag.Type);
                    List<TagInValdayClass> tagInValdayClass = valdayClass.GetTags();
                    foreach (var subTag in tagInValdayClass)
                    {
                        ws.Cells[i, 1].Value = tag.Name + "." + subTag.Name;
                        ws.Cells[i, 2].Value = settings.Station;
                        if (subTag.Type == TagInValdayClass.TagType.Status)
                        {
                            ws.Cells[i, 3].Value = "3X:" + (subTag.Register + tag.Addr3X + 1);
                            ws.Cells[i, 4].Value = "Чтение";
                        }
                        else if (subTag.Type == TagInValdayClass.TagType.StatusBit)
                        {
                            ws.Cells[i, 3].Value = "3X:" + (subTag.Register + tag.Addr3X + 1) + "." + subTag.Bit;
                            ws.Cells[i, 4].Value = "Чтение";
                        }
                        else if (subTag.Type == TagInValdayClass.TagType.Holding)
                        {
                            ws.Cells[i, 3].Value = "4X:" + (subTag.Register + tag.Addr4X + 1);
                            ws.Cells[i, 4].Value = "Чтение+Запись";
                        }
                        else if (subTag.Type == TagInValdayClass.TagType.HoldingBit)
                        {
                            ws.Cells[i, 3].Value = "4X:" + (subTag.Register + tag.Addr4X + 1) + "." + subTag.Bit;
                            ws.Cells[i, 4].Value = "Чтение+Запись";
                        }
                        i++;
                    }
                }
                else if (tag.Type.Contains("WORD"))
                {
                    ws.Cells[i, 1].Value = tag.Name;
                    ws.Cells[i, 2].Value = settings.Station;
                    if (tag.Addr3X >= 0)
                    {
                        ws.Cells[i, 3].Value = "3X:" + (tag.Addr3X + 1);
                        ws.Cells[i, 4].Value = "Чтение";
                    }
                    else if (tag.Addr4X >= 0)
                    {
                        ws.Cells[i, 3].Value = "4X:" + (tag.Addr4X + 1);
                        ws.Cells[i, 4].Value = "Чтение+Запись";
                    }
                    i++;
                }

            }
            SetStyle(ws);
        }

        private static void SetStyle(ExcelWorksheet ws)
        {
            ws.Cells.AutoFitColumns();
            int widthZoom = 20;
            ws.Column(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            ws.Column(1).Width = ws.Column(1).Width + widthZoom;
            ws.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            ws.Column(2).Width = ws.Column(2).Width + 2;
            ws.Column(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            ws.Column(3).Width = ws.Column(3).Width + widthZoom;
            ws.Column(4).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            ws.Column(4).Width = ws.Column(4).Width + widthZoom;
            ws.Column(5).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            ws.Column(5).Width = ws.Column(5).Width + widthZoom;
        }
    }

}
