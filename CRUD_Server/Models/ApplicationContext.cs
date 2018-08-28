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
        public DbSet<FavAccount> FavAccounts { get; set; }
        public DbSet<Provider> Providers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .HasKey(p => new { p.ClientId, p.ProviderId });

            modelBuilder.Entity<Payment>()
                .HasOne(c => c.Client)
                .WithMany(p => p.Payments)
                .HasForeignKey(c => c.ClientId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Provider)
                .WithMany(pv => pv.Payments)
                .HasForeignKey(p => p.ProviderId);

            modelBuilder.Entity<FavAccount>()
            .HasKey(c => new { c.ClientId, c.FavBankAccount });
        }
    }
}

