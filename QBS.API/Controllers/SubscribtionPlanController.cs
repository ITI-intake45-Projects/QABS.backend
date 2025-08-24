using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QABS.Service;
using QABS.ViewModels;

namespace QABS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribtionPlanController : ControllerBase
    {
        private readonly SubscribtionPlanService _subscribtionPlanService;
        public SubscribtionPlanController(SubscribtionPlanService subscribtionPlanService)
        {
            _subscribtionPlanService = subscribtionPlanService;
        }

        [HttpPost("CreateSubscribtionPlan")]
        public async Task<IActionResult> CreateSubscribtionPlan([FromBody] SubscribtionPlanCreateVM vm)
        {
            var result = await _subscribtionPlanService.CreateSubscribtionPlan(vm);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

        [HttpGet("GetSubscribtionPlans")]
        public async Task<IActionResult> GetSubscribtionPlans()
        {
            var result = await _subscribtionPlanService.GetSubscribtionPlans();
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }
    }
}
