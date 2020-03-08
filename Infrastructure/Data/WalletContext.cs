using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure.Data
{
    public class WalletContext : IdentityDbContext<User>
    {
        public WalletContext(DbContextOptions<WalletContext> options) : base(options)
        {
        }

        public DbSet<Income> Incomes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            Initialization(builder);

        }

        private void Initialization(ModelBuilder builder)
        {
            builder.Entity<Currency>().HasData(
               new Currency(1, "BYN"),
               new Currency(2, "RUB"),
               new Currency(3, "USD"),
               new Currency(4, "EUR"));
        }
    }

}
