using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator.Tags
{
    public class TagIOInfo
    {
        public readonly string XML_Type;
        public readonly string XML_Direct;

        public TagIOInfo(string xMLType)
        {
            XML_Type = xMLType;
            if (XML_Type.ToLower().Contains("input"))
                XML_Direct = "InputChannelNumber";
            else
                XML_Direct = "OutputChannelNumber";
        }
    }
}
