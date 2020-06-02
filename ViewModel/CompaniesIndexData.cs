using Leome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leome.ViewModel
{
    public class CompaniesIndexData
    {
        public IEnumerable<Company> Companies { get; set; }
        public IEnumerable<Job> Jobs { get; set; }
        public IEnumerable<Person> Candidates { get; set; }
    }
}
