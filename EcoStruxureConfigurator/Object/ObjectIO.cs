using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator.Object
{
    public class ObjectIO
    {
        public enum CATEGORY { IO_INPUT, IO_OUTPUT, CONTROL_INPUT, CONTROL_OUTPUT, SP, ST }
        public enum TYPE { BOOL, INT, REAL }
        public string Name;
        public string Descr;
        public string Type;
        //public TYPE Type;
        public CATEGORY Category;
        public string Color;
        public ObjectIO(string name, string descr, string type, CATEGORY category, string color = null)
        {
            if ((descr == null) || (String.Equals(descr, "")))
                throw new ArgumentException("ObjectIO не имеет Descr");
            if ((type == null) || (String.Equals(name, "")))
                throw new ArgumentException("ObjectIO не имеет Type");

            Name = name;
            Descr = descr;
            Type = type;
            /*
            if (type.ToUpper().Contains("BOOL"))
                Type = TYPE.BOOL;
            else if (type.ToUpper().Contains("INT"))
                Type = TYPE.INT;
            else if (type.ToUpper().Contains("REAL"))
                Type = TYPE.REAL;
            else
                throw new ArgumentException("В ObjectIO не найден Type");
*/
            Category = category;
            Color = color;
        }
    }
}
