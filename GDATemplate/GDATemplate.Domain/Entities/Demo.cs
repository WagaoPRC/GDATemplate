using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDATemplate.Domain.Entities
{
    public class Demo
    {
        [Key]
        public Guid IdDemo { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        public string Detalhes { get; set; }

        [Required]
        public DateTime DtInclusao { get; set; }

        [InverseProperty("Demo")]
        public ICollection<ExampleRelationship> ExampleRelationship { get; } = new List<ExampleRelationship>();
    }
}
