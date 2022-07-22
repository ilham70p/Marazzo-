using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Models
{
    public class Feature
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public string Icon { get; set; }

    }
}
