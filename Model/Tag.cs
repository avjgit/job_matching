using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leome.Model
{
    public enum TagType
    {
        Industry, Language, Skill, Undefined
    }

    public class Tag
    {
        public int ID { get; set; }
        public TagType TagType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<PersonTag> Tags { get; set; }
    }
}
