using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Leome.Model;

namespace Leome.Pages.Jobs
{
    public class IndexModel : PageModel
    {
        private readonly Leome.Data.Context _context;

        public IndexModel(Leome.Data.Context context)
        {
            _context = context;
        }

        public IList<Job> Job { get;set; }

        public async Task OnGetAsync()
        {
            Job = await _context.Jobs
                .Include(j => j.Company).ToListAsync();
        }
    }
}
