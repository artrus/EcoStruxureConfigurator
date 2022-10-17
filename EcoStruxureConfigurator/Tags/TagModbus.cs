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
        public List<string> Path = new List<string>();
        

        public TagModbus(string name, string description, string system, int addr, TagInfoModbus tagInfo) : base(name, description, system)
        {
            Addr = addr;
            TagInfo = tagInfo;
        }

        public void AddPath(string path)
        {
            Path.Add(path);
        }
    }
}
