using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QABS.Service;
using QABS.ViewModels;

namespace QABS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherService teacherService;
        public TeacherController(TeacherService teacherService)
        {
            this.teacherService = teacherService;
        }
        [HttpGet("GetAllTeachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var result = await teacherService.GetAllTeachers();
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }
        [HttpGet("GetTeachersByName/{name}")]
        public async Task<IActionResult> GetTeachersByName(string name)
        {
            var result = await teacherService.GetTeachersByName(name);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }
        [HttpGet("GetSessionsByTeacherPayoutId/{teacherPayoutId}")]
        public async Task<IActionResult> GetSessionsByTeacherPayoutId(int teacherPayoutId)
        {
            var result = await teacherService.GetSessionsByTeacherPayoutId(teacherPayoutId);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }
        [HttpGet("GetEnrolledStudentsById/{studentId}")]
        public async Task<IActionResult> GetEnrolledStudentsById(string studentId)
        {
            var result = await teacherService.GetEnrolledStudentsById(studentId);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

        [HttpPost("PayTeacherByCompletedSessions")]
        public async Task<IActionResult> PayTeacherByCompletedSessions([FromBody] TeacherPayoutCreateVM vm)
        {
            var result = await teacherService.PayTeacherByCompletedSessions(vm);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

        [HttpGet("GetPayoutByMonthAsync")]
        public async Task<IActionResult> GetPayoutByMonthAsync([FromQuery] DateTime date)
        {
            var result = await teacherService.GetPayoutByMonthAsync(date);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

        [HttpPost("CreateTeacherAvaliability")]
        public async Task<IActionResult> CreateTeacherAvaliability([FromBody] List<TeacherAvailabilityCreateVM> vm)
        {
            var result = await teacherService.CreateTeacherAvaliability(vm);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

        [HttpPut("UpdateTeacherAvaliability")]
        public async Task<IActionResult> UpdateTeacherAvaliability([FromBody] TeacherAvailabilityEditVM vm)
        {
            var result = await teacherService.UpdateTeacherAvaliability(vm);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

    }
}
