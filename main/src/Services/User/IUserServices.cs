using main.src.Dtos;
using main.src.Models;
namespace main.src.Services.User
{
    public interface IUserServices
    {
        public List<Models.User> GetAllUsers();
        public void AddUser(Dtos.WriteUserDto user);
        public Models.User GetUser(Guid id);
        public void UpdateUser(Guid id, WriteUserDto writeUserDto);
        public void DeleteUser(Guid id);
    }
}
