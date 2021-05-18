using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.Configuration
{

    public class DataSeeder
    {
        public static void SeedCustomers(RepositoryContext context)
        {
            if (!context.Customers.Any())
            {
                var customers = new List<Customer>
                {
                    new Customer { Name = "Bob" },
                    new Customer { Name = "Mary" },
                    new Customer { Name = "Joe" },

                };

                context.AddRange(customers);
                context.SaveChanges();
            }
        }
    }

}
