using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Leome.Model;
using System.Collections.Generic;

namespace Leome.Pages.People
{
    public class CreateModel : PersonPageModel
    {
        private readonly Data.Context _context;

        public CreateModel(Data.Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var person = new Person
            {
                PersonTags = new List<PersonTag>()
            };

            PopulatePersonTags(_context, person);

            return Page();
        }

        [BindProperty]
        public Person Person { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(
            string[] selectedTags)
        {
            var newPerson = new Person();
            if (selectedTags != null)
            {
                newPerson.PersonTags = new List<PersonTag>();
                foreach (var tag in selectedTags)
                {
                    var tagToAdd = new PersonTag
                    {
                        TagID = int.Parse(tag)
                    };
                    newPerson.PersonTags.Add(tagToAdd);
                }
            }

            if (await TryUpdateModelAsync<Person>(
                newPerson,
                "person",
                 s => s.ID, s => s.BirthDate, s => s.CanRelocate, s => s.City,
                 s => s.CurrentCareerLevel, s => s.Email, s => s.ExperienceType,
                 s => s.FirstMidName, s => s.LastName, s => s.Phone, s => s.ShortBio
                 ))
            {
                _context.People.Add(newPerson);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            
            UpdatePersonTags(_context, selectedTags, newPerson);
            PopulatePersonTags(_context, newPerson);
            return Page();
        }
    }
}
