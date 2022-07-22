using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Models
{
    public class Advertisement
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Image { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        [ForeignKey("category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
