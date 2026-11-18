using ECommerce.Domain;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    public class RefreshTokenRepository(AppDbContext db) : Repository<RefreshToken>(db), IRefreshTokenRepository
    {
        public async Task<RefreshToken?> FindByHashAsync(string hash)
        {
            return await db.RefreshTokens.AsNoTracking().Include(r => r.User).ThenInclude(u => u.Roles).FirstOrDefaultAsync(r => r.Token == hash);
        }

        /*
         * Rotation: eski token koşullu UPDATE sorgusu ile kapatılır. Aynı anda gelen iki istekten sadece biri true olabilir. 
         * Önce revoke
         * Sonra yeni token insert
         * 
         */
        public async Task<bool> TryRevokeAsync(string tokenHash, string replacedByHash)
        {
            var now = DateTime.Now;
            var affected = await db.RefreshTokens.Where(t => t.Token == tokenHash && t.RevokedAt == null)
                                                 .ExecuteUpdateAsync(r => r.SetProperty(x => x.RevokedAt, now)
                                                                           .SetProperty(x => x.ReplacedByToken, replacedByHash));

            return affected == 1;
        }
    }
}
