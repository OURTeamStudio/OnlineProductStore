using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineProductStore.Shared
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Role { get; set; } = "Customer";

        public Address Address { get; set; }
    }
}
