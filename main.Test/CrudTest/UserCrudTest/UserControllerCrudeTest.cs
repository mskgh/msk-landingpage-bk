using AutoMapper;
using FakeItEasy;
using main.src.Controllers;
using main.src.Repositories;
using main.src.Services.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main.Test.CrudTest.UserCrudTest
{
    public class UserControllerCrudeTest
    {
        IMapper mapper;
        DataBaseContext dataBaseContext;
        public UserControllerCrudeTest(IMapper mapper,DataBaseContext dataBaseContext)
        {
            this.mapper = mapper;
            this.dataBaseContext = dataBaseContext;
        }
        private List<src.Entities.User> getInitialDbEntities()
        {
            var enititiesUsers = new List<src.Entities.User>();

            return enititiesUsers;
        }

        private DbContextMock<TodoContext> getDbContext(TodoItem[] initialEntities)
        {
            DbContextMock<TodoContext> dbContextMock = new DbContextMock<TodoContext>(new DbContextOptionsBuilder<TodoContext>().Options);
            dbContextMock.CreateDbSetMock(x => x.TodoItems, initialEntities);
            return dbContextMock;
        }

        public void UserController_Get() 
        {
            //Arrange
    
            //DbContextMock<>
            /*src.Entities.User user1 = new src.Entities.User();
            user1.FirstName = "Test";
            user1.LastName = "Test";
            user1.Email = "Test";
            user1.MobileNumber = "Test";
            user1.Password = "Test";
            user1.Id = new Guid;
            user1.OtherNames = "Test";
            
            List<src.Entities.User> users = new List<src.Entities.User>();
            users.Add(user1);

            var dbContext = dataBaseContext.Users.Add(user1);

            var fakeUser = A.Fake<DbSet<src.Entities.User>>();
            
            UserServices userServices = new UserServices(dbContext,mapper);
            UserController userController = new UserController(userServices,mapper);
            //Act
            var result = userController.Get();
            //Assert*/
            
        }
    }
}
