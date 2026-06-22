using Microsoft.EntityFrameworkCore;
using Voky.Shared.Visma.Database;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Data
{
    public class VismaDbContext : VokyDbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<MainSupplier> MainSuppliers => Set<MainSupplier>();
        public DbSet<QuestionGroup> QuestionGroups => Set<QuestionGroup>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<QuestionChoice> QuestionChoices => Set<QuestionChoice>();
        public DbSet<QuestionType> QuestionTypes => Set<QuestionType>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Currency> Currencies => Set<Currency>();
        public DbSet<PriceMatrix> PriceMatrices => Set<PriceMatrix>();
        public VismaDbContext(DbContextOptions<VismaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

