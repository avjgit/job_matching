using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Leome.Model;
using System;

namespace Leome.Pages.Jobs
{
    public class EditModel : JobPageModel
    {
        private readonly Data.Context _context;

        public EditModel(Data.Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Job Job { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Job = await _context.Jobs
                .Include(j => j.Company)
                .Include(x => x.JobTags)
                    .ThenInclude(x => x.Tag)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Job == null)
            {
                return NotFound();
            }
            PopulateCompaniesDropDownList(_context, Job.CompanyID);
            PopulateJobTags(_context, Job);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedTags)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobToUpdate = await _context.Jobs
                .Include(i => i.Company)
                .Include(i => i.JobTags)
                    .ThenInclude(i => i.Tag)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (jobToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Job>(
                jobToUpdate,
                "job",
                 s => s.ID, s => s.CompanyID, s => s.Title, s => s.CareerLevel, s => s.City,
                 s => s.Description))
            {
                UpdateJobTags(_context, selectedTags, jobToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateJobTags(_context, selectedTags, jobToUpdate);
            PopulateJobTags(_context, jobToUpdate);
            PopulateCompaniesDropDownList(_context, jobToUpdate.CompanyID);

            return Page();
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.ID == id);
        }
    }
}
