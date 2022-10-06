using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator
{
    public class Module
    {
        public readonly int ID;
        public readonly string Name;
        public readonly string Type;
        public readonly ModuleInfo ModuleInfo;

        public Module(int iD, string name, string type, ModuleInfo moduleInfo)
        {
            ID = iD;
            Name = name;
            Type = type;
            ModuleInfo = moduleInfo;
        }
    }
}
