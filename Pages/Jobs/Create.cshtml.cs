using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Leome.Model;
using System.Security.Cryptography.X509Certificates;

namespace Leome.Pages.Jobs
{
    public class CreateModel : CompaniesNamePageModel
    {
        private readonly Data.Context _context;

        public CreateModel(Data.Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateCompaniesDropDownList(_context);

            return Page();
        }

        [BindProperty]
        public Job Job { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyJob = new Job();

            if (await TryUpdateModelAsync<Job>(
                 emptyJob,
                 "job",   // Prefix for form value.
                 s => s.ID, s => s.CompanyID, s => s.Title, s => s.CareerLevel, s => s.City,
                 s => s.Description))
            {
                _context.Jobs.Add(emptyJob);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            PopulateCompaniesDropDownList(_context, emptyJob.CompanyID);
            return Page();
        }
    }
}
