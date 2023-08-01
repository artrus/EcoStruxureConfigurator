using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class TEMeasure
    {
        public DateTime DT;
        public double AvgValue;
        public double MinValue;
        public double MaxValue;

        public TEMeasure(DateTime dT)
        {
            DT = dT;
        }


        public override string ToString()
        {
            return DT.ToString() + "    " + MinValue + "   " + MaxValue + "   " + AvgValue;
        }
    }
}
