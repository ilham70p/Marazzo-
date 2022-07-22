using Marazzo.Data;
using Marazzo.Models;
using Marazzo.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.Areas.admin.Controllers
{
    [Area("admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(AppDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            
            return View(_context.Products.Include(c=>c.Category).ThenInclude(s=>s.Subcategories).Include(i=>i.Images).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Categories = _context.Categories.ToList();
     

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {



                string prdModel = JsonConvert.SerializeObject(model);
                HttpContext.Session.SetString("Product", prdModel);
                return RedirectToAction("CreateSpecToProduct");


            }
            else {
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Subcategories = _context.Subcategories.ToList();
                ViewBag.Brands = _context.Brands.ToList();
                
                return View();

            }
         
                
            
        }

        public JsonResult GetSubcategory(int categoryId) {


            List<Subcategory> data = _context.Subcategories.Where(s => s.CategoryId == categoryId).ToList();
            return Json(data);
        
        }











        public IActionResult CreateSpecToProduct()
        {
            string prdModelString = HttpContext.Session.GetString("Product");
            Product prdModel = JsonConvert.DeserializeObject<Product>(prdModelString);
            int? subcategoryid = prdModel.SubcategoryId;
           
            List<Spec> specs = _context.Specs.Where(sp => sp.SubcategoryId == subcategoryid).ToList();
            ViewBag.specs = specs;
            return View();
        }

        [HttpPost]
        public IActionResult CreateSpecToProduct(VmPrdAll model2)
        {
            int ProductId = addProduct();

            addSpecdetail(model2,ProductId);

            addImages(ProductId,model2.Images);
            return RedirectToAction("index");

            
            
        }

        


        public void addSpecdetail(VmPrdAll model,int productId) {

            string prdModelString = HttpContext.Session.GetString("Product");
            Product prdModel = JsonConvert.DeserializeObject<Product>(prdModelString);
            //int productId = prdModel.Id;
            List<Spec> specs = _context.Specs.Where(sp => sp.SubcategoryId == prdModel.SubcategoryId).ToList();
            int i = 0;
            foreach (var item in specs)
            {
                string mymodel = model.SpecDetails[i].Value;

                SpecDetail myspecdetail = new SpecDetail
                {
                    Value = mymodel,
                    ProductId = productId,
                    SpecId = item.Id,
                };
                _context.SpecDetails.Add(myspecdetail);
                _context.SaveChanges();
                i++;
            }


        }


        public int addProduct() {

            string prdModelString = HttpContext.Session.GetString("Product");
            Product prdModel = JsonConvert.DeserializeObject<Product>(prdModelString);

            _context.Products.Add(prdModel);
            _context.SaveChanges();
            int productId = prdModel.Id;
            return productId;
        }

        public void addImages(int productId,IFormFile[] images) {
            int i = 0;
            foreach (var item in images)
            {

                string fileName = Guid.NewGuid() + "-" + images[i].FileName;
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    images[i].CopyTo(stream);
                }

                Image myimage = new Image
                {
                    ImageName = fileName,
                    ProductId = productId
                };

                _context.Images.Add(myimage);
                _context.SaveChanges();


                i++;
            }

            
        }

        public IActionResult Delete(int id)
        {

            Product product = _context.Products.Find(id);

            List<Image> myimages = _context.Images.Where(i => i.ProductId == id).ToList();
            foreach (var item in myimages)
            {
                string? oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads",item.ImageName);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                } 
                
            }


            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("index");


        }
    }
}
