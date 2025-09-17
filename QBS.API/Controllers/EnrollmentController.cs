using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QABS.Models;
using QABS.Service;
using QABS.ViewModels;
using System.Threading.Tasks;

namespace QABS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class EnrollmentController : ControllerBase
    {
        private readonly EnrollmentService enrollmentService;
        public EnrollmentController(EnrollmentService enrollmentService)
        {
            this.enrollmentService = enrollmentService;
        }

        [HttpPost("CreateEnrollment")]
        public async Task<IActionResult> CreateEnrollment([FromForm] EnrollmentCreateVM vm)
        {
            var result = await enrollmentService.CreateEnrollment(vm);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

        [HttpGet("GetAllEnrollments")]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var result = await enrollmentService.GetAllEnrollments();
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchEnrollment(
          [FromQuery] string? studentId = "",
          [FromQuery] string? teacherId = "",
          [FromQuery] DateTime? startDate = null ,
          [FromQuery] int? day = null,
          [FromQuery] EnrollmentStatus? status = null,
          [FromQuery] bool descending = false,
          [FromQuery] int pageSize = 5,
          [FromQuery] int pageIndex = 1)
        {
            var result = await enrollmentService.SearchEnrollmentList(
                studentId, teacherId, startDate,day,status, descending, pageSize, pageIndex);
            if (!result.IsSuccess)
                return new JsonResult(result);
            return new JsonResult(result);
        }
        [HttpGet("GetEnrollmentsByStudentId/{id}")]
        public async Task<IActionResult> GetEnrollmentsByStudentId(string id)
        {
            var result = await enrollmentService.GetEnrollmentsByStudentId(id);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

        [HttpGet("GetEnrollmentById/{id}")]
        public async Task<IActionResult> GetEnrollmentById(int id)
        {
            var result = await enrollmentService.GetEnrollmentById(id);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }
        [HttpGet("GetEnrollmentsByTeacherId/{id}")]
        public async Task<IActionResult> GetEnrollmentsByTeacherId(string id)
        {
            var result = await enrollmentService.GetEnrollmentsByTeacherId(id);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

        //[HttpGet("SearchEnrollmentsByDate")]
        //public async Task<IActionResult> SearchEnrollmentsByDate([FromQuery]DateTime date)
        //{
        //    var result = await enrollmentService.SearchEnrollmentsByDate(date);
        //    if (result.IsSuccess)
        //    {
        //        return new JsonResult(result);
        //    }
        //    return new JsonResult(result);
        //}





    }
}
