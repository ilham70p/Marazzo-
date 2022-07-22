using Marazzo.Data;
using Marazzo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.ViewComponents
{
    public class CategoriesVC:ViewComponent
    {
        private readonly AppDbContext _context;

        public CategoriesVC(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> categories = await _context.Categories.Include(u => u.Subcategories).Take(5).ToListAsync();
            return View(categories);
        }
    }
}
