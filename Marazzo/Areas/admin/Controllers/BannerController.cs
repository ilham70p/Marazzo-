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
    public class BannerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BannerController(AppDbContext context,IWebHostEnvironment webHostEnvironment )
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Banner> banners = _context.Banners.ToList();
            return View(banners);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Banner model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile.ContentType == "image/jpg" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/png")
                {
                    if (model.ImageFile.Length <= 3145728)
                    {

                        string fileName = System.Guid.NewGuid() + "" + model.ImageFile.FileName;
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath,"Uploads", fileName);
                        using (var stream=new FileStream(filePath,FileMode.Create))
                        {
                            model.ImageFile.CopyTo(stream);
                        }
                        model.Image = fileName;
                        _context.Banners.Add(model);
                        _context.SaveChanges();
                        return RedirectToAction("index");





                    }
                    else {
                        ModelState.AddModelError("","Size of the file is too big");
                        return View(model);
                    }
                }
                else {

                    ModelState.AddModelError("", "Type of image file is not supported");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
            return View();
        }
        public IActionResult Update(int id)
        {
            return View(_context.Banners.Find(id));
        }
        [HttpPost]
        public IActionResult Update(Banner model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    if (model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/png" || model.ImageFile.ContentType == "image/jpg")
                    {
                        if (model.ImageFile.Length <= 3145728)
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
                            _context.Banners.Update(model);
                            _context.SaveChanges();
                            return RedirectToAction("index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Image is too big");
                            return View(model);
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Type of image file is not supported");
                        return View(model);

                    }
                }
                else
                {

                    _context.Banners.Update(model);
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
            Banner funbanner = _context.Banners.Find(id);
            string? oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", funbanner.Image);
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
                _context.Banners.Remove(funbanner);
                _context.SaveChanges();
            }
            else
            {
                _context.Banners.Remove(funbanner);
                _context.SaveChanges();
            }


            return RedirectToAction("index");
        }

    }
}
