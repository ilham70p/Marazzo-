using Marazzo.Data;
using Marazzo.Models;
using Marazzo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
      
        public IActionResult Index()
        {
            List<Product> products = _context.Products.Include(i=>i.Images).Take(5).ToList();
            List<Category> categories = _context.Categories.Include(a=>a.Subcategories).ToList();
            List<Banner>  banners= _context.Banners.ToList();
            List<Blog> blogs = _context.Blogs.ToList();
            List<Brand> brands = _context.Brands.ToList();
            List<Feature> features = _context.Features.ToList();
            List<PayMethod> payMethods = _context.PayMethods.ToList();
            List<Setting> settings = _context.Settings.ToList();
            List<Social> socials = _context.Socials.ToList();
            List<Advertisement> advertisements = _context.Advertisements.Take(3).ToList();
            



            ProCateVM data = new ProCateVM() {
                Products = products,
                Categories = categories,
                Banners = banners,
                Blogs = blogs,
                Brands = brands,
                Features = features,
                PayMethods = payMethods,
                Settings = settings,
                Socials = socials,
                Advertisements = advertisements
            }; 
           
            return View(data);
        }

    
    }
}
