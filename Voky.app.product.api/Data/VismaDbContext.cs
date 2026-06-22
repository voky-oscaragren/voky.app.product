using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Voky.Shared.Visma.Database;
using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Data;
{
    public class VismaDbContext : VokyDbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<MainSupplier> MainSuppliers => Set<MainSupplier>();
        public DbSet<Lifecycle> Lifecycles => Set<Lifecycle>();
        public DbSet<SupplierCurrency> SupplierCurrencies => Set<SupplierCurrency>();
        public DbSet<CurrencyEndPrice> CurrencyEndPrices => Set<CurrencyEndPrice>();
        public DbSet<QuestionGroup> QuestionGroups => Set<QuestionGroup>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<QuestionChoice> QuestionChoices => Set<QuestionChoice>();
        public DbSet<QuestionType> QuestionTypes => Set<QuestionType>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<PriceMatrix> PriceMatrices => Set<PriceMatrix>();
        public VismaDbContext(DbContextOptions<VismaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // The Visma "Actor" table stores both customers and suppliers as subtypes,
            // distinguished by which id column is populated: CustNo for customers, SupNo
            // for suppliers. Two keyless entities cannot share a table via annotations,
            // so map each to a filtered query that selects only its subtype's rows.
            modelBuilder.Entity<Customer>()
                .ToSqlQuery("SELECT * FROM dbo.Actor WHERE CustNo <> 0");

            modelBuilder.Entity<Supplier>()
                .ToSqlQuery("SELECT * FROM dbo.Actor WHERE SupNo <> 0");
        }
    }
}

