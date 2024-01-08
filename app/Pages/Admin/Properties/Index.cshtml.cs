using app.Models;
using app.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace app.Pages.Admin.Properties
{
    public class IndexModel : PageModel
    {
        private readonly EstateAgencyDbContext context;
        
        public List<RealState> Properties { get; set; } = new List<RealState>(); 
        public IndexModel(EstateAgencyDbContext context)
        {
            this.context = context;
        }
        public void OnGet()
        {
            Properties = context.Properties
                .Include(p => p.RealStateImages)
                .OrderByDescending(p => p.Id)
                .ToList();
        }
    }
}
