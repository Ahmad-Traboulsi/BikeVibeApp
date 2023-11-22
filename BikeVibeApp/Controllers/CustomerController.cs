using BikeVibeApp.Data;
using BikeVibeApp.Models;
using BikeVibeApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeVibeApp.Controllers
{
    [Authorize]
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

        /// <summary>
        /// Checks for user input's validity, if new customer add to the database, else update the customer
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Customer customer)
        {
            //Checking the model validity before action
            if (!ModelState.IsValid)
            {
                //if not valid redirect to the form with posted data
                CustomerFormViewModel viewModel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList(),
                };
                return View("CustomerForm", viewModel);
            }
            
            //checking if the customer is new or old
            if(customer.Id == 0)
            {
                //if new add customer
                _context.Customers.Add(customer);
            }
            else
            {
                //fetch the customer from the database
                var customerInDb = _context.Customers.Find(customer.Id);

                //if found edit the data
                if (customerInDb != null)
                {
                    customerInDb.Name = customer.Name;
                    customerInDb.BirthDate = customer.BirthDate;
                    customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                    customerInDb.MembershipTypeId = customer.MembershipTypeId;
                }
                else //report error
                {
                    return NotFound();
                }
            }

            //save and redirect
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
