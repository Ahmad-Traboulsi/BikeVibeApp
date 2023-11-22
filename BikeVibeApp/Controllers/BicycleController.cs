using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeVibeApp.Data;
using BikeVibeApp.Models;
using BikeVibeApp.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BikeVibeApp.Controllers
{
    [Authorize]
    public class BicycleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BicycleController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bicycles = _context.Bicycles.Include(b => b.Brand).Include(bt => bt.BicycleType);
            return View(bicycles.ToList());
        }

        public IActionResult New()
        {
            var brands = _context.Brands.ToList();
            var types = _context.BicycleTypes.ToList();
            BicycleFormViewModel viewmodel = new BicycleFormViewModel()
            {
                Bicycle = new Bicycle(),
                Brands = new SelectList(brands, "Id", "Name"),
                BicycleTypes = new SelectList(types, "Id", "Name")
            };

            return View("BicycleForm", viewmodel);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Bicycles == null)
            {
                return NotFound();
            }

            var bicycle = _context.Bicycles.Find(id);
            if (bicycle == null)
            {
                return NotFound();
            }
            var brands = _context.Brands.ToList();
            var types = _context.BicycleTypes.ToList();
            BicycleFormViewModel viewmodel = new BicycleFormViewModel()
            {
                Bicycle = bicycle,
                Brands = new SelectList(brands, "Id", "Name"),
                BicycleTypes = new SelectList(types, "Id", "Name")
            };

            return View("BicycleForm", viewmodel);
        }

        [HttpPost]
        public IActionResult Save(Bicycle bicycle)
        {
            if (!ModelState.IsValid)
            {
                var brands = _context.Brands.ToList();
                var types = _context.BicycleTypes.ToList();
                BicycleFormViewModel viewmodel = new BicycleFormViewModel()
                {
                    Bicycle = bicycle,
                    Brands = new SelectList(brands, "Id", "Name"), //we can create the select list in other places, one option is the view, a better option is build a helper function
                    BicycleTypes = new SelectList(types, "Id", "Name") //we can create the select list in other places, one option is the view, a better option is build a helper function
                };

                return View("BicycleForm", viewmodel);
            }

            if (bicycle.Id == 0)
            {
                bicycle.Dateadded = DateTime.Today;
               _context.Bicycles.Add(bicycle);
            }
            else
            {
                _context.Bicycles.Update(bicycle);
            }
            try
            {
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return RedirectToAction(nameof(Index));
        }




    }
}
