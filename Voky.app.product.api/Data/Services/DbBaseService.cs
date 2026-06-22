using Voky.Shared.Visma.Database;

namespace Voky.app.product.api.Data.Services
{
    public class DbBaseService
    {
        protected readonly VokyDbContextFactory<VismaDbContext> _contextFactory;

        public DbBaseService(VokyDbContextFactory<VismaDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        protected string? _tenantId;

        public void UseTenant(string tenantId)
        {
            _tenantId = tenantId;
        }

        protected void CheckTenant()
        {
            if (_tenantId == null)
                throw new InvalidOperationException("Call UseTenant before use.");
        }
    }
}
