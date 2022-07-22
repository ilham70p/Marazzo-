using Marazzo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.ViewModels
{
    public class ProCateVM
    {
       
       public List<Category> Categories { get; set; }

        public List<Banner> Banners { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Feature> Features { get; set; }
        public List<PayMethod> PayMethods { get; set; }
        public List<Setting> Settings { get; set; }
        public List<Social> Socials { get; set; }
        public List<Advertisement> Advertisements { get; set; }
        public List<Product> Products { get; set; }


    }
}
