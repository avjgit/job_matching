using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Leome.Model;

namespace Leome.Pages.People
{
    public class IndexModel : PageModel
    {
        private readonly Data.Context _context;

        public IndexModel(Data.Context context)
        {
            _context = context;
        }

        public IList<Person> Person { get;set; }

        public async Task OnGetAsync()
        {
            Person = await _context.People.ToListAsync();
        }
    }
}
