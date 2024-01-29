using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.InterFaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<ReqResCustomer>> GetInternalCustomersAsync();
        Task<ReqResCustomer> GetReqResCustomerByIdAsync(int id);
        Task<InternalCustomer> GetInternalCustomerByIdAsync(int id);
        void UpdateCustomerAsync(InternalCustomer customer);
        void DeleteCustomerAsync(int id);
        void CreateCustomerAsync(InternalCustomer customer);
        bool InternalCustomerExists(int id);
    }
}
