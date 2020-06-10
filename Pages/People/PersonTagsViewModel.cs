using Leome.Model;

namespace Leome.Pages.People
{
    public class PersonTagsViewModel
    {
        public int PersonTagId { get; set; }
        public PersonTag PersonTag { get; set; }
        public bool Assigned { get; set; }
    }
}
