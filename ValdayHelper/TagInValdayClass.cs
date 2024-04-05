using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValdayHelper
{
    public class TagInValdayClass
    {
        public enum TagType { Status, StatusBit, Holding, HoldingBit, UNFIND }
        
        public string Name { get; set; }
        
        public TagType Type { get; set; }

        public int Register { get; set; }

        public int Bit { get; set; }

        public TagInValdayClass(string name, TagType type, int register, int bit)
        {
            Name = name;
            Type = type;
            Register = register;
            Bit = bit;
        }
    }
}
