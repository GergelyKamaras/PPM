﻿namespace PPMAPIDTOModelLibrary.OutputDTOs.FinancialObjects
{
    public class FinancialObjectOutputDTO : IFinancialObjectOutputDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public string PropertyId { get; set; }
        public bool IsRental { get; set; }
        public string FinancialObjectType { get; set; }
    }
}
