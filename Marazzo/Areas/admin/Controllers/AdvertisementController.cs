using Marazzo.Data;
using Marazzo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Areas.admin.Controllers
{
    [Area("admin")]
    public class AdvertisementController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdvertisementController(AppDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Advertisement> advertisements = _context.Advertisements.Include(a=>a.Category).ToList();

            return View(advertisements);
        }
        public IActionResult Create()
        {
       
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Advertisement model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile.ContentType == "image/jpg" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/png")
                {
                    if (model.ImageFile.Length<=3455795)
                    {
                        string fileName = Guid.NewGuid() + "-" + model.ImageFile.FileName;
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            model.ImageFile.CopyTo(stream);
                        }
                        model.Image = fileName;
                        _context.Advertisements.Add(model);
                        _context.SaveChanges();
                        return RedirectToAction("index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "File is too big to save");

                        return View(model);
                    }
                }
                else {
                    return View(model);
                }
            }
            else {
                return View(model);
            }
        }

        public IActionResult Update(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            Advertisement advertisement = _context.Advertisements.Find(id);
            return View(advertisement);
        }
        [HttpPost]
        public IActionResult Update(Advertisement model)
        {

            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {

                    if (model.ImageFile.ContentType == "image/jpg" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/png")
                    {
                        if (model.ImageFile.Length <= 3556651)
                        {
                            string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", model.Image);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                            string fileName = Guid.NewGuid() + "" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads",fileName);

                          
                            using (var stream=new FileStream(filePath,FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }
                            model.Image = fileName;
                            _context.Advertisements.Update(model);
                            _context.SaveChanges();
                            return RedirectToAction("index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Sebet gotunnen gilas tokulmur");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Apar sox gotuve");
                        return View(model);
                    }

                }
                else
                {
                    _context.Advertisements.Update(model);
                    _context.SaveChanges();
                    return RedirectToAction("index");
                }


            }
            else {
                return View(model);
            }

        }


        public IActionResult Delete(int id)
        {

            Advertisement advertisement = _context.Advertisements.Find(id);
            string? oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads",advertisement.Image);
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
                _context.Advertisements.Remove(advertisement);
                _context.SaveChanges();
            }
            else
            {
                _context.Advertisements.Remove(advertisement);
                _context.SaveChanges();
            }
            return RedirectToAction("index");
        }

    }
}
