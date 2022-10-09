using EcoStruxureConfigurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator
{
    public class TagIO : TagBase 
    {
        public readonly Module Module;
        public readonly int Channel;
        public readonly TagInfoIO TagInfo;

        public TagIO(string name, string description, string system, Module module, int channel, TagInfoIO tagInfo) : base(name, description, system)
        {
            Module = module;
            Channel = channel;
            TagInfo = tagInfo;
        }
    }
}
