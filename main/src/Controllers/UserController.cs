using AutoMapper;
using main.src.Models;
using main.src.Dtos;
using main.src.Services.User;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace main.src.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        IUserServices userServices;
        public UserController(IUserServices userServices) 
        { 
            this.userServices = userServices;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<List<Dtos.ReadUserDto>>> Get()
        {
            List<Models.User> users = userServices.GetAllUsers();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Models.User, ReadUserDto>());

            var mapper = new Mapper(config);

            List<ReadUserDto> userDtos = mapper.Map<List<ReadUserDto>>(users);
            return Ok(userDtos);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dtos.ReadUserDto>> Get(Guid id)
        {
            Models.User userModel = userServices.GetUser(id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Models.User, ReadUserDto>());

            var mapper = new Mapper(config);

            var readUserDto = mapper.Map<ReadUserDto>(userModel);

            return Ok(readUserDto);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WriteUserDto userDto)
        {
            if (userDto == null) 
            {
                return BadRequest("No data sent");
            }
            var newUser = userServices.AddUser(userDto);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Models.User, ReadUserDto>());
            var mapper = new Mapper(config);
            ReadUserDto readUserDto = mapper.Map<ReadUserDto>(newUser);
            return Ok(readUserDto);


        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Dtos.WriteUserDto writeUserDto)
        {
            userServices.UpdateUser(id, writeUserDto);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            userServices.DeleteUser(id);
        }
    }
}
