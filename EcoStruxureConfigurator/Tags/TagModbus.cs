using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator
{
    public class TagModbus : TagBase
    {
        public readonly int Addr;
        public readonly TagInfoModbus TagInfo;

        public TagModbus(string name, string description, string system, int register, TagInfoModbus tagInfo) : base(name, description, system)
        {
            Addr = register;
            TagInfo = tagInfo;
        }
    }
}
