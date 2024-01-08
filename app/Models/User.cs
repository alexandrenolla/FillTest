using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models
{
    public class User
    {
        // Atributos do modelo
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR"), Required, StringLength(64, MinimumLength = 6)]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR"), Required, StringLength(64, MinimumLength = 6)]
        public string Email { get; set; }

        [Column(TypeName = "VARCHAR"), Required, StringLength(11)]
        public string Telephone { get; set; }

        [Column(TypeName = "VARCHAR"), Required, StringLength(12, MinimumLength = 6)]
        public string Password { get; set; }
    }
}