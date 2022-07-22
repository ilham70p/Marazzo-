
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string BannerImage { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [MaxLength(100),Required(ErrorMessage ="You should have a Title")]
        public string Title { get; set; }
        [Column(TypeName ="nText"),Required]
        public string Content { get; set; }
    }
}
