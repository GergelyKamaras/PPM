using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServiceModelLibrary.DTOs
{
    public interface IUserLoginDTO
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}
