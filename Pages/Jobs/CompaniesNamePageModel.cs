using System.Linq;
using Leome.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Leome.Pages.Jobs
{
    public class CompaniesNamePageModel : PageModel
    {
        public SelectList CompaniesNamesSelectList { get; set; }

        public void PopulateCompaniesDropDownList(
            Context _context,
            object selectedCompany = null)
        {
            var companiesQuery = from d in _context.Companies
                                   orderby d.CompanyName
                                   select d;

            CompaniesNamesSelectList = new SelectList(companiesQuery.AsNoTracking(),
                        "ID", "CompanyName", selectedCompany);
        }
    }
}
