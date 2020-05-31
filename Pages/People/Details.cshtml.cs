using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Leome.Model;

namespace Leome.Pages.People
{
    public class DetailsModel : PageModel
    {
        private readonly Data.Context _context;

        public DetailsModel(Data.Context context)
        {
            _context = context;
        }

        public Person Person { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person = await _context
                .People
                .Include(x => x.PersonTags)
                .ThenInclude(x => x.Tag)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Person == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
