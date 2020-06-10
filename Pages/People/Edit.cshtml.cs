using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Leome.Model;

namespace Leome.Pages.People
{
    public class EditModel : PersonPageModel
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

            Person = await _context.People
                .Include(x => x.PersonTags)
                .ThenInclude(x => x.Tag)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Person == null)
            {
                return NotFound();
            }
            PopulatePersonTags(_context, Person);
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

            var personToUpdate = await _context.People
                .Include(x => x.PersonTags)
                    .ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (personToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Person>(
                personToUpdate,
                "person",
                s => s.FirstMidName, s => s.LastName))
            {
                UpdatePersonTags(_context, selectedTags, personToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdatePersonTags(_context, selectedTags, personToUpdate);
            PopulatePersonTags(_context, personToUpdate);

            return Page();
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.ID == id);
        }
    }
}
