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
    public class Payments:ViewComponent
    {
        private readonly AppDbContext _context;

        public Payments(AppDbContext context)
        {
            _context = context;
        }




        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<PayMethod> paymethods = await _context.PayMethods.ToListAsync();
            return View(paymethods);
        }
    }
}
