using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Leome.Model;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

namespace Leome.Pages.Jobs
{
    public class CreateModel : JobPageModel
    {
        private readonly Data.Context _context;

        public CreateModel(Data.Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateCompaniesDropDownList(_context);
            var job = new Job
            {
                JobTags = new List<JobTag>()
            };

            // Provides an empty collection for the foreach loop
            // foreach (var course in Model.AssignedCourseDataList)
            // in the Create Razor page.
            PopulateJobTags(_context, job);
            return Page();
        }

        [BindProperty]
        public Job Job { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(
            string[] selectedTags)
        {
            var newJob = new Job();
            if (selectedTags != null)
            {
                newJob.JobTags = new List<JobTag>();
                foreach (var tag in selectedTags)
                {
                    var tagtoAdd = new JobTag
                    {
                        TagID = int.Parse(tag)
                    };
                    newJob.JobTags.Add(tagtoAdd);
                }
            }

            if (await TryUpdateModelAsync<Job>(
                newJob,
                "job",
                 s => s.ID, s => s.CompanyID, s => s.Title, s => s.CareerLevel, s => s.City,
                 s => s.Description))
            {
                _context.Jobs.Add(newJob);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateJobTags(_context, selectedTags, newJob);
            PopulateJobTags(_context, newJob);
            PopulateCompaniesDropDownList(_context, newJob.CompanyID);
            return Page();
        }
    }
}
