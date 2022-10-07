using EcoStruxureConfigurator.Tags;
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
        public readonly TagIOInfo TagInfo;

        public TagIO(string name, string description, Module module, int channel, TagIOInfo tagInfo) : base(name, description)
        {
            Module = module;
            Channel = channel;
            TagInfo = tagInfo;
        }
    }
}
