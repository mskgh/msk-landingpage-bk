namespace main.src.Dtos
{
    public class ReadUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherNames { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; } 
    }
}
