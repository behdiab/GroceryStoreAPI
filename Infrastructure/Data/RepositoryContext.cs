using System;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }

       
        public DbSet<Customer> Customers { get; set; }


       
    }
}
