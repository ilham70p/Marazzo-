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
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View(_context.Settings.ToList());
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Setting model)
        {
            _context.Settings.Add(model);
            _context.SaveChanges();
            return RedirectToAction("index");
        }


        public IActionResult Update(int id)
        {
            return View(_context.Settings.Find(id));
        }
        [HttpPost]
        public IActionResult Update(Setting model)
        {
            _context.Settings.Update(model);
            _context.SaveChanges();
            return RedirectToAction("index");
        }


        public IActionResult Delete(int id)
        {
            Setting model = _context.Settings.Find(id);
            _context.Settings.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
