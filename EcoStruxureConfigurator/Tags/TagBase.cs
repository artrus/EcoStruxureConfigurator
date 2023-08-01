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
        public readonly string SystemNameRus;
        public readonly string SystemNameEng;
        public TagReference TagReference;

        public TagBase(string name, string description, string systemNameRus, string systemNameEng)
        {
            if (name == null)
                Name = string.Empty;
            else
                Name = name;

            if (description == null)
                Description = string.Empty;
            else
                Description = description;

            if (systemNameRus == null)
                SystemNameRus = string.Empty;
            else
                SystemNameRus = systemNameRus;

            if (systemNameEng == null)
                SystemNameEng = string.Empty;
            else
                SystemNameEng = systemNameEng;
        }
    }
}
