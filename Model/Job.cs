using System.Collections.Generic;

namespace Leome.Model
{
    public class Job
    {
        public int ID { get; set; }
        public CareerLevel CareerLevel { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; }
        public ICollection<JobTag> JobTags { get; set; }
    }
    public enum CareerLevel
    {
        Intern, Junior, Middle, Senior, Lead, Head
    }

}
