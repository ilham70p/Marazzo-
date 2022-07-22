using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Models
{
    public class Subcategory
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }

        public Category Category { get; set; }
        public List<Spec> Specs { get; set; }
        public List<Banner> Banners { get; set; }
        public List<Product> Products { get; set; }
    }
}
