using System.ComponentModel.DataAnnotations;

namespace main.src.ENTITIES
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
