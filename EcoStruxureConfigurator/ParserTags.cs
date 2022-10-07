using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStruxureConfigurator
{
    public class ParserTags
    {
        public static List<Module> GetModules(List<TagIO> tags)
        {
            List<Module> modules = new List<Module>();
            if (tags == null || tags.Count == 0)
                throw new Exception("Функция GetModules(List<TagIO> принимает либо null, либо Count = 0");

            foreach (var tag in tags)
            {
                if (!modules.Exists(x => x.ID == tag.Module.ID))
                {
                    modules.Add(tag.Module);
                }
            }
            return modules;
        }
    }
}
