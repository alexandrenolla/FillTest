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

        public async Task OnGetAsync(string searchString, decimal? minPrice, decimal? maxPrice, int? pageIndex)
        {
            Properties = context.Properties
                .Include(p => p.RealStateImages)
                .OrderByDescending(p => p.Id)
                .ToList();

            IQueryable<RealState> query = context.Properties
                    .Include(p => p.RealStateImages)
                    .OrderByDescending(p => p.Id);

                if (!string.IsNullOrEmpty(searchString))
                {
                    query = query.Where(p =>
                        p.Title.Contains(searchString) ||
                        p.Neighborhood.Contains(searchString) ||
                        p.BusinessType.Contains(searchString) ||
                        p.Address.Contains(searchString)
                    );
                    pageIndex = 1;
                }

                if (minPrice.HasValue)
                {
                    query = query.Where(p => p.Price >= minPrice.Value);
                }

                if (maxPrice.HasValue)
                {
                    query = query.Where(p => p.Price <= maxPrice.Value);
                }

                Properties = await query.AsNoTracking().ToListAsync();
        }
    }
}
