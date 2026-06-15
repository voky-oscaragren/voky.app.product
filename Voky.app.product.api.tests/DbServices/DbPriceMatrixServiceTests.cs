using Voky.app.product.api.Data.Services;
using Voky.app.product.api.Models;
using Voky.app.product.api.tests.Helpers;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.tests.DbServices;

public class DbPriceMatrixServiceTests
{
    private static async Task<(AppDbContext db, string productNr, int supplierCurrencyId, int currencyEndPriceId)> SetupPrerequisitesAsync()
    {
        var db = TestDbContextFactory.Create();
        var product = new Product { ProductNr = "PM-001", Name = "Matrix Product" };
        var supplierCurrency = new SupplierCurrency { Name = "USD" };
        var currencyEndPrice = new CurrencyEndPrice { Name = "EUR" };
        db.Products.Add(product);
        db.SupplierCurrencies.Add(supplierCurrency);
        db.CurrencyEndPrices.Add(currencyEndPrice);
        await db.SaveChangesAsync();
        return (db, product.ProductNr, supplierCurrency.Id, currencyEndPrice.Id);
    }

    private static PriceMatrix SampleMatrix(string productNr, int supplierCurrencyId, int currencyEndPriceId) => new()
    {
        ProductNr = productNr,
        SupplierCurrencyId = supplierCurrencyId,
        CurrencyEndPriceId = currencyEndPriceId,
        EndCustPrice = 99.99m,
        SupplierNetPrice = 49.99m,
    };

    [Fact]
    public async Task GetAllAsync_ReturnsAllPriceMatrices()
    {
        var (db, productNr, scId, cepId) = await SetupPrerequisitesAsync();
        db.PriceMatrices.AddRange(SampleMatrix(productNr, scId, cepId), SampleMatrix(productNr, scId, cepId));
        await db.SaveChangesAsync();

        var service = new DbPriceMatrixService(db);
        var result = await service.GetAllAsync();

        Assert.Equal(2, result.Count());
        db.Dispose();
    }

    [Fact]
    public async Task GetByIdAsync_ExistingMatrix_ReturnsMatrix()
    {
        var (db, productNr, scId, cepId) = await SetupPrerequisitesAsync();
        var matrix = SampleMatrix(productNr, scId, cepId);
        db.PriceMatrices.Add(matrix);
        await db.SaveChangesAsync();

        var service = new DbPriceMatrixService(db);
        var result = await service.GetByIdAsync(matrix.Id);

        Assert.NotNull(result);
        Assert.Equal(99.99m, result.EndCustPrice);
        db.Dispose();
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingMatrix_ReturnsNull()
    {
        var (db, _, _, _) = await SetupPrerequisitesAsync();
        var service = new DbPriceMatrixService(db);

        var result = await service.GetByIdAsync(Guid.NewGuid());

        Assert.Null(result);
        db.Dispose();
    }

    [Fact]
    public async Task CreateAsync_AddsMatrixToDatabase()
    {
        var (db, productNr, scId, cepId) = await SetupPrerequisitesAsync();
        var service = new DbPriceMatrixService(db);

        var created = await service.CreateAsync(SampleMatrix(productNr, scId, cepId));

        Assert.Equal(productNr, created.ProductNr);
        Assert.Equal(1, db.PriceMatrices.Count());
        db.Dispose();
    }
}
