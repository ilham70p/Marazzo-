using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Models
{
    public class Social
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Link { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Image { get; set; }
    }
}
