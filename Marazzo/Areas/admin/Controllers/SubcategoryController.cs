using Marazzo.Data;
using Marazzo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Areas.admin.Controllers
{
    [Area("admin")]
    public class SubcategoryController : Controller
    {
        private readonly AppDbContext _context;

        public SubcategoryController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            List<Subcategory> subcategories = _context.Subcategories.Include(c => c.Category).ToList();
            return View(subcategories);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Subcategory subcategory)
        {
            _context.Subcategories.Add(subcategory);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
