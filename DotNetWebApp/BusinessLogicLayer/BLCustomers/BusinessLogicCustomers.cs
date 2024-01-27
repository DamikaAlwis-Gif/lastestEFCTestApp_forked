using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseAccessLayer.Postgre.DbContext;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace BusinessLogicLayer.BLCustomers
{
    public class BusinessLogicCustomers : IBusinessLogicCustomers
    {
 
        private readonly AppDbContextICustomerPostgre _context;
        private IMapper _mapper;


        public BusinessLogicCustomers() 
        {
            _context = new AppDbContextICustomerPostgre();
            // Create mapper configuration
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InternalCustomer, ReqResCustomer>();
            });
            // Initialize the _mapper field
            _mapper = mapperConfiguration.CreateMapper();
        }
        public void AddCustomer(InternalCustomer data)
        {
            if(data != null) 
            {
                _context.Icustomers.Add(data);
                _context.SaveChanges();
            }
        }

        public async Task<ReqResCustomer> GetCustomerById(int id)
        {
            InternalCustomer DbCustomer = await _context.Icustomers.FirstOrDefaultAsync(m => m.Id == id);

            ReqResCustomer ResCustomer = _mapper.Map<ReqResCustomer>(DbCustomer);
            ResCustomer.Id = id;

            return ResCustomer;
        }

        public async Task<List<ReqResCustomer>> GetDataList()
        {
            var internalCustomers = await _context.Icustomers.ToListAsync();
            var reqResCustomers = _mapper.Map<List<ReqResCustomer>>(internalCustomers);

            return reqResCustomers;
        }


        public void RemoveCustomer(int id)
        {
            var customerToRemove = _context.Icustomers.FirstOrDefault(c => c.Id == id);
            if (customerToRemove != null)
            {
                _context.Icustomers.Remove(customerToRemove);
                _context.SaveChanges();
            }
        }
        public void UpdateCustomer(ReqResCustomer data)
        {
            var existingCustomer = _context.Icustomers.FirstOrDefault(c => c.Id == data.Id);
            if (existingCustomer != null)
            {
                // Update the properties of existingCustomer with data properties
                existingCustomer.Name = data.Name;
                existingCustomer.Age = data.Age;
                existingCustomer.Address = data.Address;

                _context.SaveChanges();
            }
        }
    }
}
