using app.Models;
using app.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace app.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly EstateAgencyDbContext context;
    public List<RealState> Properties { get; set; } = new List<RealState>(); 

    public IndexModel(ILogger<IndexModel> logger, EstateAgencyDbContext context)
    {
        _logger = logger;
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
