using Voky.app.product.api.Data.Services;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Services;

public class LifecycleService(DbLifecycleService dbLifecycleService)
{
    public async Task<IEnumerable<Lifecycle>> GetAllAsync() =>
        await dbLifecycleService.GetAllAsync();

    public async Task<Lifecycle?> GetByIdAsync(int id) =>
        await dbLifecycleService.GetByIdAsync(id);

    public async Task<Lifecycle> CreateAsync(CreateLifecycleDto dto)
    {
        var lifecycle = new Lifecycle { Name = dto.Name };
        return await dbLifecycleService.CreateAsync(lifecycle);
    }
}
