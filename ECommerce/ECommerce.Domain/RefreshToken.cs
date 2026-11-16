using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain
{
    public class RefreshToken : IEntity
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public string? ReplacedByToken { get; set; }
    }

}
