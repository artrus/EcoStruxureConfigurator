using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            double d = 45137.523826;
              var  DT =DateTime.FromOADate(d);
     
        }


        // Проверка на несколько измерений с одной датой
        private bool CheakDoubleDateInTERH(List<TERHMeasure> measures, TERHMeasure measure)
        {
            if ((measures.FindAll(x => (x.DT.Day == measure.DT.Day)
                                        && (x.DT.Month == measure.DT.Month)
                                        && (x.DT.Year == measure.DT.Year))).Count() > 1)
            {
                return true;
            }
            return false;
        }


        private bool CheakDoubleDateInPower(List<EnergyMeasure> measures, EnergyMeasure measure)
        {
            if ((measures.FindAll(x => (x.DT.Day == measure.DT.Day)
                                        && (x.DT.Month == measure.DT.Month)
                                        && (x.DT.Year == measure.DT.Year))).Count() > 1)
            {
                return true;
            }
            return false;
        }


        private List<CSVStroke> GetInitStrokes(int yearStart, int monthStart, int dayStart, int yearEnd, int monthEnd, int dayEnd)
        {
            //Забиваем датой массив
            var dateOne = new DateTime(yearStart, monthStart, dayStart).Date;
            var dateTwo = new DateTime(yearEnd, monthEnd, dayEnd).Date;
            var datesBetween = Enumerable
                .Range(1, (int)(dateTwo - dateOne).TotalDays - 1)
                .Select(diff => dateOne.AddDays(diff))
                .ToArray();

            List<CSVStroke> strokes = new List<CSVStroke>();
            foreach (var date in datesBetween)
            {
                string dt = date.Day.ToString() + "." + date.Month.ToString();
                strokes.Add(new CSVStroke(date, dt));
            }

            return strokes;

        }


        private void AddTERHStrokes(List<CSVStroke> strokes)
        {
            //Разбор архивов тэгов
            List<TERHMeasure> measuresTERH;
            measuresTERH = GetTE(@"d:\Projects\Контейнер\Logs\Input\PLC_Box1_ti_pvt3_temp.csv");
            foreach (var measure in measuresTERH)
            {
                if (CheakDoubleDateInTERH(measuresTERH, measure))
                {
                    MessageBox.Show("В функции Ext TE получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Ext_TE_Min = measure.MinValue.ToString();
                    stroke.Ext_TE_Max = measure.MaxValue.ToString();
                    stroke.Ext_TE_Avg = measure.AvgValue.ToString();
                }
            }

            measuresTERH = GetTE(@"d:\Projects\Контейнер\Logs\Input\PLC_Box1_ti_pvt3_hum.csv");
            foreach (var measure in measuresTERH)
            {
                if (CheakDoubleDateInTERH(measuresTERH, measure))
                {
                    MessageBox.Show("В функции Ext RH получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Ext_RH_Min = measure.MinValue.ToString();
                    stroke.Ext_RH_Max = measure.MaxValue.ToString();
                    stroke.Ext_RH_Avg = measure.AvgValue.ToString();
                }
            }

            //Разбор архивов тэгов 1 БОКСА
            measuresTERH = GetTE(@"d:\Projects\Контейнер\Logs\Input\PLC_Box1__boksTE.csv");
            foreach (var measure in measuresTERH)
            {
                if (CheakDoubleDateInTERH(measuresTERH, measure))
                {
                    MessageBox.Show("В функции Box1_TE получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Box1_TE_Min = measure.MinValue.ToString();
                    stroke.Box1_TE_Max = measure.MaxValue.ToString();
                    stroke.Box1_TE_Avg = measure.AvgValue.ToString();
                }
            }

            measuresTERH = GetTE(@"d:\Projects\Контейнер\Logs\Input\PLC_Box1__boksRH.csv");
            foreach (var measure in measuresTERH)
            {
                if (CheakDoubleDateInTERH(measuresTERH, measure))
                {
                    MessageBox.Show("В функции Box1_RH получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Box1_RH_Min = measure.MinValue.ToString();
                    stroke.Box1_RH_Max = measure.MaxValue.ToString();
                    stroke.Box1_RH_Avg = measure.AvgValue.ToString();
                }
            }

            //Разбор архивов тэгов 2 БОКСА
            measuresTERH = GetTE(@"d:\Projects\Контейнер\Logs\Input\PLC_Box2__boksTE.csv");
            foreach (var measure in measuresTERH)
            {
                if (CheakDoubleDateInTERH(measuresTERH, measure))
                {
                    MessageBox.Show("В функции Box2_TE получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Box2_TE_Min = measure.MinValue.ToString();
                    stroke.Box2_TE_Max = measure.MaxValue.ToString();
                    stroke.Box2_TE_Avg = measure.AvgValue.ToString();
                }
            }

            measuresTERH = GetTE(@"d:\Projects\Контейнер\Logs\Input\PLC_Box2__boksRH.csv");
            foreach (var measure in measuresTERH)
            {
                if (CheakDoubleDateInTERH(measuresTERH, measure))
                {
                    MessageBox.Show("В функции Box2_RH получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Box2_RH_Min = measure.MinValue.ToString();
                    stroke.Box2_RH_Max = measure.MaxValue.ToString();
                    stroke.Box2_RH_Avg = measure.AvgValue.ToString();
                }
            }

        }


        //Запись в отчет
        private void SaveReport(string filePath, List<CSVStroke> strokes)
        {

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";",
            };

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(strokes);
            }
        }


        private List<EnergyMeasure> GetEnergy(string filePath)
        {
            List<EnergyMeasure> energyMeasureList = new List<EnergyMeasure>();
            List<CSVValue> powerList = OpenHistory(filePath);

            List<DateTime> dtlist = new List<DateTime>();
            dtlist.Add(powerList[0].DT);
            foreach (var power in powerList)
            {
                if (dtlist.FindAll(x => (x.Day == power.DT.Day) && (x.Month == power.DT.Month) && (x.Year == power.DT.Year)).Count == 0)
                    dtlist.Add(power.DT);
            }

            DateTime curDay = dtlist[0];
            //List<CSVValue> powerListInDay = powerList.FindAll(x => (x.DT.Day == curDay.Day) && (x.DT.Month == curDay.Month) && (x.DT.Year == curDay.Year));
            List<CSVValue> powerListInDay;
            foreach (var dt in dtlist)
            { 
                double Wh = 0;
                curDay = dt;
                powerListInDay = powerList.FindAll(x => (x.DT.Day == curDay.Day) && (x.DT.Month == curDay.Month) && (x.DT.Year == curDay.Year));
                DateTime curTime = powerListInDay[1].DT;
                DateTime lastTime = powerListInDay[0].DT;
                foreach (CSVValue power in powerListInDay)
                {
                    curTime = power.DT;
                    double k = ((curTime - lastTime).TotalMilliseconds / 3600000000);
                    Wh = Wh + power.value * k;
                    lastTime = curTime;

                }
                if ((Wh < 1) && (Wh > -1))
                    Wh = Math.Round(Wh, 5);
                else
                    Wh = Math.Round(Wh, 1);
                energyMeasureList.Add(new EnergyMeasure(curDay, Wh));
                
            }

            return energyMeasureList;
        }


        private List<TERHMeasure> GetTE(string file)
        {
            List<CSVValue> values = OpenHistory(file);

            List<TERHMeasure> measures = new List<TERHMeasure>();
            bool newDay;
            DateTime lastDay = values[0].DT, curDay;

            List<double> avgValues = new List<double>();
            double avgValue;
            double minValue = 100;
            double maxValue = 0;
            foreach (var value in values)
            {
                curDay = value.DT;
                if (measures.Count == 0)
                {
                    measures.Add(new TERHMeasure(value.DT));
                    newDay = false;
                    continue;
                }

                newDay = Math.Abs((curDay.Day - lastDay.Day)) >= 1;

                if (newDay)
                {
                    double totalSum = 0;
                    foreach (var val in avgValues)
                    {
                        totalSum += val;
                    }
                    avgValue = totalSum / avgValues.Count;
                    measures[measures.Count - 1].AvgValue = Math.Round(avgValue, 1);
                    measures[measures.Count - 1].MinValue = Math.Round(minValue, 1);
                    measures[measures.Count - 1].MaxValue = Math.Round(maxValue, 1);
                    measures.Add(new TERHMeasure(value.DT));
                    avgValue = 0;
                    minValue = 100;
                    maxValue = 0;
                    newDay = false;
                }

                avgValues.Add(value.value);
                if (value.value < minValue)
                    minValue = value.value;
                if (value.value > maxValue)
                    maxValue = value.value;

                lastDay = value.DT;
            }

            return measures;
        }

        private List<CSVValue> OpenHistory(string path)
        {
            FileInfo existingFile = new FileInfo(path);
            if (!existingFile.Exists)
            {
                MessageBox.Show("Фаил " + path + " не найден");
                return new List<CSVValue>();
            }

            try
            {
                return GetValues(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "    Ошибка парсинга " + path);
                return null;
            }
        }

        public class TempVal
        {
            public string DateTime { get; set; }
            public string Value { get; set; }
        }

        private List<CSVValue> GetValues(string pathCsvFile)
        {
            List<CSVValue> values = new List<CSVValue>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ";",
            };

            using (var reader = new StreamReader(pathCsvFile))
            {

                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<TempVal>();
                    foreach (var record in records)
                    {
                        try
                        {
                            CSVValue value = new CSVValue();
                            value.SetValueFromString(record.Value);
                            value.SetDateTimeFromOADate(record.DateTime);
                            values.Add(value);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }

                    }
                }
            }
            return values;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<CSVStroke> CSVStrokes = GetInitStrokes(2023, 02, 1, 2023, 10, 30);

            AddTERHStrokes(CSVStrokes);
            AddPowerStrokes(CSVStrokes);

            SaveReport(@"d:\Projects\Контейнер\Logs\Output\reportSummary.csv", CSVStrokes);
            Process.Start(@"d:\Projects\Контейнер\Logs\Output\reportSummary.csv");
        }

        private void AddPowerStrokes(List<CSVStroke> strokes)
        {
            List<EnergyMeasure> measuresPower;

            measuresPower = GetEnergy(@"d:\Projects\Контейнер\Logs\Input\WB1_Ch1_Total_P.csv");
            foreach (var measure in measuresPower)
            {
                if (CheakDoubleDateInPower(measuresPower, measure))
                {
                    MessageBox.Show("В функции получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Input = measure.Energy.ToString();
                }
            }
            measuresPower = GetEnergy(@"d:\Projects\Контейнер\Logs\Input\WB1_Ch2_Total_P.csv");
            foreach (var measure in measuresPower)
            {
                if (CheakDoubleDateInPower(measuresPower, measure))
                {
                    MessageBox.Show("В функции получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Solar = measure.Energy.ToString();
                }
            }
            measuresPower = GetEnergy(@"d:\Projects\Контейнер\Logs\Input\WB1_Ch3_Total_P.csv");
            foreach (var measure in measuresPower)
            {
                if (CheakDoubleDateInPower(measuresPower, measure))
                {
                    MessageBox.Show("В функции получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.WaterGen = measure.Energy.ToString();
                }
            }

            measuresPower = GetEnergy(@"d:\Projects\Контейнер\Logs\Input\WB2_Ch1_Total_P.csv");
            foreach (var measure in measuresPower)
            {
                if (CheakDoubleDateInPower(measuresPower, measure))
                {
                    MessageBox.Show("В функции получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Box1_LampMain = measure.Energy.ToString();
                }
            }
            measuresPower = GetEnergy(@"d:\Projects\Контейнер\Logs\Input\WB2_Ch2_Total_P.csv");
            foreach (var measure in measuresPower)
            {
                if (CheakDoubleDateInPower(measuresPower, measure))
                {
                    MessageBox.Show("В функции получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Box2_LampMain = measure.Energy.ToString();
                }
            }

            measuresPower = GetEnergy(@"d:\Projects\Контейнер\Logs\Input\WB2_Ch3_P_L1.csv");
            foreach (var measure in measuresPower)
            {
                if (CheakDoubleDateInPower(measuresPower, measure))
                {
                    MessageBox.Show("В функции получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Box1_LampSub = measure.Energy.ToString();
                }
            }
            measuresPower = GetEnergy(@"d:\Projects\Контейнер\Logs\Input\WB2_Ch3_P_L2.csv");
            foreach (var measure in measuresPower)
            {
                if (CheakDoubleDateInPower(measuresPower, measure))
                {
                    MessageBox.Show("В функции получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Box2_LampSub = measure.Energy.ToString();
                }
            }

            measuresPower = GetEnergy(@"d:\Projects\Контейнер\Logs\Input\WB2_Ch3_P_L3.csv");
            foreach (var measure in measuresPower)
            {
                if (CheakDoubleDateInPower(measuresPower, measure))
                {
                    MessageBox.Show("В функции получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Box1_Humi = measure.Energy.ToString();
                }
            }
            measuresPower = GetEnergy(@"d:\Projects\Контейнер\Logs\Input\WB2_Ch4_P_L1.csv");
            foreach (var measure in measuresPower)
            {
                if (CheakDoubleDateInPower(measuresPower, measure))
                {
                    MessageBox.Show("В функции получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Box2_Humi = measure.Energy.ToString();
                }
            }

            measuresPower = GetEnergy(@"d:\Projects\Контейнер\Logs\Input\WB2_Ch4_P_L2.csv");
            foreach (var measure in measuresPower)
            {
                if (CheakDoubleDateInPower(measuresPower, measure))
                {
                    MessageBox.Show("В функции получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Box1_Pump = measure.Energy.ToString();
                }
            }
            measuresPower = GetEnergy(@"d:\Projects\Контейнер\Logs\Input\WB2_Ch4_P_L3.csv");
            foreach (var measure in measuresPower)
            {
                if (CheakDoubleDateInPower(measuresPower, measure))
                {
                    MessageBox.Show("В функции получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Box2_Pump = measure.Energy.ToString();
                }
            }

            measuresPower = GetEnergy(@"d:\Projects\Контейнер\Logs\Input\WB3_Ch1_P_L1_Conditioner1.csv");
            foreach (var measure in measuresPower)
            {
                if (CheakDoubleDateInPower(measuresPower, measure))
                {
                    MessageBox.Show("В функции получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Box1_Cond = measure.Energy.ToString();
                }
            }
            measuresPower = GetEnergy(@"d:\Projects\Контейнер\Logs\Input\WB3_Ch1_P_L2_Conditioner2.csv");
            foreach (var measure in measuresPower)
            {
                if (CheakDoubleDateInPower(measuresPower, measure))
                {
                    MessageBox.Show("В функции получилось больше 1 элементов с одной датой!", "Ошибка!");
                }
                else
                {
                    var stroke = strokes.Find(x => (x.DT.Day == measure.DT.Day) && ((x.DT.Month == measure.DT.Month)) && ((x.DT.Year == measure.DT.Year)));
                    stroke.Box2_Cond = measure.Energy.ToString();
                }
            }
        }
    }
}


