using Marazzo.Data;
using Marazzo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Areas.admin.Controllers
{
    [Area("admin")]
    public class FeatureController : Controller
    {
        private readonly AppDbContext _context;

        public FeatureController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Features.ToList());

        }
        
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(Feature model)
        {
            _context.Features.Add(model);
            _context.SaveChanges();
           return RedirectToAction("index");

        }

        public IActionResult Update(int id)
        {
            return View(_context.Features.Find(id));

        }
        [HttpPost]
        public IActionResult Update(Feature model)
        {
            _context.Features.Update(model);
            _context.SaveChanges();
            return RedirectToAction("index");

        }

        public IActionResult Delete(int id)
        {
            Feature model = _context.Features.Find(id);
            _context.Features.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("index");

        }
    }
}
