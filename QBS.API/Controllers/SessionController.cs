using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QABS.Service;
using QABS.ViewModels;

namespace QABS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]

    public class SessionController : ControllerBase
    {
        private readonly SessionService _sessionService;
        public SessionController(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet("SearchSessions")]
        public async Task<IActionResult> SearchSessions(
            DateTime? startDate = null,
            bool descending = false,
            int pageSize = 10,
            int pageIndex = 1
)
        {
            var result = await _sessionService.SearchSessions(
                startDate,
                descending,
                pageSize,
                pageIndex
            );
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }

            return new JsonResult(result);
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

        [HttpGet("GetCompletedSessionsByTeacherId/{id}")]
        public async Task<IActionResult> GetCompletedSessionsByTeacherId(string id)
        {
            var result = await _sessionService.GetCompletedSessionsDetailsByTeacherId(id);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }




        [HttpPost("CreateSession")]
        public async Task<IActionResult> CreateSessions([FromBody] SessionCreateVM sessionsVm)
        {
            var result = await _sessionService.CreateSessions(sessionsVm);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

        [HttpPut("UpdateSession")]
        public async Task<IActionResult> UpdateSession([FromBody] SessionEditVM vm)
        {

            
            var result = await _sessionService.UpdateSession(vm);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

    }
}
