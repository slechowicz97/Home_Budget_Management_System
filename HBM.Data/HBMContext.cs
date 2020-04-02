using HBM.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Data
{
    public class HBMContext : IdentityDbContext<HBMUser>
    {
        public HBMContext() : base("HBMConnection")
        {
        }

        public static HBMContext Create()
        {
            return new HBMContext();
        }

        public DbSet<ScheduledTransaction> ScheduledTransactions { get; set; }
        public DbSet<KindOfDebts> KindOfDebts { get; set; }
        public DbSet<Debts> Debts { get; set; }

        public DbSet<KBudget> KBudgets { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<ImportantShopping> ImportantShoppings { get; set; }

        public DbSet<Picture> Pictures { get; set; }
        public DbSet<ShoppingPicture> ShoppingPictures { get; set; }
        public DbSet<Balance> Balance { get; set; }
        public DbSet<Savings> Savings { get; set; }

        public DbSet<DebtCard> DebtCards { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Currency> Currencies { get; set; }

    }
}
