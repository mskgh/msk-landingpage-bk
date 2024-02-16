using main.src.Dtos;
using main.src.Entities;

namespace main.src.Repositories.MyInMemoryDB
{
    public class MyInMemoryDB:IMyInMemoryDB
    {
        List<Entities.User> users;
        public MyInMemoryDB()
        {
            this.users = new List<User>();
        }

        public List<User> GetUsers()
        {
            return users;
        }

        public User AddUser( Entities.User user )
        {
            users.Add( user );
            return user;
        }

        public User GetUserById(Guid id)
        {
            Entities.User myuser = new User();

            int index = users.FindIndex(u => u.Id == id);

            myuser = users[index];

            return myuser;
        }

        public void DeleteUser(Guid id)
        {
            if(users.Exists(u => u.Id == id)) 
            {
                int index = users.FindIndex(u => u.Id == id);

                users.RemoveAt(index);

            }
        }

        public void UpdateUser(Guid id, WriteUserDto writeUserDto)
        {
            if (users.Exists(u => u.Id == id))
            {
                int index = users.FindIndex(u => u.Id == id);

                users[index].FirstName = writeUserDto.FirstName;
                users[index].LastName = writeUserDto.LastName;
                users[index].Email = writeUserDto.Email;
                users[index].MobileNumber = writeUserDto.MobileNumber;

            }
        }
    }
}
