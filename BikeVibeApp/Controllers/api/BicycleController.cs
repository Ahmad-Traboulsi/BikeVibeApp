using BikeVibeApp.Data;
using BikeVibeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeVibeApp.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BicycleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BicycleController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bicycle>>> GetAvailableByTypeAndSize(string size, int type)
        {
            if (_context.Bicycles == null)
            {

                return NotFound();
            }

            return await _context.Bicycles.Where(x=> x.BicycleTypeId == type && x.Size == size && x.NumberInStock > 0).Include(x=> x.Brand).ToListAsync();
        }

    }
}
