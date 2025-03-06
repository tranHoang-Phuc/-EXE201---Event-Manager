using EventManagerAPI.DTO.Request;
using EventManagerAPI.Models;
using EventManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaService _agendaService;

        public AgendaController(IAgendaService agendaService)
        {
            _agendaService = agendaService;
        }

        /// <summary>
        /// Lấy danh sách Agenda theo EventId
        /// </summary>
        [HttpGet("getByEvent/{eventId}")]
        public async Task<IActionResult> GetAgendasByEventId(Guid eventId)
        {
            var (agendas, message) = await _agendaService.GetAgendasByEventId(eventId);
            if (!agendas.Any())
            {
                return Ok(new { Message = message, Data = agendas });
            }
            return Ok(new { Message = message, Data = agendas });
        }

        /// <summary>
        /// Tạo một Agenda mới
        /// </summary>
        [HttpPost("createAgenda")]
        public async Task<IActionResult> CreateAgenda([FromBody] Agenda agenda)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (isSuccess, message) = await _agendaService.CreateAgenda(agenda);
            if (!isSuccess)
            {
                return BadRequest(new { Message = message });
            }
            return Ok(new { Message = message });
        }

        /// <summary>
        /// Xóa một Agenda theo Id
        /// </summary>
        //[HttpDelete("delete/{agendaId}")]
        //public async Task<IActionResult> DeleteAgenda(Guid agendaId)
        //{
        //    var isDeleted = await _agendaService.DeleteAgenda(agendaId);
        //    if (!isDeleted)
        //    {
        //        return NotFound(new { Message = "Agenda not found or deletion failed!" });
        //    }
        //    return Ok(new { Message = "Agenda deleted successfully!" });
        //}

        /// <summary>
        /// Lưu tất cả Agenda của một Event (Thêm, Sửa, Xóa)
        /// </summary>
        [HttpPost("saveAll/{eventId}")]
        public async Task<IActionResult> SaveAllAgendas(Guid eventId, [FromBody] List<AgendaInfo> updatedAgendasDTO)
        {
            if (updatedAgendasDTO == null || !updatedAgendasDTO.Any())
            {
                return BadRequest(new { Message = "Agenda list cannot be empty" });
            }
            var updatedAgendas = updatedAgendasDTO.Select(dto => new Agenda
            {
                
                AgendaId = dto.AgendaId ?? Guid.NewGuid(),  
                Description = dto.Description,
                TimeStart = dto.TimeStart,
                TimeEnd = dto.TimeEnd,
                EventId = dto.EventId
            }).ToList();

            var (isSuccess, message) = await _agendaService.SaveAllAgendas(eventId, updatedAgendas);
            if (!isSuccess)
            {
                return BadRequest(new { Message = message });
            }
            return Ok(new { Message = message });
        }
    }
}
