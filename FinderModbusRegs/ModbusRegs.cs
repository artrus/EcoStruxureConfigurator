using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinderModbusRegs
{
    public class ModbusRegs
    {
        public string FileName;
        public string Name;
        public string Description;
        public string Function;
        public string Address;

        public ModbusRegs(string fileName, string name, string description, string function, string address)
        {
            FileName = fileName;
            Name = name;
            Description = description;
            Function = function;
            Address = address;
        }

        public override string ToString()
        {
            return $"{FileName}   {Name}   {Description}   {Function}   {Address}";
        }

        internal string ToCSVString()
        {
            return $"{FileName};{Name};{Description};{Function};{Address}";
        }
    }
}
