using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModelLibrary.Enums;

namespace EntityModelLibrary.Models.Users
{
    public interface IUser
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
    }
}
