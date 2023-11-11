using BikeVibeApp.Data;
using BikeVibeApp.Models;
using BikeVibeApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeVibeApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var customers = _context.Customers.Include(c=> c.MembershipType).ToList();
            return View(customers);
        }

        public IActionResult Create()
        {
            CustomerFormViewModel viewModel = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                 MembershipTypes = _context.MembershipTypes.ToList(),
            };
            return View("CustomerForm",viewModel);
        }

        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null) { return NotFound(); }

            CustomerFormViewModel viewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList(),
            };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public IActionResult Save(Customer customer)
        {

            if (!ModelState.IsValid)
            {
                CustomerFormViewModel viewModel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList(),
                };
                return View("CustomerForm", viewModel);
            }
            
            if(customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Find(customer.Id);

                if (customerInDb != null)
                {
                    customerInDb.Name = customer.Name;
                    customerInDb.BirthDate = customer.BirthDate;
                    customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                    customerInDb.MembershipTypeId = customer.MembershipTypeId;
                }
                else
                {
                    return NotFound();
                }
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
