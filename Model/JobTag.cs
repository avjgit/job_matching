namespace Leome.Model
{
    public class JobTag
    {
        public int ID { get; set; }
        public int JobID { get; set; }
        public int TagID { get; set; }
        public SkillLevel SkillLevel { get; set; }
        public TagWeight Weight { get; set; } 

        public Job Job { get; set; }
        public Tag Tag { get; set; }
    }
}
