using Voky.app.product.api.Data.Services;
using Voky.app.product.api.Models;
using Voky.app.product.api.tests.Helpers;

namespace Voky.app.product.api.tests.DbServices;

public class DbProductServiceTests
{
    private static Product SampleProduct(string productNr = "P-001") => new()
    {
        ProductNr = productNr,
        Name = "Test Product",
    };

    [Fact]
    public async Task GetAllAsync_ReturnsAllProducts()
    {
        using var db = TestDbContextFactory.Create();
        db.Products.AddRange(SampleProduct("P-001"), SampleProduct("P-002"));
        await db.SaveChangesAsync();

        var service = new DbProductService(db);
        var result = await service.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ExistingProduct_ReturnsProduct()
    {
        using var db = TestDbContextFactory.Create();
        db.Products.Add(SampleProduct("P-001"));
        await db.SaveChangesAsync();

        var service = new DbProductService(db);
        var result = await service.GetByIdAsync("P-001");

        Assert.NotNull(result);
        Assert.Equal("P-001", result.ProductNr);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingProduct_ReturnsNull()
    {
        using var db = TestDbContextFactory.Create();
        var service = new DbProductService(db);

        var result = await service.GetByIdAsync("NONE");

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_AddsProductToDatabase()
    {
        using var db = TestDbContextFactory.Create();
        var service = new DbProductService(db);

        var created = await service.CreateAsync(SampleProduct("P-NEW"));

        Assert.Equal("P-NEW", created.ProductNr);
        Assert.Equal(1, db.Products.Count());
    }

    [Fact]
    public async Task UpdateAsync_ExistingProduct_UpdatesFields()
    {
        using var db = TestDbContextFactory.Create();
        db.Products.Add(SampleProduct("P-001"));
        await db.SaveChangesAsync();

        var service = new DbProductService(db);
        var updated = await service.UpdateAsync(new Product
        {
            ProductNr = "P-001",
            Name = "Updated Name",
            Description = "Updated Description",
        });

        Assert.NotNull(updated);
        Assert.Equal("Updated Name", updated.Name);
        Assert.Equal("Updated Description", updated.Description);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingProduct_ReturnsNull()
    {
        using var db = TestDbContextFactory.Create();
        var service = new DbProductService(db);

        var result = await service.UpdateAsync(SampleProduct("NONE"));

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_ExistingProduct_RemovesAndReturnsTrue()
    {
        using var db = TestDbContextFactory.Create();
        db.Products.Add(SampleProduct("P-001"));
        await db.SaveChangesAsync();

        var service = new DbProductService(db);
        var deleted = await service.DeleteAsync("P-001");

        Assert.True(deleted);
        Assert.Equal(0, db.Products.Count());
    }

    [Fact]
    public async Task DeleteAsync_NonExistingProduct_ReturnsFalse()
    {
        using var db = TestDbContextFactory.Create();
        var service = new DbProductService(db);

        var deleted = await service.DeleteAsync("NONE");

        Assert.False(deleted);
    }
}
