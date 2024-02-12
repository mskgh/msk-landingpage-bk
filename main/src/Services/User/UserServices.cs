
using AutoMapper;
using main.src.Dtos;
using main.src.Repositories;

namespace main.src.Services.User
{
    public class UserServices : IUserServices
    {
        DataBaseContext dataBaseContext;
        IMapper mapper;
        public UserServices(DataBaseContext dataBaseContext, IMapper mapper) 
        {
            this.dataBaseContext = dataBaseContext;
            this.mapper = mapper;
        }

        public List<Models.User> GetAllUsers()
        {
            List<Entities.User> userEntity = dataBaseContext.Users.ToList();

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

            dataBaseContext.Users.Add(newUser);
            dataBaseContext.SaveChanges();

            user = mapper.Map<Models.User>(newUser);
            return user;
        }

        public Models.User GetUser(Guid id)
         {
             Models.User user = new Models.User();
             try 
             {
                 var userEntity = dataBaseContext.Users.Where(p => p.Id == id).Single();

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
                var userEntity = dataBaseContext.Users.Find(id);

                if(userEntity != null) 
                {
                    userEntity.FirstName = writeUserDto.FirstName;
                    userEntity.LastName = writeUserDto.LastName;
                    userEntity.Email = writeUserDto.Email;
                    userEntity.OtherNames = writeUserDto.OtherNames;
                    userEntity.MobileNumber = writeUserDto.MobileNumber;

                    dataBaseContext.SaveChanges();
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteUser(Guid id)
        {
            Entities.User user = dataBaseContext.Users.Find(id);

            if (user != null) 
            {
                dataBaseContext.Users.Remove(user);
                dataBaseContext.SaveChanges();
            }
        }
    }
}
