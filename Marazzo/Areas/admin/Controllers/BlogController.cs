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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogController(AppDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Blog> blogs = _context.Blogs.ToList();
            return View(blogs);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                if (blog.ImageFile.ContentType == "image/jpeg" || blog.ImageFile.ContentType == "image/png" || blog.ImageFile.ContentType == "image/jpg")
                {
                    if (blog.ImageFile.Length <= 3145728)
                    {

                        string fileName =Guid.NewGuid()+"-"+ blog.ImageFile.FileName;
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);
                        using (var stream = new FileStream(filePath,FileMode.Create))
                        {
                            blog.ImageFile.CopyTo(stream);
                        }
                        blog.BannerImage = fileName;
                        _context.Blogs.Add(blog);
                        _context.SaveChanges();
                        return RedirectToAction("index");
                    }
                    else {
                        ModelState.AddModelError("", "Image is too big");
                        return View(blog);
                    }
                   
                }
                else {
                    ModelState.AddModelError("", "Type of image file is not supported");
                    return View(blog);
                
                }
            }

            else
            {
                return View(blog);
            }
           
        }

        public IActionResult Update(int id)
        {
            return View(_context.Blogs.Find(id));
        }
        [HttpPost]
        public IActionResult Update(Blog blog)
        {
            if (ModelState.IsValid)
            {
                if (blog.ImageFile != null)
                {
                    if (blog.ImageFile.ContentType == "image/jpeg" || blog.ImageFile.ContentType == "image/png" || blog.ImageFile.ContentType == "image/jpg")
                    {
                        if (blog.ImageFile.Length <= 3145728)
                        {
                            string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", blog.BannerImage);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }




                            string fileName = Guid.NewGuid() + "-" + blog.ImageFile.FileName;
                            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                blog.ImageFile.CopyTo(stream);
                            }
                            blog.BannerImage = fileName;
                            _context.Blogs.Update(blog);
                            _context.SaveChanges();
                            return RedirectToAction("index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Image is too big");
                            return View(blog);
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Type of image file is not supported");
                        return View(blog);

                    }
                }
                else {

                    _context.Blogs.Update(blog);
                    _context.SaveChanges();
                    return RedirectToAction("index");

                }


                
            }

            else
            {
                return View(blog);
            }

        }

        public IActionResult Delete(int id)
        {
            Blog funblog = _context.Blogs.Find(id);
            string? oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", funblog.BannerImage);
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
                _context.Blogs.Remove(funblog);
                _context.SaveChanges();
            }
            else {
                _context.Blogs.Remove(funblog);
                _context.SaveChanges();
            }


            return RedirectToAction("index");
        }


    }
}
