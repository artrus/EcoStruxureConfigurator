using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator
{
    public class ModuleInfo
    {
        public enum TypeChannels {NONE, BOOL, INT, REAL, ALL}
        public readonly string XMLType;
        public readonly int CntInputs;
        public readonly int CntOutputs;
        public readonly TypeChannels TypeInputs;
        public readonly TypeChannels TypeOutputs;

        public ModuleInfo(string xmlType, int cntInputs, int cntOutputs, TypeChannels typeInputs, TypeChannels typeOutputs)
        {
            XMLType = xmlType;
            CntInputs = cntInputs;
            CntOutputs = cntOutputs;
            TypeInputs = typeInputs;
            TypeOutputs = typeOutputs;
        }
    }
}
