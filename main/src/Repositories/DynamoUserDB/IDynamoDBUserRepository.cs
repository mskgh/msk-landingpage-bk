using main.src.Entities;

namespace main.src.Repositories.DynamoUserDB
{
    public interface IDynamoDBUserRepository
    {
        Task<User> GetUserById(Guid id,Guid ternantId);
        Task<bool> AddUser(User user);
        Task<bool> DeleteUser(Guid id, Guid ternantId);
        Task<bool> UpdateUser(Guid id, Guid ternantId,Dtos.UpdateUserDto updateUserDto);
        Task<bool> CheckIfUserExists(Guid id,Guid ternantId);
        Task<List<User>> GetAllUsers();
        
    }
}
