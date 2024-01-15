using app.Models;
using app.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace app.Pages.Admin.Properties
{
    public class EditModel : PageModel
    {       
        private readonly IWebHostEnvironment environment;
        private readonly EstateAgencyDbContext context;
        
        public EditModel(IWebHostEnvironment environment, EstateAgencyDbContext context)
        {
            this.environment = environment;
            this.context = context;   
        }

        [BindProperty]
        public RealStateDTO RealStateDTO { get; set; } = new RealStateDTO();
        
        public RealState RealState { get; set; } = new RealState();

        public string errorMessage = "";
        public string successMessage = "";

    
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("Admin/Properties/Index");
                return;
            }

            var property = context.Properties
                .Include(p => p.RealStateImages)
                .FirstOrDefault(p => p.Id == id);

            if (property == null)
            {
                Response.Redirect("Admin/Properties/Index");
                return;
            }

            RealStateDTO.Title = property.Title;
            RealStateDTO.Price = property.Price;
            RealStateDTO.Neighborhood = property.Neighborhood;
            RealStateDTO.BedroomQuantity = property.BedroomQuantity;
            RealStateDTO.BusinessType = property.BusinessType;
            RealStateDTO.Address = property.Address;

            RealState = property;
            
            // Identificador exclusivo para a URL do script do Google Maps parar de bugar com o cache do navegador
            ViewData["GoogleMapsScriptUrl"] = $"https://maps.googleapis.com/maps/api/js?key=AIzaSyBXMVMKpFlxF1uHhrjzFzSlh3VfrTKTV6A&callback=initMapEdit&uniqueId={Guid.NewGuid()}";
        }

        public async Task OnPost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("Admin/Properties/Index");
                return;
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields.";
                return;
            }

            var property = context.Properties.Find(id);
            if (property == null)
            {
                Response.Redirect("Admin/Properties/Index");
                return;
            }

            // Update the image file if we have a new image file
            var images = new List<RealStateImage>();

            if (RealStateDTO.ImageFile != null)
            {
                foreach (var file in RealStateDTO.ImageFile!)
                {   
                    string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    string imageFullPath = environment.WebRootPath + "/photos/" + newFileName;
                    using (var stream = System.IO.File.Create(imageFullPath))
                    {
                        await file.CopyToAsync(stream);
                    }

                    images.Add(new RealStateImage(){
                        ImageFileName = newFileName
                    });
                }
            }

            // Update the property in the database
            property.Title = RealStateDTO.Title;
            property.Price = RealStateDTO.Price;
            property.Neighborhood = RealStateDTO.Neighborhood;
            property.BedroomQuantity = RealStateDTO.BedroomQuantity;
            property.BusinessType = RealStateDTO.BusinessType;
            property.Address = RealStateDTO.Address;
            property.RealStateImages = images;

            await context.SaveChangesAsync();

            RealState = property;

            successMessage = "Product updated successfully";

            Response.Redirect("/Admin/Properties/Index");
        }
    }
}