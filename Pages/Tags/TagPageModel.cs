using Leome.Data;
using Leome.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Leome.Pages.Jobs
{
    public class TagPageModel : PageModel
    {
        public SelectList TagTypesSelectList { get; set; }

    }
}
