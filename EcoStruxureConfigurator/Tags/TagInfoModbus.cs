using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator
{
    public class TagInfoModbus : TagInfoBase
    {
        public readonly int Size;
        public readonly int XML_RegisterType;

        public TagInfoModbus(string typeName, int size, string xMLType, int xML_RegisterType, TagInfoBase.BinaryAnalog type) : base(typeName, xMLType, type)
        {
            Size = size;
            XML_RegisterType = xML_RegisterType;
        }
    }
}
