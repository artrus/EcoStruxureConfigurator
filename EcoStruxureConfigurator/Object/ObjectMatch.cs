using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator.Object
{
    public class ObjectMatch
    {
        public string SystemName;
        public Dictionary<string, ObjectBase> objects = new Dictionary<string, ObjectBase>();

        public ObjectMatch(string systemName)
        {
            SystemName = systemName;
        }

        public void AddObject(string descr, ObjectBase obj)
        {
            objects.Add(descr, obj);
        }
    }
}
