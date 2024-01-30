using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Customer;
using DataAccessLayer.InterFaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerServices _services;
        

        public CustomerController(ICustomerServices customerServices)
        {
            _services = customerServices;
        }
        //GEt:customer/index
        //[Route("hi/bye")]
        public async Task<IActionResult> Index()
        {
            var Customers = await _services.GetInternalCustomersAsync();
            return View(Customers);
            
        }
        // GET: CustomersController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _services.GetReqResCustomerByIdAsync(id));
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Password,Age,Address")] InternalCustomer internalCustomer)
        {
            if (ModelState.IsValid)
            {
                _services.CreateCustomerAsync(internalCustomer);
                return RedirectToAction(nameof(Index));
            }
            return View(internalCustomer);
        }

        // GET: CustomersController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internalCustomer = await _services.GetInternalCustomerByIdAsync(id);
            if (internalCustomer == null)
            {
                return NotFound();
            }
            return View(internalCustomer);
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Password,Age,Address")] InternalCustomer internalCustomer)
        {
            if (id != internalCustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _services.UpdateCustomerAsync(internalCustomer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_services.InternalCustomerExists(internalCustomer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(internalCustomer);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reqResCustomer = await _services.GetInternalCustomerByIdAsync((int)id);
            if (reqResCustomer == null)
            {
                return NotFound();
            }

            _services.DeleteCustomerAsync(reqResCustomer.Id);
            return View(reqResCustomer);
        }

        // POST: InternalCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var internalCustomer = await _services.GetInternalCustomerByIdAsync(id);
            if (internalCustomer != null)
            {
                _services.DeleteCustomerAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
