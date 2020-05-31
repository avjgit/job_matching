using System.Collections.Generic;

namespace Leome.Model
{
    public enum TagType
    {
        Industry, Language, Skill, Undefined
    }

    public enum TagWeight
    {
        Weight1, Weight2, Weight3, Weight5, Weight8, Weight13, Weight20, Weight40, Weight100
    }

    public class Tag
    {
        public int ID { get; set; }
        public TagType TagType { get; set; }
        public string Title { get; set; }
        public string SynonymsCSV { get; set; }
        public string Description { get; set; }

        public ICollection<PersonTag> PersonTags { get; set; }
    }
}
