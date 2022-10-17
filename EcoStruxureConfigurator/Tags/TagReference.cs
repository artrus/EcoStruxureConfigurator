using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator
{
    public class TagReference
    {
        public string Path = "~/";

        public TagReference()
        {
        }

        public void addReferenceByIO (string moduleName, string tagName)
        {
            Path += "IO Bus/" + moduleName + "/" + tagName;
        }
    }
}
