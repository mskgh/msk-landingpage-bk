using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using main.src.Controllers;
using main.src.Repositories.DynamoUserDB;
using main.src.Dtos;
using main.src.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace main.Test.UnitTest.Controllers
{
    public class UserControllerTest
    {
        private readonly IUserServices userServices;
        private readonly IMapper mapper;
        private readonly IDynamoDBUserRepository userRepository;
        public UserControllerTest()
        {
            userServices = A.Fake<IUserServices>();
            mapper = A.Fake<IMapper>();
            IDynamoDBUserRepository userRepository = A.Fake<IDynamoDBUserRepository>();
        }

        [Fact]
        public void UserController_Get_ReturnOk()
        {
            //Arrange
            
            var controller = new UserController(userServices,mapper,userRepository);
            
            //Act
            var result = controller.Get();

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<IActionResult>));
            
        }

       [Fact]
        public void UserController_Post_ReturnActionResult() 
        {
            //Arrange
            //var users = userServices.AddUser();
            var controller = new UserController(userServices,mapper,userRepository);
            var userWriteDto = A.Fake<WriteUserDto>();
            //Act
            var result = controller.Post(userWriteDto);

            //Assert
            result.Should().NotBeNull();
            
            result.Should().BeOfType(typeof(Task<IActionResult>));
        }
    }
}
