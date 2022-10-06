using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator
{
    public class TagBase
    {
        public readonly string Name;
        public readonly string Description;

        public TagBase(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
