using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Leome.Model;
using System;
using System.Linq;

namespace Leome.Pages.People
{
    public class IndexModel : PageModel
    {
        private readonly Data.Context _context;

        public IndexModel(Data.Context context)
        {
            _context = context;
        }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Person> Person { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            NameSort = string.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            CurrentFilter = searchString;

            IQueryable<Person> people = from s in _context.People
                                             select s;

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                people = people.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    people = people.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    people = people.OrderBy(s => s.BirthDate);
                    break;
                case "date_desc":
                    people = people.OrderByDescending(s => s.BirthDate);
                    break;
                default:
                    people = people.OrderBy(s => s.LastName);
                    break;
            }

            Person = await people.AsNoTracking().ToListAsync();
        }
    }
}
