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
    public class WeintekGen
    {
        public static void WriteNewExcel(Settings settings, string path, List<TagModbus> tags)
        {
            FileInfo existingFile = new FileInfo(path);
            if (existingFile.Exists)
                File.Delete(path);

            ExcelPackage package = new ExcelPackage(existingFile);

            try
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("метка");
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
                MessageBox.Show(ex.Message + "    Ошибка создания Excel Weintek");
            }
        }

        public static void WriteModbusTags(Settings settings, ExcelWorksheet ws, List<TagModbus> tags)
        {
            int startIndx = createConstVars(ws);
            startIndx += 1;
            for (int i = 0; i < tags.Count; i++)
            {
                ws.Cells[i + startIndx, 1].Value = tags[i].PsevdoName;
                ws.Cells[i + startIndx, 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + startIndx, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + startIndx, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;


                ws.Cells[i + startIndx, 2].Value = "MODBUS TCP/IP";
                ws.Cells[i + startIndx, 2].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + startIndx, 2].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + startIndx, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                string func = "";
                if (tags[i].TagInfo.Type == TagInfoBase.BinaryAnalog.Binary)
                {
                    if (tags[i].DIR == TagModbus.ST_SP.ST)
                        func = "1x";
                    else if (tags[i].DIR == TagModbus.ST_SP.SP)
                        func = "0x";
                }
                else if (tags[i].TagInfo.Type == TagInfoBase.BinaryAnalog.Analog)
                {
                    if (tags[i].DIR == TagModbus.ST_SP.ST)
                    {
                        if (tags[i].TagInfo.TypeName.Contains("INT"))
                            func = "4x";
                        else if (tags[i].TagInfo.TypeName.Contains("REAL"))
                            func = "4x_Double";
                    }
                    else if (tags[i].DIR == TagModbus.ST_SP.SP)
                    {
                        if (tags[i].TagInfo.TypeName.Contains("INT"))
                            func = "3x";
                        else if (tags[i].TagInfo.TypeName.Contains("REAL"))
                            func = "3x_Double";
                    }
                }
                ws.Cells[i + startIndx, 3].Value = func;
                ws.Cells[i + startIndx, 3].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + startIndx, 3].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + startIndx, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[i + startIndx, 4].Value = tags[i].Addr;
                ws.Cells[i + startIndx, 4].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + startIndx, 4].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + startIndx, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[i + startIndx, 6].Value = "Неопределенный";
                ws.Cells[i + startIndx, 6].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + startIndx, 6].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + startIndx, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                if (settings.MODBUS_ENABLE_LIGHT_REZERV && (tags[i].Name.ToUpper().Contains("РЕЗЕРВ")))  //light color in "rezerv" tags
                {
                    ws.Cells[i + startIndx, ws.Dimension.Start.Column, i + startIndx, ws.Dimension.End.Column].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[i + startIndx, ws.Dimension.Start.Column, i + startIndx, ws.Dimension.End.Column].Style.Fill.BackgroundColor.SetColor(Color.SaddleBrown); ;
                }
            }
            SetStyle(ws);
        }

        private static int createConstVars(ExcelWorksheet ws)
        {
            string[,] arrConst = {   { "Результат выполнения команды UAC : успешно",                     "Local HMI", "LW_Bit", "895100" },
                                { "Результат выполнения команды UAC : неверная команда",            "Local HMI", "LW_Bit", "895101" },
                                { "Результат выполнения команды UAC : аккаунт существует",          "Local HMI", "LW_Bit", "895102" },
                                { "Результат выполнения команды UAC : аккаунт не существет",        "Local HMI", "LW_Bit", "895103" },
                                { "Результат выполнение команды UAC : неверный пароль",             "Local HMI", "LW_Bit", "895104" },
                                { "Результат выполнения команды UAC : команда запрещена",           "Local HMI", "LW_Bit", "895105" },
                                { "Результат выполнения команды UAC : неверное имя",                "Local HMI", "LW_Bit", "895106" },
                                { "Результат выполнение команды UAC : неверный символ в пароле",    "Local HMI", "LW_Bit", "895107" },
                                { "Результат выполнения команды UAC : неверный импорт данных",      "Local HMI", "LW_Bit", "895108" },
                                { "Результат выполнения команды UAC : неверный диапазон",           "Local HMI", "LW_Bit", "895109" },
                                { "Привилегии UAC (Класс A )", "Local HMI", "LW_Bit", "895300" },
                                { "Привилегии UAC (Класс B )", "Local HMI", "LW_Bit", "895301" },
                                { "Привилегии UAC (Класс C )", "Local HMI", "LW_Bit", "895302" },
                                { "Привилегии UAC (Класс D )", "Local HMI", "LW_Bit", "895303" },
                                { "Привилегии UAC (Класс E )", "Local HMI", "LW_Bit", "895304" },
                                { "Привилегии UAC (Класс F )", "Local HMI", "LW_Bit", "895305" },
                                { "Привилегии UAC (Класс G )", "Local HMI", "LW_Bit", "895306" },
                                { "Привилегии UAC (Класс H )", "Local HMI", "LW_Bit", "895307" },
                                { "Привилегии UAC (Класс I )", "Local HMI", "LW_Bit", "895308" },
                                { "Привилегии UAC (Класс J )", "Local HMI", "LW_Bit", "895309" },
                                { "Привилегии UAC (Класс K )", "Local HMI", "LW_Bit", "895310" },
                                { "Привилегии UAC (Класс L )", "Local HMI", "LW_Bit", "895311" },
                                { "Команда UAC",                            "Local HMI", "LW", "8950" },
                                { "Результат выполнения команды UAC",       "Local HMI", "LW", "8951" },
                                { "Индекс пользователя UAC",                "Local HMI", "LW", "8952" },
                                { "Привилегии пользователя UAC",            "Local HMI", "LW", "8953" },
                                { "Имя пользователя UAC",                   "Local HMI", "LW", "8954" },
                                { "Пароль UAC",                             "Local HMI", "LW", "8962" },
                            };
            int i;
            for (i = 0; i < arrConst.Length/4; i++)
            {
                ws.Cells[i + 1, 1].Value = arrConst[i, 0];
                ws.Cells[i + 1, 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 1, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 1, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[i + 1, 2].Value = arrConst[i, 1];
                ws.Cells[i + 1, 2].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 1, 2].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 1, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[i + 1, 3].Value = arrConst[i, 2];
                ws.Cells[i + 1, 3].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 1, 3].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 1, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[i + 1, 4].Value = arrConst[i, 3];
                ws.Cells[i + 1, 4].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 1, 4].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 1, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[i + 1, 6].Value = "Неопределенный";
                ws.Cells[i + 1, 6].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 1, 6].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                ws.Cells[i + 1, 6].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            }

            return i;
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
    }
}
