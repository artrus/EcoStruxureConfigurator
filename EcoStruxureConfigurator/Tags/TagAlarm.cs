using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EcoStruxureConfigurator
{
    public class TagAlarm
    {
        public string Description;
        Color Color;

        public TagAlarm(string color)
        {
            Color = (Color)ColorConverter.ConvertFromString(color.Insert(0, "#"));
        }

        public string GetColor()
        {
            return Color.R + ":" + Color.G + ":" + Color.B;
        }
    }
}
