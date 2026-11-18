using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Auth
{
    public class JwtOptions
    {
        public const string Section = "Jwt";
        public string Issuer { get; set; } = string.Empty;// token'ı kim verdi?
        public string Audience { get; set; } = string.Empty; //token kime verildi? 

        public string SigningKey { get; set; } = string.Empty; //token içindeki şifrelenmiş gizli ifade

        public int AccessTokenMinutes { get; set; }  // Access Token kaç dakika geçerli?
        public int RefreshTokenDays { get; set; } //Refresh token kaç gün geçerli?

    }
}
