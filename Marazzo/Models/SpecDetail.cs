using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Models
{
    public class SpecDetail
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }

        [ForeignKey("Spec")]
        public int? SpecId { get; set; }

        public Product Product { get; set; }

        public Spec Spec { get; set; }
    }
}
