using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator.Object
{
    public class ObjectMatch
    {
        public string SystemNameRus;
        public string PsevdoName;
        public Dictionary<string, ObjectBase> objects = new Dictionary<string, ObjectBase>();

        public ObjectMatch(string systemName, string psevdoName)
        {
            SystemNameRus = systemName;
            PsevdoName = psevdoName;
        }

        public void AddObject(string descrEng, string descrRus, ObjectBase obj)
        {
            /* obj.RussianName = descrRus;
             objects.Add(descrEng, obj);*/
            ObjectBase objCopy = (ObjectBase)obj.Clone();
            objCopy.RussianName = descrRus;
            objects.Add(descrEng, objCopy);
        }
    }
}
