using BikeVibeApp.Data;
using BikeVibeApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeVibeApp.Controllers.api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<JsonResult> Rentals()
        {
            var pendingReturn = _context.Rental.Where(r => r.isReturned == false);
          
            var totalRentals = _context.Rental.Count();
            var couponsUsed = _context.Rental.Where(r => r.withCoupon == true).Count();
            var pendingReturns = pendingReturn.Count();
            var overDueReturns = pendingReturn.Where(r => r.ExpectedReturnDate <= DateTime.Today).Count();
            
            return new JsonResult(
                   new { TotalRentals = totalRentals,
                         CouponsUsed = couponsUsed,
                         PendingReturns = pendingReturns,
                         OverDueReturns = overDueReturns});
        }

        [HttpGet]
        public async Task<JsonResult> Customers()
        {
            var customerCount = _context.Customers.Count();
            var subCount = _context.Customers.Where(c => c.IsSubscribedToNewsLetter == true).Count();
            var withMemberships = _context.Customers.Where(c => c.MembershipTypeId > 1).Count();    

            return new JsonResult(
                   new { CustomerCount = customerCount,
                        SubscribedCount = subCount,     
                        WithMemberships = withMemberships,
                        WithoutMemberships = customerCount - withMemberships
            });
        }

        public async Task<JsonResult> MembershipType()
        {
            var q = _context.Customers
                    .GroupBy(x => x.MembershipType.Name)
                    .Select(group => new
                    {
                        Label = group.Key,
                        Count = group.Count()
                    })
                    .ToList();

            return new JsonResult(q);
        }

        public async Task<JsonResult> AgeGroups()
        {
            var ageGroupCounts = _context.Customers.ToList()
                                .GroupBy(c => CalculateAgeGroup(c.BirthDate))
                                .Select(g => new
                                {
                                    AgeGroup = g.Key,
                                    Count = g.Count()
                                })
                                .ToList();

            return new JsonResult(ageGroupCounts);
        }

        private string CalculateAgeGroup(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;

            // Check if the customer's birthday has occurred this year
            if (dateOfBirth.Date > today.AddYears(-age))
                age--;

            string ageGroup;

            // Define your age group criteria and corresponding labels
            if (age < 18)
            {
                ageGroup = "Under 18";
            }
            else if (age >= 18 && age <= 25)
            {
                ageGroup = "18-25";
            }
            else if (age >= 26 && age <= 35)
            {
                ageGroup = "26-35";
            }
            else if (age >= 36 && age <= 45)
            {
                ageGroup = "36-45";
            }
            else if (age >= 46 && age <= 55)
            {
                ageGroup = "46-55";
            }
            else
            {
                ageGroup = "Over 55";
            }

            return ageGroup;
        }

        public async Task<JsonResult> RentalsFrequency()
        {
            var q = _context.Rental.Where(r => r.RentalDate >= DateTime.Today.AddMonths(-3))
                                  .GroupBy(x => x.RentalDate.Value.Date)
                                  .Select(group => new { 
                                      date = group.Key.ToShortDateString(),
                                      Count = group.Count()
                                  }).ToList();

            return new JsonResult(q);
        }

        public async Task<JsonResult> BikeDemand()
        {
            var q = _context.Rental.Where(r=> r.RentalDate>= DateTime.Today.AddMonths(-3)).GroupBy(r => r.Bicycle.Name)
                                  .Select(group => new { 
                                      Name = group.Key,
                                      Count = group.Count()
                                  }).ToList();

            return new JsonResult(q);
        }

    }
}
