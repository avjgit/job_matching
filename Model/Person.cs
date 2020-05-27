using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leome.Model
{
    public class Person
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string CanRelocate { get; set; }
        public string PrefersRemote { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<PersonTag> Tags { get; set; }
    }
}
