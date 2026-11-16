using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; }

        public List<Role> Roles { get; set; }


    }
}
