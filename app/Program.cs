using app.Services;
using app.Utilites.GeoCoding;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<GoogleMapsGeoCoding>();

var connectionString = "Server=localhost;User ID=root;Password=root;Database=EstateAgencyDb;";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

builder.Services.AddDbContext<EstateAgencyDbContext>(options =>
{
    options.UseMySql(connectionString, serverVersion);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Remove the authentication and authorization middleware
// app.UseAuthentication();
// app.UseAuthorization();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<EstateAgencyDbContext>();

    dbContext.Database.Migrate();

    // Remove the SeedData call
    // await SeedData.EnsureSeedData(serviceProvider, userManager);
}

app.Run();
