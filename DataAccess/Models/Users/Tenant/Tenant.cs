using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Enums;

namespace DataAccess.Models.Users.Tenant
{
    public class Tenant : ITenant
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
    }
}
