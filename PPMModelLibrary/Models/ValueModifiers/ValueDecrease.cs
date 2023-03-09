﻿using System.ComponentModel.DataAnnotations;

namespace PPMModelLibrary.Models.ValueModifiers
{
    public class ValueDecrease : IValueChange
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }
}
