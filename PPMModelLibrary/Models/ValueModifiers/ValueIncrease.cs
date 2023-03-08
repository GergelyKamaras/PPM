﻿namespace PPMModelLibrary.Models.ValueModifiers
{
    public class ValueIncrease : IValueChange
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }
}