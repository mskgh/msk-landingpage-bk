using Asp.Versioning;
using main.src.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace main.src.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DemoController : Controller
    {
        [HttpGet("dotnet/success")]
        public ActionResult<SendSuccessDto<string>> SendSuccessfulMessageDotNet()
        {
            SendSuccessDto<string> sendSuccessDto = new SendSuccessDto<string>();
            List<Data<string>> data1 = new List<Data<string>>();


            sendSuccessDto.Statuscode = 200;
            //sendSuccessDto.Message = "Everything works out well!";
            sendSuccessDto.data = data1;
           
            return Ok(sendSuccessDto);
        }

        [HttpGet("dotnet/error")]
        public ActionResult<SendErrorDto> SendErrorMessageDotNet()
        {
            SendErrorDto sendErrorDto = new SendErrorDto();

            sendErrorDto.Statuscode = 404;
            sendErrorDto.Error = "Can't find user name";
            //sendErrorDto.Errcode = "MSK_NOT_FOUND";
            return NotFound(sendErrorDto);
        }
       
    }
}
