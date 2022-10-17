using EcoStruxureConfigurator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator
{
    public class TagBase
    {
        public readonly string Name;
        public readonly string Description;
        public readonly string System;
        public TagReference TagReference;

        public TagBase(string name, string description, string system)
        {
            if (name == null)
                Name = string.Empty;
            else
                Name = name;

            if (description == null)
                Description = string.Empty;
            else
                Description = description;

            if (system == null)
                System = string.Empty;
            else
                System = system;
        }
    }
}
