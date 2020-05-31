using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Leome.Model;

namespace Leome.Pages.People
{
    public class EditModel : PageModel
    {
        private readonly Data.Context _context;

        public EditModel(Data.Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Person Person { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person = await _context.People.FindAsync(id);

            if (Person == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var personToUpdate = await _context.People.FindAsync(id);

            if (personToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Person>(
                personToUpdate,
                "person",
                s => s.FirstMidName, s => s.LastName))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.ID == id);
        }
    }
}
