using AutoMapper;
using main.src.Models;
using main.src.Dtos;
using main.src.Services.User;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using main.src.Repositories.DynamoUserDB;

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

        private readonly IDynamoDBUserRepository userRepository;
        public UserController(IUserServices userServices, IMapper mapper, IDynamoDBUserRepository userRepository) 
        { 
            this.userServices = userServices;
            this.mapper = mapper;

            this.userRepository = userRepository;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var users = await userRepository.GetAllUsers();
            var readUserDto = mapper.Map<List<Dtos.ReadUserDto>>(users);
            var sendSuccessDto = new SendSuccessOrErrorDto<ReadUserDto>();
            sendSuccessDto.Success = true;
            sendSuccessDto.Message = "All users";
            sendSuccessDto.data = readUserDto;

            return Ok(sendSuccessDto);
        }

        // GET api/<UserController>/id/ternantId
        [HttpGet("{id}/{ternantId}")]
        public async Task<IActionResult> GetUserById(Guid id,Guid ternantId)
        {
                var user = await userRepository.GetUserById(id,ternantId);

                if ( user == null)
                {
                    var sendFailureDto = new SendSuccessOrErrorDto<ReadUserDto>();
                    sendFailureDto.Success = false;
                    sendFailureDto.Message = "User was not found";
                    
                return NotFound(sendFailureDto);
                }

            var sendSuccessDto = new SendSuccessOrErrorDto<ReadUserDto>();
            sendSuccessDto.Success = true;
            sendSuccessDto.Message = "User found";
            sendSuccessDto.data = new List<ReadUserDto>();
            var readUser = mapper.Map<ReadUserDto>(user);
            sendSuccessDto.data.Add(readUser);
            return Ok(sendSuccessDto);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WriteUserDto userDto)
        {
            if (userDto == null) 
            {
                return BadRequest("No data sent");
            }

            var userWithIds = userServices.AssignIds(userDto);

            

            var result = await userRepository.AddUser(userWithIds);
            if(result == false)
            {
                return BadRequest();
            }

            var readUserDto = mapper.Map<ReadUserDto>(userWithIds);

            var sendSuccessDto = new SendSuccessOrErrorDto<ReadUserDto>();
            sendSuccessDto.Success = true;
            sendSuccessDto.Message = "User created successfully";
            sendSuccessDto.data = new List<ReadUserDto>();
            sendSuccessDto.data.Add(readUserDto);

            return Created("~/api/v1/User/"+readUserDto.Id+"/"+readUserDto.TernantId,sendSuccessDto);
        
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}/{ternantId}")]
        public async Task<IActionResult> Put(Guid id,Guid ternantId, [FromBody] Dtos.UpdateUserDto updateUserDto)
        {
            
            if (updateUserDto == null)
            {
                var sendFailureDto = new SendSuccessOrErrorDto<ReadUserDto>();
                sendFailureDto.Success = false;
                sendFailureDto.Message = "No data sent";

                return BadRequest(sendFailureDto);
            }

            var userExists = await userRepository.CheckIfUserExists(id, ternantId);
            if (userExists == false) 
            {
                var sendFailureDto = new SendSuccessOrErrorDto<ReadUserDto>();
                sendFailureDto.Success = false;
                sendFailureDto.Message = "User with not found";
                return NotFound(sendFailureDto);
            }


            var result = await userRepository.UpdateUser(id,ternantId,updateUserDto);
            if (result == false)
            {
                return BadRequest();
            }

            var sendSuccessDto = new SendSuccessOrErrorDto<ReadUserDto>();
            sendSuccessDto.Success = true;
            sendSuccessDto.Message = "User updated successfully";
            return Created("~/api/v1/User/" + id + "/" + ternantId,sendSuccessDto);
        }

        // DELETE api/<UserController>/id/ternantId
        [HttpDelete("{id}/{ternantId}")]
        public async Task<IActionResult> Delete(Guid id,Guid ternantId)
        {
            var result = await userRepository.DeleteUser(id,ternantId);
            if(result == false)
            {
                var sendFailureDto = new SendSuccessOrErrorDto<ReadUserDto>();
                sendFailureDto.Success = false;
                sendFailureDto.Message = "User not deleted";
                return BadRequest(sendFailureDto); 
            }
            var sendSuccessDto = new SendSuccessOrErrorDto<ReadUserDto>();
            sendSuccessDto.Success = true;
            sendSuccessDto.Message = "User has been successfully deleted";
            return Ok(sendSuccessDto);

        }
    }
}
