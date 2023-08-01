using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        List<Value> valuesPower;
        List<Value> valuesTE;
        List<Value> valuesRH;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetPowerMeas();
            GetTE();
            GetRH();


        }

        private void GetPowerMeas()
        {
            valuesPower = OpenHistory(@"d:\Projects\Контейнер\Logs\water2.csv");

            List<DateTime> dateList = new List<DateTime>();
            List<PowerMeasure> powerList = new List<PowerMeasure>();
            List<double> avgList = new List<double>();
            DateTime dateTimeLast = valuesPower[0].dateTime;
            int count = 1;
            foreach (var value in valuesPower)
            {
                avgList.Add(value.value);
                if (dateList.Count == 0)
                {
                    dateList.Add(value.dateTime);

                    continue;
                }

                double diffMinutes = (value.dateTime - dateTimeLast).TotalMinutes;
                if ((diffMinutes > 10) || (count > valuesPower.Count - 2))
                {
                    dateList.Add(value.dateTime);
                    double totalSum = 0;
                    foreach (var avg in avgList)
                    {
                        totalSum += avg;
                    }
                    int avgValue = Convert.ToInt32(totalSum / avgList.Count);
                    avgList.Clear();
                    powerList.Add(new PowerMeasure(dateList[dateList.Count - 2], dateTimeLast, avgValue));

                }
                dateTimeLast = value.dateTime;
                count++;
            }


            foreach (var power in powerList)
            {
                richTextBox1.AppendText(power.ToString());
                richTextBox1.AppendText("\n\r");

                richTextBoxT1.AppendText(power.DateTimeStart.ToString());
                richTextBoxT1.AppendText("\r");

                richTextBoxT2.AppendText(power.DateTimeEnd.ToString());
                richTextBoxT2.AppendText("\r");

                richTextBoxAvg.AppendText(power.AvgValue.ToString());
                richTextBoxAvg.AppendText("\r");

                richTextBoxTotal.AppendText(power.TotalPower.ToString());
                richTextBoxTotal.AppendText("\r");

                richTextBoxTimer.AppendText(power.DiffMinute.ToString());
                richTextBoxTimer.AppendText("\r");

                richTextBoxpower.AppendText(power.Power.ToString());
                richTextBoxpower.AppendText("\r");
            }
        }

        private void GetTE()
        {
            valuesTE = OpenHistory(@"d:\Projects\Контейнер\Logs\te.csv");

            List<TEMeasure> measures = new List<TEMeasure>();
            bool newDay = true;
            DateTime lastDay = valuesTE[0].dateTime, curDay;

            List<double> avgValues = new List<double>();
            double avgValue;
            double minValue = 100;
            double maxValue = 0;
            foreach (var value in valuesTE)
            {
                curDay = value.dateTime;
                if (measures.Count == 0)
                {
                    measures.Add(new TEMeasure(value.dateTime));
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
                    measures.Add(new TEMeasure(value.dateTime));
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

                lastDay = value.dateTime;
            }

            foreach (var meas in measures)
            {
                richTextBox2.AppendText(meas.ToString());
                richTextBox2.AppendText("\r");

                richTextBoxTEmin.AppendText(meas.MinValue.ToString());
                richTextBoxTEmin.AppendText("\r");

                richTextBoxTEmax.AppendText(meas.MaxValue.ToString());
                richTextBoxTEmax.AppendText("\r");

                richTextBoxTEavg.AppendText(meas.AvgValue.ToString());
                richTextBoxTEavg.AppendText("\r");
            }

        }


        private void GetRH()
        {
            valuesTE = OpenHistory(@"d:\Projects\Контейнер\Logs\rh.csv");

            List<TEMeasure> measures = new List<TEMeasure>();
            bool newDay = true;
            DateTime lastDay = valuesTE[0].dateTime, curDay;

            List<double> avgValues = new List<double>();
            double avgValue;
            double minValue = 100;
            double maxValue = 0;
            foreach (var value in valuesTE)
            {
                curDay = value.dateTime;
                if (measures.Count == 0)
                {
                    measures.Add(new TEMeasure(value.dateTime));
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
                    measures[measures.Count - 1].AvgValue = Math.Round(avgValue, 0);
                    measures[measures.Count - 1].MinValue = Math.Round(minValue, 0);
                    measures[measures.Count - 1].MaxValue = Math.Round(maxValue, 0  );
                    measures.Add(new TEMeasure(value.dateTime));
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

                lastDay = value.dateTime;
            }

            foreach (var meas in measures)
            {
                richTextBox3.AppendText(meas.ToString());
                richTextBox3.AppendText("\r");

                richTextBoxRHmin.AppendText(meas.MinValue.ToString());
                richTextBoxRHmin.AppendText("\r");

                richTextBoxRHmax.AppendText(meas.MaxValue.ToString());
                richTextBoxRHmax.AppendText("\r");

                richTextBoxRHavg.AppendText(meas.AvgValue.ToString());
                richTextBoxRHavg.AppendText("\r");
            }

        }

        private List<Value> OpenHistory(string path)
        {
            FileInfo existingFile = new FileInfo(path);
            if (!existingFile.Exists)
            {
                MessageBox.Show("Фаил " + path + " не найден");
                return new List<Value>();
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
            public string dateTime { get; set; }
            public string value { get; set; }
        }

        private List<Value> GetValues(string pathCsvFile)
        {
            List<Value> values = new List<Value>();
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
                            Value value = new Value();
                            value.SetValueFromString(record.value);
                            value.SetDateTimeFromOADate(record.dateTime);
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

    }
}


