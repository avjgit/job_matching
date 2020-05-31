using System;
using System.Collections.Generic;

namespace Leome.Model
{
    public class Person
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public string ShortBio { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string CanRelocate { get; set; }
        public string PrefersRemote { get; set; }
        public DateTime BirthDate { get; set; }
        public CurrentCareerLevel CurrentCareerLevel { get; set; }
        public ExperienceType ExperienceType { get; set; }
        public string MyProperty { get; set; }
        public ICollection<PersonTag> PersonTags { get; set; }
    }

    public enum CurrentCareerLevel
    {
        Intern, Junior, Middle, Senior, Lead, Head
    }

    public enum ExperienceType
    {
        SalesOrAccount,
        CustomerCare,
        KeyAccount,
        PreSales,
        AfterSales,
        TeleSales,
        TechnicalSales,
        BusDevSales,
        SalesOperations,
        Other
    }
}
