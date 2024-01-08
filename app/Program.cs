using app.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = "Server=localhost;User ID=root;Password=root;Database=EstateAgencyDb;";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

builder.Services.AddDbContext<EstateAgencyDbContext>(options => 
{
    builder.Configuration.GetConnectionString(connectionString);
    options.UseMySql(connectionString, serverVersion);
});

//builder.Services.AddMySqlDataSource(builder.Configuration.GetConnectionString(connectionString)!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
