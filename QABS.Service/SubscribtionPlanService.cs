

using QABS.Repository;
using QABS.ViewModels;
using System.Net;
using Utilities;

namespace QABS.Service
{
    public class SubscribtionPlanService
    {
        private readonly UnitOfWork _unitOfWork;

        public SubscribtionPlanService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> CreateSubscribtionPlan(SubscribtionPlanCreateVM vm)
        {
            try
            {
                if (vm == null)
                {
                    return ServiceResult.FailureResult("Invalid subscription plan data.", HttpStatusCode.BadRequest);
                }

                var existingPlan = await _unitOfWork._subscribtionPlanRepository.DoesNameExist(vm.Name);
                if (existingPlan)
                {
                    return ServiceResult.FailureResult("A subscription plan with this name already exists.", HttpStatusCode.Conflict);
                }

                var subscribtionPlan = vm.ToCreate();
                await _unitOfWork._subscribtionPlanRepository.AddAsync(subscribtionPlan);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.SuccessResult("Subscription plan created successfully.", HttpStatusCode.Created);

            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult.FailureResult($"An error occurred while creating the subscription plan: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ServiceResult<List<SubscribtionPlanDetailsVM>>> GetSubscribtionPlans()
        {
            try
            {
                var plans = await _unitOfWork._subscribtionPlanRepository.GetSubscribtionPlans();
                if (plans == null || !plans.Any())
                {
                    return ServiceResult<List<SubscribtionPlanDetailsVM>>.FailureResult("No subscription plans found.", HttpStatusCode.NotFound);
                }
                return ServiceResult<List<SubscribtionPlanDetailsVM>>.SuccessResult(plans, "Subscription plans retrieved successfully.", HttpStatusCode.OK);

            }
            catch
            {
                return ServiceResult<List<SubscribtionPlanDetailsVM>>.FailureResult("An error occurred while retrieving subscription plans.", HttpStatusCode.InternalServerError);
            }
        }
    }
}
