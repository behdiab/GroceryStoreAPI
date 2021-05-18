using System;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IRepositoryWrapper
    {

            ICustomerRepository Customer { get; }
            Task SaveAsync();
        
    }
}

