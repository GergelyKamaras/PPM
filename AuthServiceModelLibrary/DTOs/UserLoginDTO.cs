using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServiceModelLibrary.DTOs
{
    public class UserLoginDTO : IUserLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
