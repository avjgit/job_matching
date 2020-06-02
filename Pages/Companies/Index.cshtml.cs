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
        public int JobID { get; set; }


        public async Task OnGetAsync(int? id, int? jobId)
        {
            CompaniesWithJobs = new CompaniesIndexData();
            CompaniesWithJobs.Companies = await _context.Companies
                .Include(i => i.Jobs)
                    .ThenInclude(i => i.JobTags)
                .AsNoTracking()
                .OrderBy(i => i.Jobs.Count)
                .ToListAsync();

            if (id != null)
            {
                CompanyID = id.Value;
                var company = CompaniesWithJobs.Companies
                    .Where(i => i.ID == id.Value).Single();
                CompaniesWithJobs.Jobs = company.Jobs;
            }

            if (jobId != null)
            {
                JobID = jobId.Value;
                var selectedJob = CompaniesWithJobs.Jobs
                    .Where(x => x.ID == jobId).Single();

                var people = _context.People.Include(i => i.PersonTags).AsNoTracking();

                CompaniesWithJobs.Candidates = await GetBest.Candidates(selectedJob, people);
            }
        }
    }
}
