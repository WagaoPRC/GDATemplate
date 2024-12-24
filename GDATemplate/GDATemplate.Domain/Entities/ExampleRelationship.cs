using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDATemplate.Domain.Entities
{
    public class ExampleRelationship
    {
        [Key]
        public Guid IdExampleRelationship { get; set; }
        public Guid IdDemo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string DescriptionType { get; set; }

        [ForeignKey("IdDemo")]
        [InverseProperty("ExampleRelationship")]
        public Demo Demo { get; set; } = null!;




    }
}
