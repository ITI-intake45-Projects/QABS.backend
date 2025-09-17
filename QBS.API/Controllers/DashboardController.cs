using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QABS.Service;

namespace QABS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService dashboardService;
        public DashboardController(DashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        
        [HttpGet("GetDashboardData")]
        public IActionResult GetDashboardData()
        {
            var result = dashboardService.GetDashboardDetailsAsync();
            if (result.Result.IsSuccess)
            {
                return new JsonResult(result.Result);
            }
            return new JsonResult(result);
        }
    }
}
