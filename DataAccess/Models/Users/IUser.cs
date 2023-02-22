using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Enums;

namespace DataAccess.Models.Users
{
    public interface IUser
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
    }
}
