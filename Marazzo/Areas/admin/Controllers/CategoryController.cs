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
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category model)
        {
            _context.Categories.Add(model);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


        public IActionResult Update(int id)
        {

            return View(_context.Categories.Find(id));
        }

        [HttpPost]
        public IActionResult Update(Category model)
        {
            _context.Categories.Update(model);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            
            Category category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction("index");
        }




    }
}
