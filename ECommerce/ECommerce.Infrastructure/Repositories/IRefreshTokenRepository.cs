using ECommerce.Domain;

namespace ECommerce.Infrastructure.Repositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {

        // TODO 1.1: Burada, Revoke meaknizması açıklanmalı
        Task<RefreshToken?> FindByHashAsync(string hash);

        Task<bool> TryRevokeAsync(string tokenHash, string replacedByHash);
    }
}
