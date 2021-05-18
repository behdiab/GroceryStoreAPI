using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Contracts
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {


        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);

    }


}
