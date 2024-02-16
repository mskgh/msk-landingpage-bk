using main.src.Dtos;

namespace main.src.Repositories.MyInMemoryDB
{
    public interface IMyInMemoryDB
    {
        public Entities.User AddUser(Entities.User user);
        public void UpdateUser(Guid id, WriteUserDto writeUserDto);
        public void DeleteUser(Guid id);
        public List<Entities.User> GetUsers();
        public Entities.User GetUserById(Guid id);
    }
}
