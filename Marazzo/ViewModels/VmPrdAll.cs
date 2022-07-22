using Marazzo.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.ViewModels
{
    public class VmPrdAll
    {
        public IFormFile[] Images { get; set; }
        public SpecDetail[] SpecDetails { get; set; }
    }
}
