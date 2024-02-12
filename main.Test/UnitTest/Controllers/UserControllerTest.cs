using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using main.src.Controllers;
using main.src.Dtos;
using main.src.Services.User;
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
        public UserControllerTest()
        {
            userServices = A.Fake<IUserServices>();
        }

        [Fact]
        public void UserController_Get_ReturnOk()
        {
            //Arrange
            var users = userServices.GetAllUsers();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<src.Models.User, ReadUserDto>());
            var fakeReadUserDto = A.Fake<List<ReadUserDto>>();
            var mapper = new Mapper(config);
            A.CallTo(() => mapper.Map<List<ReadUserDto>>(users)).Returns(fakeReadUserDto);
            var controller = new UserController(userServices);
            
            //Act
            var result = controller.Get();

            //assert
            result.Should().NotBeNull();
        }
    }
}
