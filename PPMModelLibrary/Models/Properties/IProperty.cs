using PPMModelLibrary.Models.Users;
using PPMModelLibrary.Models.UtilityModels;

namespace PPMModelLibrary.Models.Properties
{
    internal interface IProperty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public decimal PurchasePrice { get; }
        public DateTime PurchaseDate { get; }
        public Owner Owner { get; set; }
    }
}
