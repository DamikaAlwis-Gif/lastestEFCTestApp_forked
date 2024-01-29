using DataAccessLayer.InterFaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Customer
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerServices(ICustomerRepository customerRepository)
        {
            _customerRepo = customerRepository;
        }

        public void CreateCustomerAsync(InternalCustomer customer)
        {
            _customerRepo.CreateCustomerAsync(customer);
        }

        public void DeleteCustomerAsync(int id)
        {
            _customerRepo.DeleteCustomerAsync(id);
        }

        public async Task<InternalCustomer> GetInternalCustomerByIdAsync(int id)
        {
            return await _customerRepo.GetInternalCustomerByIdAsync(id);
        }

        public async Task<IEnumerable<InternalCustomer>> GetInternalCustomersAsync()
        {
            return await _customerRepo.GetInternalCustomersAsync();
        }

        public bool InternalCustomerExists(int id)
        {
            return _customerRepo.InternalCustomerExists(id);
        }

        public void UpdateCustomerAsync(InternalCustomer customer)
        {
            _customerRepo.UpdateCustomerAsync(customer);
        }
    }
}
