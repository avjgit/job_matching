using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Leome.Model;
using System.Collections.Generic;

namespace Leome.Pages.Jobs
{
    public class DetailsModel : PageModel
    {
        private readonly Data.Context _context;

        public DetailsModel(Data.Context context)
        {
            _context = context;
        }

        public Job Job { get; set; }
        public IList<Person> Candidates { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Job = await _context.Jobs
                .Include(j => j.Company)
                .Include(j => j.JobTags)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Job == null)
            {
                return NotFound();
            }
            var people = _context.People.Include(i => i.PersonTags).AsNoTracking();

            Candidates = await GetBest.Candidates(Job, people);

            return Page();
        }
    }
}
