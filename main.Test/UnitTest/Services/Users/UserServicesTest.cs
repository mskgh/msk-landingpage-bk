using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using main.src.Repositories;
using main.src.Services.User;
using main.src;
using main.src.Models;
using main.src.Entities;


namespace main.Test.UnitTest.Services.Users
{
    public class UserServicesTest
    {
        DataBaseContext dataBaseContext;
        IMapper mapper;
        IUserServices userServices;
        public UserServicesTest()
        {
            dataBaseContext = new DataBaseContext();
            mapper = A.Fake<IMapper>();
            userServices = A.Fake<IUserServices>();
        }

        [Fact]
        public void UserServices_GetAllUsers_ReturnListofUsers()
        {
            //Arrange
            //var users = A.Fake<List<src.Models.User>>();
            //var entityUsers = A.Fake<List<src.Entities.User>>();
            List<src.Models.User> users = new List<src.Models.User>();
            src.Models.User users1 = new src.Models.User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "j@gmail.com",
                MobileNumber = "02020200202"
            };
            users.Add(users1);
            A.CallTo(() => userServices.GetAllUsers()).Returns(users);

            //Act
            var result = userServices.GetAllUsers();
            

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(List<src.Models.User>));
            A.CallTo(()=>userServices.GetAllUsers()).MustHaveHappenedOnceExactly();
        }
    }
}
