﻿using System.ComponentModel.DataAnnotations;

namespace PPMModelLibrary.Models.Users.Administrator
{
    public class Administrator : IAdministrator
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
    }
}
