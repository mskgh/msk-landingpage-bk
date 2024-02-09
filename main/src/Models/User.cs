namespace main.src.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherNames {  get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
    }
}