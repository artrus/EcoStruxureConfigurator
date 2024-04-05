using EcoStruxureConfigurator.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator
{
    public class ObjectBase : ICloneable
    {
        public string Type { get; set; }
        //public string PsevdoName { get; set; }
        public string RussianName { get; set; }

        private readonly List<ObjectIO> IO;

        public ObjectBase()
        {
            IO = new List<ObjectIO>();
        }

        public ObjectBase(string type, string russianName, List<ObjectIO> iO)
        {
            Type = type;
            RussianName = russianName;
            IO = iO;
        }

        public void AddIOinput(string name, string descr, string type)
        {
            if (CheakName(descr))
                IO.Add(new ObjectIO(name, descr, type, ObjectIO.CATEGORY.IO_INPUT));
        }

        public void AddIOoutput(string name, string descr, string type)
        {
            if (CheakName(descr))
                IO.Add(new ObjectIO(name, descr, type, ObjectIO.CATEGORY.IO_OUTPUT));
        }

        public void AddControlInput(string name, string descr, string type)
        {
            if (CheakName(descr))
                IO.Add(new ObjectIO(name, descr, type, ObjectIO.CATEGORY.CONTROL_INPUT));
        }

        public void AddControlOutput(string name, string descr, string type)
        {
            if (CheakName(descr))
                IO.Add(new ObjectIO(name, descr, type, ObjectIO.CATEGORY.CONTROL_OUTPUT));
        }

        public void AddST(string name, string descr, string type, string color)
        {
            if (CheakName(descr))
                IO.Add(new ObjectIO(name, descr, type, ObjectIO.CATEGORY.ST, color));
        }

        public void AddSP(string name, string descr, string type)
        {
            if (CheakName(descr))
                IO.Add(new ObjectIO(name, descr, type, ObjectIO.CATEGORY.SP));
        }

        public List<ObjectIO> GetAll()
        {
            return IO;
        }

        public List<ObjectIO> GetAllST()
        {
            return IO.FindAll(x => x.Category == ObjectIO.CATEGORY.ST);
        }

        public List<ObjectIO> GetAllSP()
        {
            return IO.FindAll(x => x.Category == ObjectIO.CATEGORY.SP);
        }

        private bool CheakName(string descr)
        {
            if ((descr != null) && (descr.Length > 0))
                return true;
            return false;
        }

        public object Clone()
        {
            List<ObjectIO> objIO = new List<ObjectIO>();
            objIO.AddRange(IO);
            return new ObjectBase(Type, RussianName, objIO);
        }
    }
}
