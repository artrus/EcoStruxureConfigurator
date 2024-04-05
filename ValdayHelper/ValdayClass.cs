using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValdayHelper
{
    public class ValdayClass
    {
        public string Name { get; set; }

        private List<TagInValdayClass> Tags { get; set; }

        public ValdayClass(string name)
        {
            Name = name;
        }

        public void AddTag(string tagName, string type, int reg, int bit)
        {
            if (Tags == null)
                Tags = new List<TagInValdayClass>();
            TagInValdayClass.TagType tagType = TagInValdayClass.TagType.UNFIND;
            if (type.Contains("3X"))
            {
                if (bit < 16)
                    tagType = TagInValdayClass.TagType.StatusBit;
                else
                    tagType = TagInValdayClass.TagType.Status;
            }
            if (type.Contains("4X"))
            {
                if (bit < 16)
                    tagType = TagInValdayClass.TagType.HoldingBit;
                else
                    tagType = TagInValdayClass.TagType.Holding;
            }
            Tags.Add(new TagInValdayClass(tagName, tagType, reg, bit));
        }

        public List<TagInValdayClass> GetTags ()
        {
            return Tags;
        }
    }
}
