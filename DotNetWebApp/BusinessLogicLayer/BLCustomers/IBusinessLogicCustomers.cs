using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace BusinessLogicLayer.BLCustomers
{
    internal interface IBusinessLogicCustomers
    {
        Task<List<ReqResCustomer>> GetDataList();
        Task<ReqResCustomer> GetCustomerById(int id);
        void AddCustomer(InternalCustomer data);
        void RemoveCustomer(int id);
        void UpdateCustomer(ReqResCustomer data);
    }
}
