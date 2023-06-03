using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESSWebAPI.Repository;
using GherkinWebAPI.Models;

namespace GherkinWebAPI.Core
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int customerId);
        //Task<Customer> GetCustomerWithDetailsAsync(int customerId);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}
