using Voky.app.product.api.Data.Services;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;
using Voky.app.product.api.Services;
using Voky.app.product.api.tests.Helpers;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.tests.Services;

public class PriceMatrixServiceTests
{
    private static async Task<(PriceMatrixService service, AppDbContext db, string productNr, int scId, int cepId)> SetupAsync()
    {
        var db = TestDbContextFactory.Create();
        var product = new Product { ProductNr = "PM-001", Name = "Matrix Product" };
        var supplierCurrency = new SupplierCurrency { Name = "USD" };
        var currencyEndPrice = new CurrencyEndPrice { Name = "EUR" };
        db.Products.Add(product);
        db.SupplierCurrencies.Add(supplierCurrency);
        db.CurrencyEndPrices.Add(currencyEndPrice);
        await db.SaveChangesAsync();

        var dbService = new DbPriceMatrixService(db);
        var service = new PriceMatrixService(dbService);
        return (service, db, product.ProductNr, supplierCurrency.Id, currencyEndPrice.Id);
    }

    private static CreatePriceMatrixDto SampleDto(string productNr, int scId, int cepId) => new(
        ProductNr: productNr,
        SupplierCurrencyId: scId,
        CurrencyEndPriceId: cepId,
        EndCustPrice: 99.99m,
        MOQ: 10,
        SupplierNetPrice: 49.99m
    );

    [Fact]
    public async Task CreateAsync_MapsAllDtoFieldsToMatrix()
    {
        var (service, db, productNr, scId, cepId) = await SetupAsync();

        var created = await service.CreateAsync(SampleDto(productNr, scId, cepId));

        Assert.Equal(productNr, created.ProductNr);
        Assert.Equal(99.99m, created.EndCustPrice);
        Assert.Equal(49.99m, created.SupplierNetPrice);
        Assert.Equal(10, created.MOQ);
        db.Dispose();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllMatrices()
    {
        var (service, db, productNr, scId, cepId) = await SetupAsync();
        await service.CreateAsync(SampleDto(productNr, scId, cepId));
        await service.CreateAsync(SampleDto(productNr, scId, cepId));

        var result = await service.GetAllAsync();

        Assert.Equal(2, result.Count());
        db.Dispose();
    }

    [Fact]
    public async Task GetByIdAsync_ExistingMatrix_ReturnsMatrix()
    {
        var (service, db, productNr, scId, cepId) = await SetupAsync();
        var created = await service.CreateAsync(SampleDto(productNr, scId, cepId));

        var result = await service.GetByIdAsync(created.Id);

        Assert.NotNull(result);
        Assert.Equal(99.99m, result.EndCustPrice);
        db.Dispose();
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingMatrix_ReturnsNull()
    {
        var (service, db, _, _, _) = await SetupAsync();

        var result = await service.GetByIdAsync(Guid.NewGuid());

        Assert.Null(result);
        db.Dispose();
    }
}
