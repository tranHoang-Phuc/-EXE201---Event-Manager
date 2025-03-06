using EventManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventManagerAPI.DTO.Request;

namespace EventManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Lấy thông tin Event theo EventId
        /// </summary>
        [HttpGet("getByEventId/{eventId}")]
        public async Task<IActionResult> GetEventById(Guid eventId)
        {
            var (eventData, message) = await _eventService.GetEventById(eventId);
            if (eventData == null)
            {
                return NotFound(new { Message = message });
            }
            return Ok(new { Message = message, Data = eventData });
        }

        /// <summary>
        /// Lấy danh sách Event theo OrganizerId
        /// </summary>
        [HttpGet("getByOrganizer/{organizerId}")]
        public async Task<IActionResult> GetEventsByOrganizerId(string organizerId)
        {
            var (events, message) = await _eventService.GetEventByOrganizedId(organizerId);
            if (!events.Any())
            {
                return NotFound(new { Message = message });
            }
            return Ok(new { Message = message, Data = events });
        }

        /// <summary>
        /// Tạo một Event mới
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateEvent([FromBody] EventInfo eventInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (isSuccess, message) = await _eventService.CreateEvent(eventInfo);
            if (!isSuccess)
            {
                return BadRequest(new { Message = message });
            }
            return Ok(new { Message = message });
        }

        /// <summary>
        /// Cập nhật thông tin Event
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateEvent([FromBody] EventInfo eventInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (isSuccess, message) = await _eventService.UpdateEvent(eventInfo);
            if (!isSuccess)
            {
                return BadRequest(new { Message = message });
            }
            return Ok(new { Message = message });
        }

        /// <summary>
        /// Xóa một Event theo EventId
        /// </summary>
        [HttpDelete("delete/{eventId}")]
        public async Task<IActionResult> DeleteEvent(Guid eventId)
        {
            var (isSuccess, message) = await _eventService.DeleteEvent(eventId);
            if (!isSuccess)
            {
                return Ok(new { Message = message });
            }
            return Ok(new { Message = message });
        }
    }
}
