using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Models
{
    public class Spec
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string SpecName { get; set; }

        [ForeignKey("Subcategory")]
        public int? SubcategoryId { get; set; }
        [MaxLength(250)]
        public Subcategory Subcategory { get; set; }
        public List<SpecDetail> SpecDetails { get; set; }
    }
}
