using Voky.app.product.api.Data.Services;
using Voky.app.product.api.Models;
using Voky.app.product.api.tests.Helpers;

namespace Voky.app.product.api.tests.DbServices;

public class DbMainSupplierServiceTests
{
    private static MainSupplier SampleSupplier(string name = "Acme Corp") => new()
    {
        Name = name,
        Email = "contact@acme.com",
    };

    [Fact]
    public async Task GetAllAsync_ReturnsAllSuppliers()
    {
        using var db = TestDbContextFactory.Create();
        db.MainSuppliers.AddRange(SampleSupplier("Acme"), SampleSupplier("Globex"));
        await db.SaveChangesAsync();

        var service = new DbMainSupplierService(db);
        var result = await service.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ExistingSupplier_ReturnsSupplier()
    {
        using var db = TestDbContextFactory.Create();
        db.MainSuppliers.Add(SampleSupplier());
        await db.SaveChangesAsync();
        var supplierNr = db.MainSuppliers.First().SupplierNr;

        var service = new DbMainSupplierService(db);
        var result = await service.GetByIdAsync(supplierNr);

        Assert.NotNull(result);
        Assert.Equal(supplierNr, result.SupplierNr);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingSupplier_ReturnsNull()
    {
        using var db = TestDbContextFactory.Create();
        var service = new DbMainSupplierService(db);

        var result = await service.GetByIdAsync(9999);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_AddsSupplierToDatabase()
    {
        using var db = TestDbContextFactory.Create();
        var service = new DbMainSupplierService(db);

        var created = await service.CreateAsync(SampleSupplier("New Supplier"));

        Assert.Equal("New Supplier", created.Name);
        Assert.Equal(1, db.MainSuppliers.Count());
    }
}
