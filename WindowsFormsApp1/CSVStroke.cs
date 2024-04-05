using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class CSVStroke
    {
        public DateTime DT { get; set; }
        public string Date { get; set; }
        public string Ext_TE_Min { get; set; }
        public string Ext_TE_Max { get; set; }
        public string Ext_TE_Avg { get; set; }
        public string Ext_RH_Min { get; set; }
        public string Ext_RH_Max { get; set; }
        public string Ext_RH_Avg { get; set; }

        public string Box1_TE_Min { get; set; }
        public string Box1_TE_Max { get; set; }
        public string Box1_TE_Avg { get; set; }
        public string Box1_RH_Min { get; set; }
        public string Box1_RH_Max { get; set; }
        public string Box1_RH_Avg { get; set; }

        public string Box2_TE_Min { get; set; }
        public string Box2_TE_Max { get; set; }
        public string Box2_TE_Avg { get; set; }
        public string Box2_RH_Min { get; set; }
        public string Box2_RH_Max { get; set; }
        public string Box2_RH_Avg { get; set; }
        public string Input { get; set; }
        public string Solar { get; set; }
        public string WaterGen { get; set; }
        public string Box1_LampMain { get; set; }
        public string Box2_LampMain { get; set; }
        public string Box1_LampSub{ get; set; }
        public string Box2_LampSub { get; set; }
        public string Box1_Humi { get; set; }
        public string Box2_Humi { get; set; }
        public string Box1_Pump { get; set; }
        public string Box2_Pump { get; set; }
        public string Box1_Cond { get; set; }
        public string Box2_Cond { get; set; }

        public CSVStroke(DateTime dT, string date)
        {
            Date = date;
            DT = dT;
        }
    }
}
