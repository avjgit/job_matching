using System.Collections.Generic;

namespace Leome.Model
{
    public class Company
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string RepresentativesFirstMidName { get; set; }
        public string RepresentativesLastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Intro { get; set; }
        public string City { get; set; }
        public string Comments { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}
