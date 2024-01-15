using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace app.Models
{
    public class RealState
    {
        // Atributos do modelo 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR"), Required, StringLength(64, MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        [Precision(16,2)]
        public decimal Price { get; set; }

        [Column(TypeName = "VARCHAR"), Required, StringLength(40, MinimumLength = 4)]
        public string Neighborhood { get; set; }

        [Required]
        public int BedroomQuantity { get; set; }
    
        [Column(TypeName = "VARCHAR"), Required, StringLength(7, MinimumLength = 5)]
        public string BusinessType { get; set; }

        [Column(TypeName = "VARCHAR"), Required, StringLength(300, MinimumLength = 4)]
        public string Address { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
        
        // Relacionamento
        public virtual List<RealStateImage> RealStateImages { get; set; }

    }
}