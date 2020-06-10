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
        public IList<Match> Candidates { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Job = await _context.Jobs
                .Include(j => j.Company)
                .Include(j => j.JobTags)
                    .ThenInclude(x => x.Tag)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Job == null)
            {
                return NotFound();
            }

            var people = _context.People
                .Include(i => i.PersonTags)
                    .ThenInclude(x => x.Tag)
                .AsNoTracking();

            Candidates = await GetBest.Candidates(Job, people);

            return Page();
        }
    }
}
