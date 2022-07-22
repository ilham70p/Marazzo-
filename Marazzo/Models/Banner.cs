using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Models
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50),Required]
        public string Title { get; set; }
        [MaxLength(50), Required]
        public string Name { get; set; }
        [MaxLength(50), Required]
        public string Description { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [ForeignKey("Subcategory")]
        public int? SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

    }
}
