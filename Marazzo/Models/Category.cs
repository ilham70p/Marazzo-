using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        public List<Subcategory> Subcategories { get; set; }
        public List<Advertisement> Advertisements { get; set; }
        public List<Product> Products { get; set; }

    }
}
