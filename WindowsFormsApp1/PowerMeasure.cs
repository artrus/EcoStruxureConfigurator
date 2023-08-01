using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class PowerMeasure
    {
        public DateTime DateTimeStart;
        public DateTime DateTimeEnd;
        public int DiffMinute;
        public int AvgValue;
        public double Power;
        public double TotalPower;

        public PowerMeasure(DateTime dateTimeStart, DateTime dateTimeEnd, int avgValue)
        {
            DateTimeStart = dateTimeStart;
            DateTimeEnd = dateTimeEnd;
            AvgValue = avgValue;
            CalcPower(avgValue, (dateTimeEnd - dateTimeStart).TotalMinutes);
        }

        public void CalcPower(int avgValue, double minute)
        {
            DiffMinute = Convert.ToInt32(minute);
            Power = (((avgValue / 60) * minute) * 2.7);

            int time1 = 1440 - Convert.ToInt32(minute);
            int power1 = Convert.ToInt32(time1 * 5600 / 1440);
            TotalPower = (Power + power1) * 0.001;
            TotalPower = Math.Round(TotalPower, 1); 
        }

        public override string ToString()
        {
            return AvgValue +  "    DATA: " + DateTimeStart.ToString() + "---" + DateTimeEnd.ToString() + "    " + TotalPower + "   " + DiffMinute;
        }
    }
}
