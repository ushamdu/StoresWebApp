using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoresWebApp.Models.DAL;
using StoresWebApp.Models.CustomModels;

namespace StoresWebApp.Models.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers();
        IEnumerable<CustomerInfo> GetCustomerDetails();
        IEnumerable<CustomerLookUp> GetCustomerLookUp();
        CustomerInfo GetCustomerById(long customerId);
        string SaveCustomer(CustomerInfo oCustomer);
        void DeleteCustomer(long customerId);
    }
}
