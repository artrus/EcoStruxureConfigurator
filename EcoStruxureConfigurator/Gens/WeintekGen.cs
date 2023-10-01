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
        public static void WriteNewExcelTags(Settings settings, string path, List<TagModbus> tags)
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
                MessageBox.Show(ex.Message + "    Ошибка создания Excel Weintek Tags");
            }
        }

        public static void WriteModbusTags(Settings settings, ExcelWorksheet ws, List<TagModbus> tags)
        {
            int startIndx = createConstVarsInTags(ws);
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
                    ws.Cells[i + startIndx, ws.Dimension.Start.Column, i + startIndx, ws.Dimension.End.Column].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                }
            }
            SetStyle(ws);
        }

        private static int createConstVarsInTags(ExcelWorksheet ws)
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
            for (i = 0; i < arrConst.Length / 4; i++)
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

        public static void WriteNewExcelAlarms(Settings settings, string path, List<TagModbus> tags)
        {
            FileInfo existingFile = new FileInfo(path);
            if (existingFile.Exists)
                File.Delete(path);

            ExcelPackage package = new ExcelPackage(existingFile);

            try
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Event");
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
                WriteAlarms(settings, ws, tagsModbus);

                package.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "    Ошибка создания Excel Weintek Alarms");
            }
        }

        public static void WriteAlarms(Settings settings, ExcelWorksheet ws, List<TagModbus> tags)
        {
            int startIndx = createConstVarsInAlarms(ws);
            string[] s = { "0: Category 0", "Low", "Bit", "MODBUS TCP/IP", "!TagName!", "False", "True", "", "null", " ", "False", "False", "", "", "False", "False", "", "null", "bt: 1", "0", "!Descr!", "False", "Режимы", "Droid Sans Fallback", "!Color!", "11", "False", "", "0", "0", "", "", "False", "False", "", "null", "", "", "", "", "False", "False", "", "null", "", "", "", "", "False", "False", "", "null", "", "", "", "", "False", "False", "", "null", "", "", "", "", "False", "False", "", "null", "", "", "", "", "False", "False", "", "null", "", "", "", "", "False", "False", "", "null", "", "", "", "", "False", "False", "", "null", "", "", "False", "10", "False", "False", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "5", "0", "", "", "False", "True", "", "null", "True", "False", "Local HMI", "LW", "False", "False", "0", "null", "16-bit Unsigned", "", "", "False", "False", "", "", "", "", "False", "False", "", "null", "", "", "False", "False", "", "", "False", "False", "", "null", "", "False", "" };
            int indxTagName = 4;
            int indxDescr = 20;
            int indxColor = 24;
            startIndx += 1;
            for (int i = 0; i < tags.Count; i++)
            {
                if (tags[i].TagAlarm == null)
                    continue;
                for (int j = 0; j < 167; j++)
                    ws.Cells[startIndx, j + 1].Value = s[j];

                ws.Cells[startIndx, indxTagName + 1].Value = tags[i].PsevdoName;
                ws.Cells[startIndx, indxDescr + 1].Value = tags[i].TagAlarm.Description;
                ws.Cells[startIndx, indxColor + 1].Value = tags[i].TagAlarm.GetColor();
                startIndx++;
            }
        }

        private static int createConstVarsInAlarms(ExcelWorksheet ws)
        {
            string[,] arrConst = {      { "VERSION", "2", "HARDWARE_VERSION", "61", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
                                        { "Категория", "Приоритет", "Тип адреса", "Имя ПЛК (Чтение)", "Тип устройства (Чтение)", "Системный тег (Чтение)", "Тег определенный пользователем (Чтение)", "Адрес (Чтение)", "Индекс (Чтение)", "Формат данных (Чтение)", "Включить уведомление", "Включить (Уведомление)", "Имя ПЛК (Уведомление)", "Тип устройства (уведомление)", "Системный тег (Уведомление)", "Тег определенный пользователем (Уведомление)", "Адрес (Уведомление)", "Индекс (Уведомление)", "Условие", "Значение триггера", "Содержание", "Использовать библиотеку меток", "Имя метки", "Шрифт", "Цвет", "Подтвержденное значение", "Включить звук", "Имя библиотеки звуков", "Индекс звука", "Номер наблюдателя", "Имя ПЛК (Наблюдатель 1)", "Тип устройства (Наблюдатель 1)", "Системный тег (Наблюдатель 1)", "Тег определенный пользователем (Наблюдатель 1)", "Адрес (Наблюдатель 1)", "Индекс (Наблюдатель 1)", "Формат данных (Наблюдатель 1)", "Номер слова (Наблюдатель 1)", "Имя ПЛК (наблюдатель 2)", "Тип устройства (Наблюдатель 2)", "Системный тег (Наблюдатель 2)", "Тег определенный пользователем (Наблюдатель 2)", "Адрес (Наблюдатель 2)", "Индекс (Наблюдатель 2)", "Формат данных (Наблюдатель 2)", "Номер слова (Наблюдатель 2)", "Имя ПЛК (Наблюдатель 3)", "Тип устройства (Наблюдатель 3)", "Системный тег (Наблюдатель 3)", "Тег определенный пользователем (Наблюдатель 3)", "Адрес (Наблюдатель 3)", "Индекс (Наблюдатель 3)", "Формат данных (Наблюдатель 3)", "Номер слова (Наблюдатель 3)", "Имя ПЛК (Наблюдатель 4)", "Тип устройства (Наблюдатель 4)", "Системный тег (наблюдатель 4)", "Тег определенный пользователем (Наблюдатель 4)", "Адрес (Наблюдатель 4)", "Индекс (Наблюдатель 4)", "Формат данных (Наблюдатель 4)", "Номер слова (наблюдатель 4)", "Имя ПЛК (Наблюдатель 5)", "Тип устройства (Наблюдатель 5)", "Системны тег (Наблюдатель 5)", "Тег определенный пользователем (Наблюдатель 5)", "Адрес (Наблюдатель 5)", "Индекс (Наблюдатель 5)", "Формат данных (Наблюдатель 5)", "Номер слова (Наблюдатель 5)", "Имя ПЛК (Наблюдатель 6)", "Тип устройства (Наблюдатель 6)", "Системный тег (Наблюдатель 6)", "Тег определенный пользователем (Наблюдатель 6)", "Адрес (Наблюдатель 6)", "Индекс (Наблюдатель 6)", "Формат данных (Наблюдатель 6)", "Номер слова (Наблюдатель 6)", "Имя ПЛК (Наблюдатель 7)", "Тип устройства (Наблюдатель 7)", "Системный тег (Наблюдатель 7)", "Тег определенный пользователем (Наблюдатель 7)", "Адрес (Наблюдатель 7)", "Индекс (Наблюдатель 7)", "Формат данных (Наблюдатель 7)", "Номер слова (Наблюдатель 7)", "Имя ПЛК (Наблюдатель 8)", "Тип устройства (Наблюдатель 8)", "Системный тег (Наблюдатель 8)", "Тег определенный пользователем (Наблюдатель 8)", "Адрес (Наблюдатель 8)", "Индекс (Наблюдатель 8)", "Формат данных (Наблюдатель 8)", "Номер слова (Наблюдатель 8)", "Непрерывный звук", "Интервал времени звука", "Отправить eMail при появлении события", "Отправить eMail при исчезновении события", "Получатели (Сработало)", "Получатели копий (Сработало)", "Получатели скрытой копии (Сработало)", "Тема как событие (Сработало)", "Тема (Сработало)", "Использовать библиотеку меток (Тема)", "Имя метки (Тема)", "Открытие (Сработало)", "Использовать библиотеку меток (Открытие)", "Имя метки (Открытие)", "Окончание (Сработало)", "Использовать библиотеку меток (Окончание)", "Имя метки (Окончание)", "Скриншот окна", "Получатели (Очищено)", "Получатели копии (Очищенно)", "Получатели скрытой копии (Очищенно)", "Тема как событие (Очищено)", "Тема (Очищено)", "Использовать библиотеку меток (Тема)", "Имя метки (Тема)", "Открытие (Очищено)", "Использовать библиотеку меток (Открытие)", "Имя метки (Открытие)", "Окончание (Очищено)", "Использовать библиотеку меток (Окончание)", "Имя метки (Окончание)", "Время задержки", "Динамическое условие", "Имя ПЛК (условие)", "Тип устройства (Условие)", "Системный тег (Условие)", "Тег определенный пользователем (условие)", "Адрес (Условие)", "Индекс (Условие)", "Сохранить в истории", "Местонахождение", "Имя ПЛК (Местонахождение)", "Тип устройства (Местонахождение)", "Системный тег (Местонахождения)", "Тег определенный пользователем (Местонахождение)", "Адрес (Местонахождение)", "Индекс (Местонахождение)", "Формат данных (Местонахождение)", "В допуске", "Вне допуска", "Следить", "Использовать таблицу строк", "ID секции", "Динамически", "ID строки", "Имя ПЛК (ID строки)", "Тип устройства (ID строки)", "Системный тег (ID строки)", "Тег определенный пользователем (ID строки)", "Адрес (ID строки)", "Индекс (ID строки)", "Формат данных (ID строки)", "Отправить уведомление", "Пройденное время", "Имя ПЛК (Пройденное время)", "Тип устройства (Пройденное время)", "Системный тег (Пройденное время)", "Тег определенный пользователем (Пройденное время)", "Адрес (Пройденное время)", "Индекс (Пройденное время)", "Формат данных (Пройденное время)", "Цвет фона", "Цвет (Цвет фона)" },
                                        { "0: Category 0", "Low", "Bit", "Local HMI", "LB-9153 : авто. соединение для устройства 4 (ethernet) (когда включено)", "True", "False", "LB-9153", "null", " ", "False", "False", "", "", "False", "False", "", "null", "bt: 1", "0", "Нет связи с ПЛК", "False", "Режимы", "Droid Sans Fallback", "255:0:0", "11", "False", "", "0", "0", "", "", "False", "False", "", "null", "", "", "", "", "False", "False", "", "null", "", "", "", "", "False", "False", "", "null", "", "", "", "", "False", "False", "", "null", "", "", "", "", "False", "False", "", "null", "", "", "", "", "False", "False", "", "null", "", "", "", "", "False", "False", "", "null", "", "", "", "", "False", "False", "", "null", "", "", "False", "10", "False", "False", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "5", "0", "Local HMI", "LB-9153 : авто. соединение для устройства 4 (ethernet) (когда включено)", "True", "False", "LB-9153", "null", "True", "False", "Local HMI", "LW", "False", "False", "0", "null", "16-bit Unsigned", "", "", "False", "False", "", "", "", "", "False", "False", "", "null", "", "", "False", "False", "", "", "False", "False", "", "null", "", "False", "" },
                            };
            int i;
            for (i = 0; i < arrConst.Length / 167; i++)
            {
                for (int j = 0; j < 167; j++)
                {
                    ws.Cells[i + 1, j + 1].Value = arrConst[i, j];
                }
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
