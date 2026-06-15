using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.tests.Helpers;

public static class TestDbContextFactory
{
    public static AppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
}
