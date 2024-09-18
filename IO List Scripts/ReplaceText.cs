using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_List_Scripts
{
    internal class ReplaceText
    {
        public string From;
        public string To;

        public ReplaceText(string findTag, string replaceTag)
        {
            From = findTag;
            To = replaceTag;
        }
    }
}
