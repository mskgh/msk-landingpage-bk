using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using main.src.Controllers;
using main.src.Dtos;
using main.src.Services.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main.Test.UnitTest.Controllers
{
    public class UserControllerTest
    {
        private readonly IUserServices userServices;
        private readonly IMapper mapper;
        public UserControllerTest()
        {
            userServices = A.Fake<IUserServices>();
            mapper = A.Fake<IMapper>();
        }

        /*[Fact]
        public void UserController_Get_ReturnOk()
        {
            //Arrange
            var users = userServices.GetAllUsers();
            var fakeReadUserDto = A.Fake<List<ReadUserDto>>();
            A.CallTo(() => mapper.Map<List<ReadUserDto>>(users)).Returns(fakeReadUserDto);
            var controller = new UserController(userServices,mapper);
            
            //Act
            var result = controller.Get();

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<ActionResult<List<src.Dtos.ReadUserDto>>>));
            
        }*/

       /* [Fact]
        public void UserController_Post_ReturnActionResult() 
        {
            //Arrange
            //var users = userServices.AddUser();
            var controller = new UserController(userServices,mapper);
            var userWriteDto = A.Fake<WriteUserDto>();
            //Act
            var result = controller.Post(userWriteDto);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<IActionResult>));
        }*/
    }
}
