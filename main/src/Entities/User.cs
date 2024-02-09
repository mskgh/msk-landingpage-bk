using System.ComponentModel.DataAnnotations;

namespace main.src.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        public string OtherNames { get; set; }
        [Required]
        public string Email { get; set; }= null!;
        public string MobileNumber {  get; set; }
        public string Password {  get; set; }
    }
}
