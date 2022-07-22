using Marazzo.Data;
using Marazzo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Areas.admin.Controllers
{
    [Area("admin")]
    public class SocialController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SocialController(AppDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Social> socials = _context.Socials.ToList();
            return View(socials);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Social model)
        {

            if (ModelState.IsValid)
            {
                if (model.ImageFile.ContentType=="image/jpg"|| model.ImageFile.ContentType == "image/jpeg"|| model.ImageFile.ContentType == "image/png")
                {
                    if (model.ImageFile.Length<=345657)
                    {
                        string fileName = Guid.NewGuid() + "-" + model.ImageFile.FileName;
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                        using (var stream=new FileStream(filePath, FileMode.Create))
                        {
                            model.ImageFile.CopyTo(stream);
                            
                        }
                        model.Image = fileName;
                        _context.Socials.Add(model);
                        _context.SaveChanges();
                        return RedirectToAction("index");
                    }
                    else
                    {
                        return View(model);
                    }
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        
        public IActionResult Update(int id)
        {
            Social model = _context.Socials.Find(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(Social model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile!=null)
                {
                    if (model.ImageFile.ContentType=="image/jpg"|| model.ImageFile.ContentType == "image/jpeg"|| model.ImageFile.ContentType == "image/png")
                    {
                        if (model.ImageFile.Length<=356489)
                        {
                            string oldImagepath =Path.Combine(_webHostEnvironment.WebRootPath,"Uploads",model.Image);
                            if (System.IO.File.Exists(oldImagepath))
                            {
                                System.IO.File.Delete(oldImagepath);
                            }

                            string fileName = Guid.NewGuid() + "-" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_webHostEnvironment.WebRootPath,"Uploads",fileName);
                            using (var stream = new FileStream(filePath,FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }
                            model.Image = fileName;
                            _context.Socials.Update(model);
                            _context.SaveChanges();
                            return RedirectToAction("index");
                        }
                        else
                        {
                            return View(model);
                        }
                    }
                    else
                    {
                        return View(model);
                    }
                }
                else
                {
                    _context.Socials.Update(model);
                    _context.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult Delete(int id)
        {
            Social model = _context.Socials.Find(id);
            if (model.ImageFile!=null)
            {
                string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,"Uploads",model.Image);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                _context.Socials.Remove(model);
                _context.SaveChanges();
                return RedirectToAction("index");
            }
            return View();
        }
    }
}
