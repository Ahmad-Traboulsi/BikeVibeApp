using BikeVibeApp.Data;
using BikeVibeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeVibeApp.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CouponController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{code}")]
        public ActionResult<Coupon> Get(string code)
        {
            var coupon = _context.Coupons.Where(x => x.Code == code &&
                                                   x.IsActive == true &&
                                                   x.ExpiryDate >= DateTime.Today).FirstOrDefault(); ;

            if (coupon == null)
            {
                return NotFound();
            }

            return Ok(coupon);
        }

    }
}
