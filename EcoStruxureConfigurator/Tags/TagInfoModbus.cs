using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator
{
    public class TagInfoModbus : TagInfoBase
    {
        public enum RegType { DEFAULT=0, REAL=7}
        public readonly int Size;
        public readonly RegType XML_RegisterType;

        public TagInfoModbus(string typeName, int size, string xMLType, RegType xML_RegisterType, TagInfoBase.BinaryAnalog type) : base(typeName, xMLType, type)
        {
            Size = size;
            XML_RegisterType = xML_RegisterType;
        }
    }
}
