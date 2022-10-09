using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator
{
    public class TagInfoBase
    {
        public readonly string TypeName;
        public readonly string XML_Type;
        public enum BinaryAnalog { Binary, Analog };
        public BinaryAnalog Type;

        public TagInfoBase(string typeName,string xML_Type, BinaryAnalog type)
        {
            TypeName = typeName;
            XML_Type = xML_Type;
            Type = type;
        }
    }
}
