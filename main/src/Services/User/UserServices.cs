
using AutoMapper;
using main.src.Dtos;
using main.src.Repositories;
using main.src.Repositories.MyInMemoryDB;

namespace main.src.Services.User
{
    public class UserServices : IUserServices
    {
        IMapper mapper;
        IMyInMemoryDB database;
        public UserServices( IMapper mapper,IMyInMemoryDB database) 
        {
            this.mapper = mapper;
            this.database = database;
        }

        public List<Models.User> GetAllUsers()
        {
            List<Entities.User> userEntity = database.GetUsers();
            List<Models.User> users = mapper.Map<List<Models.User>>(userEntity);

            return users;
        }

        public Models.User AddUser(Dtos.WriteUserDto writeUserDto)
        {
            Models.User? user = new Models.User();
            if (writeUserDto == null) 
            {
                user = null;
                return user;
            }
           
            Entities.User newUser = new Entities.User();
            newUser.Id = Guid.NewGuid();
            newUser.FirstName = writeUserDto.FirstName;
            newUser.LastName = writeUserDto.LastName;
            newUser.OtherNames = writeUserDto.OtherNames;
            newUser.Email = writeUserDto.Email;
            newUser.MobileNumber = writeUserDto.MobileNumber;
            newUser.Password = writeUserDto.Password;

            database.AddUser(newUser);
            
            user = mapper.Map<Models.User>(newUser);
            return user;
        }

        public Models.User GetUser(Guid id)
         {
             Models.User user = new Models.User();
             try 
             {
                 //var userEntity = dataBaseContext.Users.Where(p => p.Id == id).Single();
                 var userEntity = database.GetUserById(id);
                 user = mapper.Map<Models.User>(userEntity);


             }catch(Exception e) 
             {  
                 Console.WriteLine(e.Message);
             }


             return user;

         }

        public void UpdateUser(Guid id, WriteUserDto writeUserDto)
        {
            try
            {
                database.UpdateUser(id, writeUserDto);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteUser(Guid id)
        {
            Entities.User user = database.GetUserById(id);

            if (user != null) 
            {
                //dataBaseContext.Users.Remove(user);
                database.DeleteUser(id);
                //dataBaseContext.SaveChanges();
            }
        }
    }
}
