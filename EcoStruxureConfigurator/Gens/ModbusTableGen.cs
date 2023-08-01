using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoStruxureConfigurator
{
    public class ModbusTableGen
    {
        private static readonly string[] ROWS_NAMES = { "Тип", "Наименование", "Описание", "Система", "Адрес" };

        public static void WriteNewExcel(Settings settings, string path, List<TagModbus> tags)
        {
            FileInfo existingFile = new FileInfo(path);
            if (existingFile.Exists)
                File.Delete(path);

            ExcelPackage package = new ExcelPackage(existingFile);

            try
            {
                ExcelWorksheet wsBinary = package.Workbook.Worksheets.Add(settings.NAME_LIST_MODBUS_BINARY);
                var tagsBinary = tags.FindAll(x => x.TagInfo.Type == TagInfoBase.BinaryAnalog.Binary);
                tagsBinary.Sort(delegate (TagModbus m1, TagModbus m2)
                                                                    {
                                                                        return m1.Addr.CompareTo(m2.Addr);
                                                                    });
                WriteModbusTags(settings, wsBinary, tagsBinary);


                ExcelWorksheet wsAnalog = package.Workbook.Worksheets.Add(settings.NAME_LIST_MODBUS_ANALOG);
                var tagsAnalog = tags.FindAll(x => x.TagInfo.Type == TagInfoBase.BinaryAnalog.Analog);
                tagsAnalog.Sort(delegate (TagModbus m1, TagModbus m2)
                {
                    return m1.Addr.CompareTo(m2.Addr);
                });
                WriteModbusTags(settings, wsAnalog, tagsAnalog);

                package.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "    Ошибка создания Excel Modbus");
            }
        }

        public static void WriteModbusTags(Settings settings, ExcelWorksheet ws, List<TagModbus> tags)
        {
            CreateHeaderRow(ws);

            for (int i = 0; i < tags.Count; i++)
            {
                ws.Cells[i + 2, settings.ROW_MODBUS_TYPE].Value = tags[i].TagInfo.Type == TagInfoIO.BinaryAnalog.Binary ? "Цифровое значение" : "Аналоговое значение";
                ws.Cells[i + 2, settings.ROW_MODBUS_TYPE].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 2, settings.ROW_MODBUS_TYPE].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 2, settings.ROW_MODBUS_TYPE].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;


                ws.Cells[i + 2, settings.ROW_MODBUS_NAME].Value = tags[i].Name;
                ws.Cells[i + 2, settings.ROW_MODBUS_NAME].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 2, settings.ROW_MODBUS_NAME].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 2, settings.ROW_MODBUS_NAME].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[i + 2, settings.ROW_MODBUS_DESCR].Value = tags[i].Description;
                ws.Cells[i + 2, settings.ROW_MODBUS_DESCR].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 2, settings.ROW_MODBUS_DESCR].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 2, settings.ROW_MODBUS_DESCR].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[i + 2, settings.ROW_MODBUS_SYSTEM].Value = tags[i].SystemNameRus;
                ws.Cells[i + 2, settings.ROW_MODBUS_SYSTEM].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 2, settings.ROW_MODBUS_SYSTEM].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 2, settings.ROW_MODBUS_SYSTEM].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[i + 2, settings.ROW_MODBUS_ADDR].Value = tags[i].Addr;
                ws.Cells[i + 2, settings.ROW_MODBUS_ADDR].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 2, settings.ROW_MODBUS_ADDR].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 2, settings.ROW_MODBUS_ADDR].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                if (settings.MODBUS_ENABLE_LIGHT_REZERV && (tags[i].Name.ToUpper().Contains("РЕЗЕРВ")))  //light color in "rezerv" tags
                {
                    ws.Cells[i + 2, ws.Dimension.Start.Column, i + 2, ws.Dimension.End.Column].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[i + 2, ws.Dimension.Start.Column, i + 2, ws.Dimension.End.Column].Style.Fill.BackgroundColor.SetColor(Color.SaddleBrown); ;
                }
            }
            SetStyle(ws);

        }

        private static void CreateHeaderRow(ExcelWorksheet ws)
        {
            ws.Row(1).Style.Font.Bold = true;
            ws.Row(1).Style.Font.Size = 12;
            //
            for (int i = 0; i < ROWS_NAMES.Length; i++)
            {
                ws.Cells[1, i + 1].Value = ROWS_NAMES[i];
                ws.Cells[1, i + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thick);
                ws.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
            }
        }

        private static void SetStyle(ExcelWorksheet ws)
        {
            ws.Cells.AutoFitColumns();
            int widthZoom = 20;
            ws.Column(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            ws.Column(1).Width = ws.Column(1).Width + widthZoom;
            ws.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            ws.Column(2).Width = ws.Column(2).Width + widthZoom;
            ws.Column(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            ws.Column(3).Width = ws.Column(3).Width + widthZoom;
            ws.Column(4).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            ws.Column(4).Width = ws.Column(4).Width + widthZoom;
            ws.Column(5).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            ws.Column(5).Width = ws.Column(5).Width + widthZoom;
        }

    }
}
