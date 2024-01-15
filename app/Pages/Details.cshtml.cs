using app.Models;
using app.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace app.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly EstateAgencyDbContext context;

        public DetailsModel(EstateAgencyDbContext context)
        {
            this.context = context;
        }

        public RealState Property { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Index");
            }

            Property = await context.Properties
                .Include(p => p.RealStateImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Property == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
