using Marazzo.Data;
using Marazzo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marazzo.ViewComponents
{
    public class SocialVC:ViewComponent
    {
        private readonly AppDbContext _context;

        public SocialVC(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Social> socials = _context.Socials.ToList();
            return View(socials);
        }

    }
}
