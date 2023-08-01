using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EcoStruxureConfigurator
{
    public class SepGen
    {
        private static readonly string[] ROWS_NAMES = { "Сигнал", "Описание", "Тип", "Привязка", "Сегмент", "Адрес", "Номер бита", "Номер записи в файле", "Метка времени", "Размер строки", "Категория данных" };
        public static void WriteNewExcel(Settings settings, string path, List<TagModbus> tags)
        {
            FileInfo existingFile = new FileInfo(path);
            if (existingFile.Exists)
                File.Delete(path);

            ExcelPackage package = new ExcelPackage(existingFile);

            try
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("ModbusAddressMap");
                var tagsBinary = tags.FindAll(x => x.TagInfo.Type == TagInfoBase.BinaryAnalog.Binary);
                tagsBinary.Sort(delegate (TagModbus m1, TagModbus m2)
                {
                    return m1.Addr.CompareTo(m2.Addr);
                });

                var tagsAnalog = tags.FindAll(x => x.TagInfo.Type == TagInfoBase.BinaryAnalog.Analog);
                tagsAnalog.Sort(delegate (TagModbus m1, TagModbus m2)
                {
                    return m1.Addr.CompareTo(m2.Addr);
                });

                List<TagModbus> tagsModbus = new List<TagModbus>();
                tagsModbus.AddRange(tagsBinary);
                tagsModbus.AddRange(tagsAnalog);
                WriteModbusTags(settings, ws, tagsModbus);

                package.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "    Ошибка создания Excel Sep");
            }
        }

        public static void WriteModbusTags(Settings settings, ExcelWorksheet ws, List<TagModbus> tags)
        {
            CreateHeaderRow(ws);
            int indx = 2;
            for (int i = 0; i < tags.Count; i++)
            {
                if (!(tags[i].Name.ToUpper().Contains("РЕЗЕРВ")))
                {
                    string descr = tags[i].Description.Remove(0, 3);
                    string tagName = settings.SEPPrefix + "." + tags[i].SystemNameEng.Replace('-','_') + "." + tags[i].Path[1] + "." + tags[i].Path[2] + "." + descr;
                    ws.Cells[indx, 1].Value = tagName;

                    ws.Cells[indx, 2].Value = tags[i].Name;

                    string type = "";
                    if (tags[i].TagInfo.Type == TagInfoBase.BinaryAnalog.Binary)
                    {
                        type = "bool";
                    }
                    else if (tags[i].TagInfo.Type == TagInfoBase.BinaryAnalog.Analog)
                    {

                        if (tags[i].TagInfo.TypeName.Contains("INT"))
                            type = "int2";
                        else if (tags[i].TagInfo.TypeName.Contains("REAL"))
                            type = "float";
                    }
                    ws.Cells[indx, 3].Value = type;

                    ws.Cells[indx, 4].Value = "непосредственно";

                    string segment = "";
                    if (tags[i].TagInfo.Type == TagInfoBase.BinaryAnalog.Binary)
                    {
                        if (tags[i].DIR == TagModbus.ST_SP.ST)
                            segment = "Discretes Input";
                        else if (tags[i].DIR == TagModbus.ST_SP.SP)
                            segment = "Coils";
                    }
                    else if (tags[i].TagInfo.Type == TagInfoBase.BinaryAnalog.Analog)
                    {
                        if (tags[i].DIR == TagModbus.ST_SP.ST)
                            segment = "Input Registers";
                        else if (tags[i].DIR == TagModbus.ST_SP.SP)
                            segment = "Holding Registers";
                    }
                    ws.Cells[indx, 5].Value = segment;

                    ws.Cells[indx, 6].Value = tags[i].Addr-1;

                    indx++;
                }
            }
            SetStyle(ws);
        }

        private static void SetStyle(ExcelWorksheet ws)
        {
            ws.Cells.AutoFitColumns();
            ws.Column(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            ws.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            ws.Column(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            ws.Column(4).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            ws.Column(5).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
        }

        private static void CreateHeaderRow(ExcelWorksheet ws)
        {
            ws.Row(1).Style.Font.Bold = true;
            ws.Row(1).Style.Font.Size = 11;
            //
            for (int i = 0; i < ROWS_NAMES.Length; i++)
            {
                ws.Cells[1, i + 1].Value = ROWS_NAMES[i];
                ws.Cells[1, i + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                ws.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
            }
        }

    }
}

