using Voky.app.product.api.Data.Services;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;
using Voky.app.product.api.Services;
using Voky.app.product.api.tests.Helpers;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.tests.Services;

public class ProductServiceTests
{
    private static ProductService CreateService(out AppDbContext db)
    {
        db = TestDbContextFactory.Create();
        var dbService = new DbProductService(db);
        return new ProductService(dbService);
    }

    private static CreateProductDto SampleCreateDto(string productNr = "P-001") => new(
        ProductNr: productNr,
        Name: "Test Product",
        LevArtNrName: null,
        ArtNrVarianthead: null,
        Description: "A description",
        SendToOpti: false,
        ArtNrStartCost: null,
        StartCostAmount: null,
        MainSupplierNr: null,
        LifecycleId: null,
        QuestionGroupId: null
    );

    [Fact]
    public async Task CreateAsync_MapsAllDtoFieldsToProduct()
    {
        var service = CreateService(out var db);

        var created = await service.CreateAsync(SampleCreateDto("P-MAP"));

        Assert.Equal("P-MAP", created.ProductNr);
        Assert.Equal("Test Product", created.Name);
        Assert.Equal("A description", created.Description);
        db.Dispose();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllProducts()
    {
        var service = CreateService(out var db);
        await service.CreateAsync(SampleCreateDto("P-001"));
        await service.CreateAsync(SampleCreateDto("P-002"));

        var result = await service.GetAllAsync();

        Assert.Equal(2, result.Count());
        db.Dispose();
    }

    [Fact]
    public async Task GetByIdAsync_ExistingProduct_ReturnsProduct()
    {
        var service = CreateService(out var db);
        await service.CreateAsync(SampleCreateDto("P-001"));

        var result = await service.GetByIdAsync("P-001");

        Assert.NotNull(result);
        Assert.Equal("P-001", result.ProductNr);
        db.Dispose();
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingProduct_ReturnsNull()
    {
        var service = CreateService(out var db);

        var result = await service.GetByIdAsync("NONE");

        Assert.Null(result);
        db.Dispose();
    }

    [Fact]
    public async Task UpdateAsync_ExistingProduct_UpdatesName()
    {
        var service = CreateService(out var db);
        await service.CreateAsync(SampleCreateDto("P-001"));

        var updateDto = new UpdateProductDto(
            ProductNr: "P-001",
            Name: "Renamed Product",
            LevArtNrName: null,
            ArtNrVarianthead: null,
            Description: null,
            SendToOpti: true,
            ArtNrStartCost: null,
            StartCostAmount: null,
            MainSupplierNr: null,
            LifecycleId: null,
            QuestionGroupId: null
        );

        var result = await service.UpdateAsync("P-001", updateDto);

        Assert.NotNull(result);
        Assert.Equal("Renamed Product", result.Name);
        Assert.True(result.SendToOpti);
        db.Dispose();
    }

    [Fact]
    public async Task UpdateAsync_NonExistingProduct_ReturnsNull()
    {
        var service = CreateService(out var db);

        var updateDto = new UpdateProductDto(
            ProductNr: "NONE",
            Name: "Ghost",
            LevArtNrName: null,
            ArtNrVarianthead: null,
            Description: null,
            SendToOpti: false,
            ArtNrStartCost: null,
            StartCostAmount: null,
            MainSupplierNr: null,
            LifecycleId: null,
            QuestionGroupId: null
        );

        var result = await service.UpdateAsync("NONE", updateDto);

        Assert.Null(result);
        db.Dispose();
    }

    [Fact]
    public async Task DeleteAsync_ExistingProduct_ReturnsTrue()
    {
        var service = CreateService(out var db);
        await service.CreateAsync(SampleCreateDto("P-001"));

        var deleted = await service.DeleteAsync("P-001");

        Assert.True(deleted);
        db.Dispose();
    }

    [Fact]
    public async Task DeleteAsync_NonExistingProduct_ReturnsFalse()
    {
        var service = CreateService(out var db);

        var deleted = await service.DeleteAsync("NONE");

        Assert.False(deleted);
        db.Dispose();
    }
}
