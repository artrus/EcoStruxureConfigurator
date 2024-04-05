using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValdayHelper
{
    public class Tag
    {
        public string Name;
        public string Description;
        public string Type;
        public int Addr3X = -1;
        public int Addr4X = -1;

        public Tag(string name, string description, string type)
        {
            Name = name;
            Description = description;
            Type = type;
        }
    }
}
