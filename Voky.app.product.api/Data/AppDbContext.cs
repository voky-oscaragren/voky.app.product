using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
}
