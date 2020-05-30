using System.Collections.Generic;

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
        public List<string> Synonyms { get; set; }
        public string Description { get; set; }

        public ICollection<PersonTag> PersonTags { get; set; }
    }
}
