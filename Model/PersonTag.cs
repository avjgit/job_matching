namespace Leome.Model
{
    public class PersonTag
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public int TagID { get; set; }
        public SkillLevel SkillLevel { get; set; }
        public TagWeight Weight { get; set; } // also person can set preferences

        public Person Person { get; set; }
        public Tag Tag { get; set; }
    }

}
