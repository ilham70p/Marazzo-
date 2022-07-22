using Marazzo.Data;
using Marazzo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
           _context = context;
        }
        public IActionResult Index()
        {
            ProCateVM procate = new ProCateVM { Products = _context.Products.Include(i => i.Images).ToList(),Categories=_context.Categories.Include(s=>s.Subcategories).ToList() };
            return View(procate);
        }

        public IActionResult ProductBySubcategory(int id)
        {
           ProCateVM procate = new ProCateVM { Products = _context.Products.Include(i => i.Images).Where(c=>c.SubcategoryId==id).ToList(), Categories = _context.Categories.Include(a => a.Subcategories).ToList() };
            return View(procate);
        }


    }
}
