using app.Models;
using app.Services;
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

        }

        public string errorMessage = "";
        public string successMessage = "";
        public void OnPost()
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
               string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(file.FileName);

                string imageFullPath = environment.WebRootPath + "/photos/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                   file.CopyTo(stream);
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

            context.Properties.Add(realState);
            context.SaveChanges();

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