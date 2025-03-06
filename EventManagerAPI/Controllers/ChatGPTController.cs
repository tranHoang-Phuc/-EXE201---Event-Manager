    //using EventManagerAPI.Services;
    //using Microsoft.AspNetCore.Mvc;

    //// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

    //namespace EventManagerAPI.Controllers
    //{
    //    [Route("api/[controller]")]
    //    [ApiController]
    //    public class ChatGPTController : ControllerBase
    //    {
    //        private readonly ChatGPTService _openAIService;

    //        public ChatGPTController(ChatGPTService openAIService)
    //        {
    //            _openAIService = openAIService;
    //        }

    //        [HttpPost("send")]
    //        public async Task<IActionResult> SendMessage([FromBody] string message)
    //        {
    //            try
    //            {
    //                var response = await _openAIService.SendMessageAsync(message);
    //                return Ok(response);
    //            }
    //            catch (Exception ex)
    //            {
    //                return BadRequest(ex.Message);
    //            }
    //        }
    //    }
    //}
