using BikeVibeApp.Controllers.api;
using BikeVibeApp.Data;
using BikeVibeApp.Models;
using BikeVibeApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeVibeApp.Controllers
{
    [Authorize]
    public class RentalController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public RentalController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var rentals = _context.Rental.Include(c=> c.Customer).Include(b=> b.Bicycle).ToList();
            return View(rentals);
        }

        public IActionResult Create(int id)
        {

            RentalFormViewModel viewModel = GetViewModel(id, new Rental());

            return View("RentalForm", viewModel);
        }

        public IActionResult Save(Rental rental)
        {


            Coupon coupon = null;

            if(!string.IsNullOrWhiteSpace(rental.CouponCode))
                coupon = _context.Coupons.Where(x => x.Code == rental.CouponCode &&
                                                   x.IsActive == true &&
                                                   x.ExpiryDate >= DateTime.Today).FirstOrDefault();



            if (!ModelState.IsValid || (!string.IsNullOrWhiteSpace(rental.CouponCode) && coupon == null))
            {
                RentalFormViewModel viewModel = GetViewModel(rental.CustomerId, rental);
                return View("RentalForm", viewModel);
            }

            
            
            if (coupon != null)
            {
                coupon.IsActive = false;
                rental.withCoupon = true;
            }

            rental.ExpectedReturnDate = rental.RentalDate.Value.AddDays(rental.RentalDays);

            _context.Add(rental);
            _context.SaveChanges();

            
            
            return RedirectToAction("Index");
        }

        public RentalFormViewModel GetViewModel(int customerId, Rental rental)
        {

            Customer customer = null;
            if (customerId > 0)
                customer = _context.Customers.Include(x => x.MembershipType).SingleOrDefault(x => x.Id == customerId);

            if (customer == null)
                return null;

            rental.Customer = customer;
            rental.CustomerId = customerId;

            return new RentalFormViewModel()
            {
                Rental = rental,
                BicycleTypes = _context.BicycleTypes.ToList()
            };
        }
    }
}
