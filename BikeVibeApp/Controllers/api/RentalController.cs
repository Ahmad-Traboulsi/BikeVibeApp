using BikeVibeApp.Data;
using BikeVibeApp.Models;
using BikeVibeApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeVibeApp.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RentalController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> PostRental([FromBody] Rental rental)
        {
            if (_context.Rental == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rental'  is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Information");
            }

            Coupon coupon = null;

            if (!string.IsNullOrWhiteSpace(rental.CouponCode))
                coupon = _context.Coupons.Where(x => x.Code == rental.CouponCode &&
                                                   x.IsActive == true &&
                                                   x.ExpiryDate >= DateTime.Today).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(rental.CouponCode) && coupon == null)
            {
                return BadRequest("Invalid Coupon Code");
            }

            if (coupon != null)
            {
                coupon.IsActive = false;
                rental.withCoupon = true;
            }
            rental.RentalDate = DateTime.Today;
            rental.ExpectedReturnDate = rental.RentalDate.Value.AddDays(rental.RentalDays);

            _context.Add(rental);
            _context.SaveChanges();

            return Ok();
        }
    }
}
