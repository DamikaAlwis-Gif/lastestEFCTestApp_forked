using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.BLCustomers;
using DataBaseAccessLayer.Postgre.DbContext;
using Microsoft.EntityFrameworkCore;
using Models;

namespace PresentationLayer.Controllers
{
    public class CustomersController : Controller
    {
        private readonly BusinessLogicCustomers _services;
        private readonly AppDbContextICustomerPostgre _context;


        public CustomersController()
        {
            _services = new BusinessLogicCustomers();
            _context = new AppDbContextICustomerPostgre();
        }
        

    
        // GET: CustomersController
        public async Task<ActionResult> Index()
        {
            return View(await _services.GetDataList());
        }

        // GET: CustomersController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _services.GetCustomerById(id));
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
                _context.Add(internalCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(internalCustomer);
        }

        // GET: CustomersController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internalCustomer = await _context.Icustomers.FindAsync(id);
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
                    _context.Update(internalCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternalCustomerExists(internalCustomer.Id))
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

            var reqResCustomer = await _services.GetCustomerById((int)id);
            if (reqResCustomer == null)
            {
                return NotFound();
            }

            _services.RemoveCustomer(reqResCustomer.Id);
            return View(reqResCustomer);
        }

        // POST: InternalCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var internalCustomer = await _context.Icustomers.FindAsync(id);
            if (internalCustomer != null)
            {
                _context.Icustomers.Remove(internalCustomer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool InternalCustomerExists(int id)
        {
            return _context.Icustomers.Any(e => e.Id == id);
        }
    }
}
