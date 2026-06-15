using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<MainSupplier> MainSuppliers => Set<MainSupplier>();
    public DbSet<Lifecycle> Lifecycles => Set<Lifecycle>();
    public DbSet<SupplierCurrency> SupplierCurrencies => Set<SupplierCurrency>();
    public DbSet<CurrencyEndPrice> CurrencyEndPrices => Set<CurrencyEndPrice>();
    public DbSet<QuestionGroup> QuestionGroups => Set<QuestionGroup>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<QuestionChoice> QuestionChoices => Set<QuestionChoice>();
    public DbSet<PriceMatrix> PriceMatrices => Set<PriceMatrix>();
}
