using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public float Price { get; set; }       
        public List<SpecDetail> SpecDetails { get; set; }
        [ForeignKey("Brand")]
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        [ForeignKey("Subcategory")]
        public int? SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
        public List<Image> Images { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
