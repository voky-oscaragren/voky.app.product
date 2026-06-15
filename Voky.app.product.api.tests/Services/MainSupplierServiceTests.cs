using Voky.app.product.api.Data.Services;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Services;
using Voky.app.product.api.tests.Helpers;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.tests.Services;

public class MainSupplierServiceTests
{
    private static MainSupplierService CreateService(out AppDbContext db)
    {
        db = TestDbContextFactory.Create();
        var dbService = new DbMainSupplierService(db);
        return new MainSupplierService(dbService);
    }

    private static CreateMainSupplierDto SampleDto(string name = "Acme Corp") => new(
        Name: name,
        Email: "acme@example.com",
        BankAccountGiro: null
    );

    [Fact]
    public async Task CreateAsync_MapsAllDtoFieldsToSupplier()
    {
        var service = CreateService(out var db);

        var created = await service.CreateAsync(SampleDto());

        Assert.Equal("Acme Corp", created.Name);
        Assert.Equal("acme@example.com", created.Email);
        db.Dispose();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllSuppliers()
    {
        var service = CreateService(out var db);
        await service.CreateAsync(SampleDto("Acme"));
        await service.CreateAsync(SampleDto("Globex"));

        var result = await service.GetAllAsync();

        Assert.Equal(2, result.Count());
        db.Dispose();
    }

    [Fact]
    public async Task GetByIdAsync_ExistingSupplier_ReturnsSupplier()
    {
        var service = CreateService(out var db);
        var created = await service.CreateAsync(SampleDto());

        var result = await service.GetByIdAsync(created.SupplierNr);

        Assert.NotNull(result);
        Assert.Equal("Acme Corp", result.Name);
        db.Dispose();
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingSupplier_ReturnsNull()
    {
        var service = CreateService(out var db);

        var result = await service.GetByIdAsync(9999);

        Assert.Null(result);
        db.Dispose();
    }
}
