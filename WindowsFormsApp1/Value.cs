using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Value
    {
        public DateTime dateTime;
        public double value;

        public void SetDateTimeFromOADate(string s)
        {
            double d = Convert.ToDouble(s.Replace('.', ','));
            dateTime = DateTime.FromOADate(d);
        }

        public void SetValueFromString(string s)
        {
            value = Convert.ToDouble(s);
        }
    
    }
}
