namespace PPMModelLibrary.Models.UtilityModels
{
    internal class Address : IAddress
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
