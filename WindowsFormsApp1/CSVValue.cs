using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class CSVValue
    {
        public DateTime DT;
        public double value;

        public void SetDateTimeFromOADate(string s)
        {
            double d = Convert.ToDouble(s.Replace('.', ','));
            DT = DateTime.FromOADate(d);
        }

        public void SetValueFromString(string s)
        {
            value = Convert.ToDouble(s.Replace('.', ','));
        }
    
    }
}
