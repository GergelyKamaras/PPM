namespace PPMModelLibrary.Models.Users
{
    public interface IUser
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
    }
}
