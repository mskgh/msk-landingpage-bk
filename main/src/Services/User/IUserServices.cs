using main.src.Dtos;
using main.src.Models;
namespace main.src.Services.User
{
    public interface IUserServices
    {
        public Entities.User AssignIds(WriteUserDto userDto);
    }
}
