using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPMModelLibrary.Models.Users;

namespace PPMModelLibrary.Models.Properties
{
    internal interface IRentable
    {
        public Tenant Tenant { get; set; }
        public decimal RentalFee { get; set; }
    }
}
