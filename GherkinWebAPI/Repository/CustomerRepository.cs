using GherkinWebAPI.Core;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await FindAll()
               .OrderBy(customer => customer.UP_Serial_No)
               .ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await FindByCondition(customer => customer.Employee_ID.Equals(customerId))
                .FirstOrDefaultAsync();
        }

        //public async Task<Customer> GetCustomerWithDetailsAsync(int customerId)
        //{
        //    return await FindByCondition(customer => customer.customerId.Equals(customerId))
        //        .Include(ac => ac.Accounts)
        //        .FirstOrDefaultAsync();
        //}

        public void CreateCustomer(Customer customer)
        {
            Create(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            Update(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            Delete(customer);
        }
    }
}
