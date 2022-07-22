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
    public class PaymentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PaymentController(AppDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {

            return View(_context.PayMethods.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PayMethod model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile.ContentType=="image/jpg"|| model.ImageFile.ContentType == "image/jpeg"|| model.ImageFile.ContentType == "image/png")
                {
                    if (model.ImageFile.Length<=355555)
                    {
                        string fileName = Guid.NewGuid() + "-" + model.ImageFile.FileName;
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);
                        using (var stream=new FileStream(filePath,FileMode.Create))
                        {
                            model.ImageFile.CopyTo(stream);
                        }

                        model.Image = fileName;
                        _context.PayMethods.Add(model);
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
            return View();
        }

        public IActionResult Update(int id)
        {

            return View(_context.PayMethods.Find(id));

        }
        [HttpPost]
        public IActionResult Update(PayMethod model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    if (model.ImageFile.ContentType == "image/jpg" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/png")
                    {

                        if (model.ImageFile.Length <= 355555)
                        {
                            string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", model.Image);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                            string fileName = Guid.NewGuid() + "-" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_webHostEnvironment.WebRootPath,"Uploads",fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }
                            model.Image = fileName;
                            _context.PayMethods.Update(model);
                            _context.SaveChanges();
                            return RedirectToAction("index");
                        }
                        else {
                            return View(model);
                        }
                    }
                    else {
                        return View(model);
                    }
                }
                else {
                    _context.PayMethods.Update(model);
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
            PayMethod oldmodel = _context.PayMethods.Find(id);
            string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", oldmodel.Image);
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _context.PayMethods.Remove(oldmodel);
            _context.SaveChanges();
            return RedirectToAction("index");

        }
    }
}
