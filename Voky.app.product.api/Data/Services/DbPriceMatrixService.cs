using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Data.Services;

public class DbPriceMatrixService(AppDbContext db)
{
    public async Task<IEnumerable<PriceMatrix>> GetAllAsync() =>
        await db.PriceMatrices.AsNoTracking().ToListAsync();

    public async Task<PriceMatrix?> GetByIdAsync(Guid id) =>
        await db.PriceMatrices.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

    public async Task<PriceMatrix> CreateAsync(PriceMatrix priceMatrix)
    {
        db.PriceMatrices.Add(priceMatrix);
        await db.SaveChangesAsync();
        return priceMatrix;
    }
}
