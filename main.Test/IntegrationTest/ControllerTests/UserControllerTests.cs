using main.src.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace main.Test.IntegrationTest.ControllerTests
{
    public class UserControllerTests:IDisposable
    {
        private CustomWebApplicationFactory factory;
        private HttpClient client;

        public UserControllerTests()
        {
            factory = new CustomWebApplicationFactory();
            client = factory.CreateClient();
        }

        [Fact]
        public async Task UserController_Get_ReturnsOk() 
        {
            var response = await client.GetAsync("/api/v1/user");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UserController_AddUser_ReturnsOk()
        {
            var writeUserDto = new src.Dtos.WriteUserDto();
            writeUserDto.FirstName = "John";
            writeUserDto.LastName = "Doe";
            writeUserDto.OtherNames = "K";
            writeUserDto.MobileNumber = "1234567890";
            writeUserDto.Email = "e@gmail.com";
            writeUserDto.Password = "password";
            var json = JsonConvert.SerializeObject(writeUserDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/v1/user/",data);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }


        public void Dispose()
        {
            client.Dispose();
            factory.Dispose();
        }
    }
}
