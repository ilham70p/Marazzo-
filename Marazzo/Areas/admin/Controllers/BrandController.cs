﻿using Marazzo.Data;
using Marazzo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Areas.admin.Controllers
{   [Area("admin")]
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandController(AppDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Brand> brands = _context.Brands.Take(8).ToList();
            return View(brands);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Brand model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile.ContentType == "image/jpg" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/png")
                {
                    if (model.ImageFile.Length<=312456)
                    {
                        string fileName = Guid.NewGuid() + "" + model.ImageFile.FileName;
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath,"Uploads",fileName);
                        using (var stream=new FileStream(filePath,FileMode.Create))
                        {
                            model.ImageFile.CopyTo(stream);
                        }

                        model.Image = fileName;
                        _context.Brands.Add(model);
                        _context.SaveChanges();
                        return RedirectToAction("index");
                    }
                    else
                    {
                        ModelState.AddModelError("","File is too large to accept");
                        return View(model);
                    }
                }
                else {
                    ModelState.AddModelError("", "This type of files are not supported");
                    return View(model);
                }
            }
            else { 
            return View(model);

            }
        }

        public IActionResult Update(int id)
        {
            
            return View(_context.Brands.Find(id));
        }
        [HttpPost]
        public IActionResult Update(Brand model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    if (model.ImageFile.ContentType == "image/jpg" || model.ImageFile.ContentType == "image/png" || model.ImageFile.ContentType == "image/jpeg")
                    {
                        if (model.ImageFile.Length <= 345645)
                        {
                            string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", model.Image);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }


                            string fileName = Guid.NewGuid() + "-" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);

                            }
                            model.Image = fileName;
                            _context.Brands.Update(model);
                            _context.SaveChanges();
                           return RedirectToAction("index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "File is too big to save");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "File type is not supported");
                        return View(model);
                    }
                }
                else {

                    _context.Brands.Update(model);
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
            Brand funblog = _context.Brands.Find(id);
            string? oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", funblog.Image);
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
                _context.Brands.Remove(funblog);
                _context.SaveChanges();
            }
            else
            {
                _context.Brands.Remove(funblog);
                _context.SaveChanges();
            }


            return RedirectToAction("index");
        }
    }
}
