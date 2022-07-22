using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string ImageName { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
