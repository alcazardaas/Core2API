using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Server.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}
