using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace app.Models
{
    public class RealStateDTO
    {
        [Required, StringLength(64, MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required, StringLength(40, MinimumLength = 4)]
        public string Neighborhood { get; set; }

        [Required]
        public int BedroomQuantity { get; set; }
    
        [Required, StringLength(7, MinimumLength = 5)]
        public string BusinessType { get; set; }

        [Required, StringLength(300, MinimumLength = 4)]
        public string Address { get; set; }

        public IFormFile[]? ImageFile { get; set; }
    }
}