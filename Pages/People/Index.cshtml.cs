using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Leome.Data;
using Leome.Model;

namespace Leome.Pages.People
{
    public class IndexModel : PageModel
    {
        private readonly Leome.Data.Context _context;

        public IndexModel(Leome.Data.Context context)
        {
            _context = context;
        }

        public IList<Person> Person { get;set; }

        public async Task OnGetAsync()
        {
            Person = await _context.People.ToListAsync();
        }
    }
}
