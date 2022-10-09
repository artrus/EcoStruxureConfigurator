using EcoStruxureConfigurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator
{
    public class TagInfoIO : TagInfoBase
    {
        public enum Direct { Input, Output };
        public readonly Direct Dir;
        public readonly string XML_Direct;

        public TagInfoIO(string typeName, string xMLType, TagInfoBase.BinaryAnalog type) : base(typeName, xMLType, type)
        {
            if (XML_Type.ToLower().Contains("input"))
            {
                XML_Direct = "InputChannelNumber";
                Dir = Direct.Input;
            }
            else
            {
                XML_Direct = "OutputChannelNumber";
                Dir = Direct.Output;
            }
        }
    }
}
