using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Customer
{
    public interface ICustomerServices
    {
        Task<IEnumerable<InternalCustomer>> GetInternalCustomersAsync();
        Task<InternalCustomer> GetInternalCustomerByIdAsync(int id);
        void UpdateCustomerAsync(InternalCustomer customer);
        void CreateCustomerAsync(InternalCustomer customer);
        void DeleteCustomerAsync(int id);
        bool InternalCustomerExists(int id);
    }
}
