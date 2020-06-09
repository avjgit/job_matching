using Leome.Model;

namespace Leome.Pages.Jobs
{
    public class JobTagsViewModel
    {
        public int JobTagId { get; set; }
        public JobTag JobTag { get; set; }
        public bool Assigned { get; set; }
    }
}
