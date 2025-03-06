using EventManagerAPI.DTO.Request;
using EventManagerAPI.Services.Implementations;
using EventManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace EventManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventParticipantController : ControllerBase
    {

        private readonly IEPService _epService;
        public EventParticipantController(IEPService ePService)
        {
            _epService = ePService;
        }

        [HttpPost("JoinEvent")]

        public async Task<IActionResult> JoinEventByEventCode([FromBody]JoinEventInfo jeInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (isSuccess, message) = await _epService.JoinEvent(jeInfo);
            if (!isSuccess)
            {
                return BadRequest(new { Message = message });
            }
            return Ok(new { Message = message });
        }


        [HttpPost("GetEventsByUserId")]

        public async Task<IActionResult> GetEventsByUserId(string userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (EPData, message) = await _epService.getEventsByUserId(userId);
            if (EPData == null)
            {
                return BadRequest(new { Message = message });
            }
            return Ok(new { Message = message, Data = EPData });
        }

        [HttpPost("CheckMakeEvent")]

        public async Task<IActionResult> CheckMakeEvent(CheckMakeEventInfo cmei)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (check, message) = await _epService.CheckMakeEvent(cmei.EventId, cmei.UserId);
            if (check == false)
            {
                return Ok(new { Message = message, Data = check });
            }
            return Ok(new { Message = message, Data = check });
        }
    }
}
