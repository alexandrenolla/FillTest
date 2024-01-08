using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models
{   
    public class RealStateImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ImageFileName { get; set; }

        [Required, ForeignKey("RealStateId")]
        public int RealStateId { get; set; }

        // Declaração do relacionamento
        public virtual RealState RealState { get; set; }
    }
}