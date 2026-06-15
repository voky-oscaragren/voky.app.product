using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Data.Services;

public class DbProductService(AppDbContext db)
{
    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await db.Products.AsNoTracking().ToListAsync();

    public async Task<Product?> GetByIdAsync(Guid id) =>
        await db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Product> CreateAsync(Product product)
    {
        db.Products.Add(product);
        await db.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> UpdateAsync(Product product)
    {
        var existing = await db.Products.FindAsync(product.Id);
        if (existing is null) return null;

        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Price = product.Price;
        existing.Stock = product.Stock;
        existing.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await db.Products.FindAsync(id);
        if (product is null) return false;

        db.Products.Remove(product);
        await db.SaveChangesAsync();
        return true;
    }
}
