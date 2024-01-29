using DataAccessLayer.DbContext;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLayer.InterFaces
{
    public class CustomerRepository : ICustomerRepository
    {
        private MapperConfiguration mapperConfig;
        private IMapper _mapper;
        private readonly AppDbContextICustomerPostgre _context;

        public CustomerRepository()
        {
            _context = new AppDbContextICustomerPostgre();
            InitializeMapper();
        }

        public async void CreateCustomerAsync(InternalCustomer customer)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
        }

        public void DeleteCustomerAsync(int id)
        {
            var customerToRemove = _context.Icustomers.FirstOrDefault(c => c.Id == id);
            if (customerToRemove != null)
            {
                _context.Icustomers.Remove(customerToRemove);
                _context.SaveChanges();
            }
        }

        public async Task<ReqResCustomer> GetReqResCustomerByIdAsync(int id)
        {
            var internalCustomer = await _context.Icustomers.FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<ReqResCustomer>(internalCustomer);
        }

        public async Task<IEnumerable<ReqResCustomer>> GetInternalCustomersAsync()
        {
            var internalCustomer = await _context.Icustomers.ToListAsync();
            return _mapper.Map<IEnumerable<ReqResCustomer>>(internalCustomer);
        }

        public void UpdateCustomerAsync(InternalCustomer customer)
        {
            var existingCustomer = _context.Icustomers.FirstOrDefault(c => c.Id == customer.Id);
            if (existingCustomer != null)
            {
                // Update the properties of existingCustomer with data properties
                existingCustomer.Name = customer.Name;
                existingCustomer.Age = customer.Age;
                existingCustomer.Address = customer.Address;

                _context.SaveChanges();
            }
        }
        public bool InternalCustomerExists(int id)
        {
            return _context.Icustomers.Any(e => e.Id == id);
        }

        private void InitializeMapper()
        {
           mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InternalCustomer, ReqResCustomer>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        public async Task<InternalCustomer?> GetInternalCustomerByIdAsync(int id)
        {
            return await _context.Icustomers.FirstOrDefaultAsync(m => m.Id == id);
        }

   
    }
}
