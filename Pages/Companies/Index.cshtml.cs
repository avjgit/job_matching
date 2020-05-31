using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Leome.Model;

namespace Leome.Pages.Companies
{
    public class IndexModel : PageModel
    {
        private readonly Data.Context _context;

        public IndexModel(Data.Context context)
        {
            _context = context;
        }

        public IList<Company> Company { get;set; }

        public async Task OnGetAsync()
        {
            Company = await _context.Companies.ToListAsync();
        }
    }
}
