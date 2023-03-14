﻿namespace PPMDTOModelLibrary.InputDTOs.Properties
{
    public class PropertyInputDTO
    {
        public string Name { get; set; }
        public float Size { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string OwnerId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
