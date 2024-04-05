using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator.Logger
{
    public interface ILogger
    {
        void WriteLine(string message);
        void WriteText (string message);
        void NewLine ();    
        void Clear();
    }
}
