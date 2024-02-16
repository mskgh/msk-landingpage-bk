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
        IMapper mapper;
        public UserController(IUserServices userServices, IMapper mapper) 
        { 
            this.userServices = userServices;
            this.mapper = mapper;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<List<Dtos.ReadUserDto>>> Get()
        {
            List<Models.User> users = userServices.GetAllUsers();

            var userDtos = mapper.Map<List<ReadUserDto>>(users);

            return Ok(userDtos);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dtos.ReadUserDto>> GetById(Guid id)
        {
            Models.User userModel = userServices.GetUser(id);

            var readUserDto = mapper.Map<ReadUserDto>(userModel);

            return Ok(readUserDto);
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] WriteUserDto userDto)
        {
            if (userDto == null) 
            {
                return BadRequest("No data sent");
            }
            var newUser = userServices.AddUser(userDto);

            ReadUserDto readUserDto = mapper.Map<ReadUserDto>(newUser);

            var routeValue = new { readUserDto.Id };
            var actionName = nameof(GetById);

            return CreatedAtAction(actionName, routeValue,readUserDto);


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
