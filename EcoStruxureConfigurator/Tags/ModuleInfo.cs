using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator
{
    public class ModuleInfo
    {
        public enum typeChannels {NONE, BOOL, INT, REAL, ALL}
        public readonly string XMLType;
        public readonly int CntInputs;
        public readonly int CntOutputs;
        public readonly typeChannels TypeInputs;
        public readonly typeChannels TypeOutputs;

        public ModuleInfo(string xmlType, int cntInputs, int cntOutputs, typeChannels typeInputs, typeChannels typeOutputs)
        {
            XMLType = xmlType;
            CntInputs = cntInputs;
            CntOutputs = cntOutputs;
            TypeInputs = typeInputs;
            TypeOutputs = typeOutputs;
        }
    }
}
