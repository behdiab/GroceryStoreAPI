using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Contracts;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository: RepositoryBase<Customer>, ICustomerRepository
    {

            public CustomerRepository(RepositoryContext repositoryContext)
                : base(repositoryContext)
            {
            }


        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await FindAll()
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await FindByCondition(customer => customer.Id.Equals(id)).FirstOrDefaultAsync();
        }


        public void CreateCustomer(Customer customer)
        {
            Create(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            Update(customer);
        }

    }
}

