namespace PPMAPIDTOModelLibrary.OutputDTOs.Properties
{
    internal interface IRentalPropertyOutputDTO : IPropertyOutputDTO
    {
        public decimal RentalFee { get; set; }
        public string TenantId { get; set; }
    }
}
