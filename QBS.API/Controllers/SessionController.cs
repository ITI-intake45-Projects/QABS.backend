using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QABS.Service;
using QABS.ViewModels;

namespace QABS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly SessionService _sessionService;
        public SessionController(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet("GetAllSessionsByEnrollmentId/{id}")]
        public async Task<IActionResult> GetAllSessionsByEnrollmentId(int id)
        {
            var result = await _sessionService.GetAllSessionsByEnrollmentId(id);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }


        [HttpPost("CreateSessions")]
        public async Task<IActionResult> CreateSessions([FromBody] List<SessionCreateVM> sessionsVm)
        {
            var result = await _sessionService.CreateSessions(sessionsVm);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

        [HttpPut("UpdateSession")]
        public async Task<IActionResult> UpdateSession([FromBody] SessionEditVM sessionsVm)
        {
            var result = await _sessionService.UpdateSession(sessionsVm);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

    }
}
