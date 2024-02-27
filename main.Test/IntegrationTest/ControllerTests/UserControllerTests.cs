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
        public async Task UserController_GetAllUsers_ReturnsOk()
        {
            var response = await client.GetAsync("/api/v1/user");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task UserController_Get_ReturnsOk() 
        {
            var response = await client.GetAsync("/api/v1/user/c9a2ecf0-13b1-4de1-a042-c3a93bb38585/40408eea-4082-4408-a112-0acb397e27c7");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task UserController_AddUser_ReturnsOk()
        {
            var writeUserDto = new src.Dtos.UpdateUserDto();
            writeUserDto.FirstName = "John";
            writeUserDto.LastName = "Doe";
            writeUserDto.OtherNames = "K";
            writeUserDto.MobileNumber = "1234567890";
            writeUserDto.Email = "e@gmail.com";
          
            var json = JsonConvert.SerializeObject(writeUserDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/api/v1/user/c9a2ecf0-13b1-4de1-a042-c3a93bb38585/40408eea-4082-4408-a112-0acb397e27c7", data);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UserController_UpdateUser_ReturnsOk()
        {
            var updateUserDto = new src.Dtos.UpdateUserDto();
            updateUserDto.FirstName = "John";
            updateUserDto.LastName = "Doe";
            updateUserDto.OtherNames = "M";
            updateUserDto.MobileNumber = "1234567890";
            updateUserDto.Email = "e@gmail.com";
           
            var json = JsonConvert.SerializeObject(updateUserDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/api/v1/user/c9a2ecf0-13b1-4de1-a042-c3a93bb38585/40408eea-4082-4408-a112-0acb397e27c7", data);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UserController_DeleteUser_ReturnsOk()
        {
            var response = await client.DeleteAsync("/api/v1/user/6f020aed-d8ea-4386-885c-f86f183f8674/b90a69a5-78ae-477d-b687-3c811d811bc9");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        public void Dispose()
        {
            client.Dispose();
            factory.Dispose();
        }
    }
}
