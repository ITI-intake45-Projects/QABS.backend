using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QABS.Service;
using QABS.ViewModels;

namespace QABS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService studentService;
        public StudentController(StudentService studentService)
        {
            this.studentService = studentService;
        }


        [HttpGet("SearchStudents")]
        public async Task<IActionResult> SearchStudents(
            string name = "",
            bool descending = false,
            int pageSize = 10,
            int pageIndex = 1

            )
        
        {
            var result = await studentService.StudentsSearch
                (
                name,
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


        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await studentService.GetAllStudents();
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }


        [HttpGet("GetAllStudentList")]
        public async Task<IActionResult> GetAllStudentList()
        {
            var result = await studentService.GetAllStudentList();
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }


        [HttpGet("GetStudentsByName/{name}")]
        public async Task<IActionResult> GetStudentsByName(string name)
        {
            var result = await studentService.GetStudentsByName(name);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }


        [HttpGet("GetStudentById/{id}")]
        public async Task<IActionResult> GetStudentById(string id)
        {
            var result = await studentService.GetStudentById(id);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }



        [HttpGet("GetAllStudentPaymentsByStudentId/{id}")]
        public async Task<IActionResult> GetAllStudentPaymentsByStudentId(string id)
        {
            var result = await studentService.GetAllStudentPaymentsByStudentId(id);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }
        [HttpGet("GetStudentPaymentByEnrollmentId/{id}")]
        public async Task<IActionResult> GetStudentPaymentByEnrollmentId(int id)
        {
            var result = await studentService.GetStudentPaymentByEnrollmentId(id);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

        [HttpPut("EditStudentPaymentStatus")]
        public async Task<IActionResult> EditStudentPaymentStatus([FromBody] StudentPaymentEditVM vm)
        {
            var result = await studentService.EditStudentPaymentStatus(vm);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }
    }
}
