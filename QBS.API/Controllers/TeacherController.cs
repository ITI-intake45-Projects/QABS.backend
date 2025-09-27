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

        [HttpGet("GetAllTeacherList")]
        public async Task<IActionResult> GetAllTeacherList()
        {
            var result = await teacherService.GetAllTeacherList();
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

        [HttpGet("GetAllTeacherListMoreInfo")]
        public async Task<IActionResult> GetAllTeacherListMore()
        {
            var result = await teacherService.GetAllTeacherListMore();
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }


        [HttpGet("GetTeacherById/{teacherId}")]
        public async Task<IActionResult> GetTeacherById(string teacherId)
        {
            var result = await teacherService.GetTeacherById(teacherId);
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
        [HttpGet("GetEnrolledStudentsByTeacherId/{teacherId}")]
        public async Task<IActionResult> GetEnrolledStudentsByTeacherId(string teacherId)
        {
            var result = await teacherService.GetEnrolledStudentsById(teacherId);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

        [HttpPost("CreatePayTeacherByCompletedSessions")]
        public async Task<IActionResult> CreatePayTeacherByCompletedSessions([FromForm] TeacherPayoutCreateVM vm)
        {
            var result = await teacherService.CreatePayTeacherByCompletedSessions(vm);
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
        public async Task<IActionResult> CreateTeacherAvaliability([FromBody] TeacherAvailabilityCreateVM vm)
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


        [HttpDelete("DeleteTeacherAvaliability/{id}")]
        public async Task<IActionResult> DeleteTeacherAvaliability(int id)
        {
            var result = await teacherService.DeleteTeacherAvaliability(id);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }


        //[HttpDelete("DeleteTeacher/{id}")]
        //public async Task<IActionResult> DeleteTeacher(string id)
        //{
        //    var result = await teacherService.DeleteTeacherAsync(id);

        //    if (result.IsSuccess)
        //    {
        //        return new JsonResult(result);
        //    }

        //    return new JsonResult(result);
        //}

    }
}
