using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModelLibrary.Enums;

namespace EntityModelLibrary.Models.Users.Owner
{
    public class Owner : IOwner
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
    }
}
