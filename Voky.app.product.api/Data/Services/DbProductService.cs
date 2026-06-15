using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Data.Services;

public class DbProductService(AppDbContext db)
{
    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await db.Products.AsNoTracking().ToListAsync();

    public async Task<Product?> GetByIdAsync(string productNr) =>
        await db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductNr == productNr);

    public async Task<Product> CreateAsync(Product product)
    {
        db.Products.Add(product);
        await db.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> UpdateAsync(Product product)
    {
        var existing = await db.Products.FindAsync(product.ProductNr);
        if (existing is null) return null;

        existing.Name = product.Name;
        existing.LevArtNrName = product.LevArtNrName;
        existing.ArtNrVarianthead = product.ArtNrVarianthead;
        existing.Description = product.Description;
        existing.SendToOpti = product.SendToOpti;
        existing.ArtNrStartCost = product.ArtNrStartCost;
        existing.StartCostAmount = product.StartCostAmount;
        existing.MainSupplierNr = product.MainSupplierNr;
        existing.LifecycleId = product.LifecycleId;
        existing.QuestionGroupId = product.QuestionGroupId;
        existing.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(string productNr)
    {
        var product = await db.Products.FindAsync(productNr);
        if (product is null) return false;

        db.Products.Remove(product);
        await db.SaveChangesAsync();
        return true;
    }
}
