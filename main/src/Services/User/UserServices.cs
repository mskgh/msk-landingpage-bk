
using AutoMapper;
using main.src.Dtos;
using main.src.Repositories;

namespace main.src.Services.User
{
    public class UserServices : IUserServices
    {
        DataBaseContext dataBaseContext;
        public UserServices(DataBaseContext dataBaseContext) 
        {
            this.dataBaseContext = dataBaseContext;
        }

        public void AddUser(Dtos.WriteUserDto user)
        {
            if (user.Name == null) 
            {
                Console.WriteLine("Its null from service");
                return;
            }
            Console.WriteLine("data inserted");
            ENTITIES.User newUser = new ENTITIES.User();
            newUser.Id = Guid.NewGuid();
            newUser.Name = user.Name;

            dataBaseContext.Users.Add(newUser);
            dataBaseContext.SaveChanges();
        }

        public void DeleteUser(Guid id)
        {
            ENTITIES.User user = dataBaseContext.Users.Find(id);

            if (user != null) 
            {
                dataBaseContext.Users.Remove(user);
                dataBaseContext.SaveChanges();
            }
        }

        public List<Models.User> GetAllUsers()
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<ENTITIES.User, Models.User>()) ;

            List<ENTITIES.User> userEntity = dataBaseContext.Users.ToList();

            foreach (var entity in userEntity)
            {
                Console.WriteLine(entity.Name);
            }
            var mapper = new Mapper(config);

            List<Models.User> users = mapper.Map<List<Models.User>>(userEntity);

            return users;
        }

        public Models.User GetUser(Guid id)
        {
            Models.User user = new Models.User();
            try 
            {
                var userEntity = dataBaseContext.Users.Where(p => p.Id == id).Single();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<ENTITIES.User, Models.User>());

                var mapper = new Mapper(config);

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
                    userEntity.Name = writeUserDto.Name;

                    dataBaseContext.SaveChanges();
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
