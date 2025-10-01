using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAutenticación
{
    public class AuthDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public AuthDto(string email, string password) {
            Email = email;
            Password = password;
        }
    }
}
