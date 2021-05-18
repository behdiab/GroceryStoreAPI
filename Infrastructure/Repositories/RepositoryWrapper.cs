using System;
using System.Threading.Tasks;
using Core.Contracts;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _context;
        private ICustomerRepository _customer;


        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _context = repositoryContext;
        }


        public ICustomerRepository Customer
        {
            get
            {
                if (_customer == null)
                {
                    _customer = new CustomerRepository(_context);
                }

                return _customer;
            }
        }



        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}


