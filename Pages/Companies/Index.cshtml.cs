using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Leome.Model;
using Leome.ViewModel;
using System.Linq;

namespace Leome.Pages.Companies
{
    public class IndexModel : PageModel
    {
        private readonly Data.Context _context;

        public IndexModel(Data.Context context)
        {
            _context = context;
        }

        public CompaniesIndexData CompaniesWithJobs { get; set; }
        public int CompanyID { get; set; }

        public async Task OnGetAsync(int? id, int? jobId)
        {
            CompaniesWithJobs = new CompaniesIndexData();
            CompaniesWithJobs.Companies = await _context.Companies
                .AsNoTracking()
                .ToListAsync();

            if (id != null)
            {
                CompanyID = id.Value;
                var company = CompaniesWithJobs.Companies
                    .Where(i => i.ID == id.Value).Single();
            }

        }
    }
}
