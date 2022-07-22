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
    public class SpecialityController : Controller
    {
        private readonly AppDbContext _context;

        public SpecialityController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View(_context.Specs.Include(s=>s.Subcategory).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Subcategories = _context.Subcategories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Spec model)
        {
            _context.Specs.Add(model);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Update(int id)
        {
            ViewBag.Subcategories = _context.Subcategories.ToList();
            return View(_context.Specs.Find(id));
        }
        [HttpPost]
        public IActionResult Update(Spec model)
        {
            _context.Specs.Update(model);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Spec spec = _context.Specs.Find(id);
            _context.Specs.Remove(spec);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
