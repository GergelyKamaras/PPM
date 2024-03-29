﻿namespace PPMAPIDTOModelLibrary.SharedDTOs
{
    public class AddressDTO
    {
        public int? Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
