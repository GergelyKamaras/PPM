﻿namespace PPMModelLibrary.Models.ValueModifiers
{
    public class ValueDecrease : IValueChange
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }
}
