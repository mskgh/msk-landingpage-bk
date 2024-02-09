using main.src.Entities;
using Microsoft.EntityFrameworkCore;
namespace main.src.Repositories
{
    public class DataBaseContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "TestUsers");
        }

        public DbSet<User> Users { get; set; }
    }
}
