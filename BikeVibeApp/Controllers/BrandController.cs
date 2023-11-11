using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeVibeApp.Data;
using BikeVibeApp.Models;

namespace BikeVibeApp.Controllers
{
    public class BrandController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrandController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Brands
        public async Task<IActionResult> Index()
        {
            return _context.Brands != null ?
                        View(await _context.Brands.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Brands'  is null.");
        }

        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        public IActionResult Create()
        {
            return View("BrandForm", new Brand());
        }

        public IActionResult Edit(short? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = _context.Brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View("BrandForm", brand);
        }

        [HttpPost]
        public IActionResult Save(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return View("BrandForm",brand);
            }

            if (brand.Id == 0)
            {
                _context.Brands.Add(brand);
                //_context.Add(brand);
            }
            else
            {
                _context.Brands.Update(brand);
            }

            
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
           
        }



    }
}
