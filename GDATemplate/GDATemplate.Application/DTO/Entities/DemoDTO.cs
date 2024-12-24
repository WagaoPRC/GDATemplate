using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDATemplate.Domain.Entities;

namespace GDATemplate.Application.DTO.Entities
{
    public class DemoDTO
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

        public ICollection<ExampleRelationshipDTO> ExampleRelationshipDTO { get; } = new List<ExampleRelationshipDTO>();
    }
}
