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
    public class SettingsVC:ViewComponent
    {
        private readonly AppDbContext _context;

        public SettingsVC(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Setting> settings = await _context.Settings.Take(3).ToListAsync();
            return View(settings);
        }
    }
}
