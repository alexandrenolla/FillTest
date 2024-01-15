using app.Models;
using app.Services;
using app.Utilites.GeoCoding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace app.Pages.Admin.Properties
{
    public class CreateModel : PageModel
    {   
        private readonly IWebHostEnvironment environment;
        private readonly EstateAgencyDbContext context;

        [BindProperty] 
        public RealStateDTO RealStateDTO { get; set; } = new RealStateDTO();

        public CreateModel(IWebHostEnvironment environment, EstateAgencyDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        public void OnGet()
        {
            ViewData["GoogleMapsScriptUrl"] = $"https://maps.googleapis.com/maps/api/js?key=AIzaSyBXMVMKpFlxF1uHhrjzFzSlh3VfrTKTV6A&callback=initMapEdit&uniqueId={Guid.NewGuid()}";
        }

        public string errorMessage = "";
        public string successMessage = "";
        public async Task OnPost()
        {
            if (RealStateDTO.ImageFile == null)
            {
                ModelState.AddModelError("RealStateDTO.ImageFile", "The image file is required.");
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields.";
                return;
            }

            var images = new List<RealStateImage>();

            // Salva o arquivo da imagem
            foreach (var file in RealStateDTO.ImageFile!)
            {   
                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                // newFileName += Path.GetExtension(file.FileName);

                string imageFullPath = environment.WebRootPath + "/photos/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                   await file.CopyToAsync(stream);
                }

                images.Add(new RealStateImage(){
                    ImageFileName = newFileName
                });
            }

            // Save the new property in the database
            RealState realState = new RealState()
            {
                Title = RealStateDTO.Title,
                Price = RealStateDTO.Price,
                Neighborhood = RealStateDTO.Neighborhood,
                BedroomQuantity = RealStateDTO.BedroomQuantity,
                BusinessType = RealStateDTO.BusinessType,
                Address = RealStateDTO.Address,
                RealStateImages = images // Associate the imagens to the property
    
            };
            
            var coordenates = await new GoogleMapsGeoCoding().GetCoordinates(realState.Address);

            if (coordenates != null) {
                realState.Latitude = coordenates.Latitude;
                realState.Longitude = coordenates.Longitude;
            }

            context.Properties.Add(realState);
            await context.SaveChangesAsync();

            // Limpa o formulário
            RealStateDTO.Title = "";
            RealStateDTO.Price = 0;
            RealStateDTO.Neighborhood = "";
            RealStateDTO.BedroomQuantity = 0;
            RealStateDTO.BusinessType = "";
            RealStateDTO.Address = "";
            RealStateDTO.ImageFile = null;

            ModelState.Clear();

            successMessage = "Property created successfully.";
        }
    }
}