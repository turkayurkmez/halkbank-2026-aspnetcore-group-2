using ECommerce.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Auth
{
    public interface IPasswordHasher
    {
        string Hash(User user, string password);
        bool Verify(User user, string password);
    }

    public class PasswordHasher : IPasswordHasher
    {

        private readonly PasswordHasher<User> inner = new();
        public string Hash(User user, string password)
        {
            return inner.HashPassword(user, password);
        }

        public bool Verify(User user, string password)
        {
            return inner.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Failed;
        }
    }
}
