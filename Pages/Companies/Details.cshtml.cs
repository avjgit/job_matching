using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Leome.Model;
using System.Collections.Generic;
using System.Linq;

namespace Leome.Pages.Companies
{
    public class DetailsModel : PageModel
    {
        private readonly Data.Context _context;

        public DetailsModel(Data.Context context)
        {
            _context = context;
        }

        public Company Company { get; set; }
        public List<Job> Jobs { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Company = await _context.Companies
                                .Include(i => i.Jobs)
                    .ThenInclude(i => i.JobTags)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Company == null)
            {
                return NotFound();
            }
            Jobs = Company.Jobs.ToList();
            return Page();
        }
    }
}
