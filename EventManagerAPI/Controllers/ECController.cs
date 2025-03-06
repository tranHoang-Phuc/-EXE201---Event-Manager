using EventManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ECController : ControllerBase
    {

        private readonly IECService _ecService;

        public ECController(IECService ecService)
        {
            _ecService = ecService;
        }

        [HttpGet("/GetEventCagetory")]

        public async Task<IActionResult> GetEventCagetory()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (ECData, message) = await _ecService.GetCategories();
            if (ECData == null)
            {
                return BadRequest(new { Message = message });
            }
            return Ok(new { Message = message, Data = ECData });
        }
    }
}
