

using Microsoft.AspNetCore.Mvc;
using QABS.Repository;
using QABS.ViewModels;
using Utilities;
using System.Net;

namespace QABS.Service
{
    public class Service
    {
      private readonly UnitOfWork _unitOfWork;

        public Service(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> CreateEnrollment([FromForm] EnrollmentCreateVM vm)
        {
            try
            {
                if (vm == null)
                {
                    return ServiceResult.FailureResult("Invalid enrollment data.");
                }
               await _unitOfWork._enrollmentRepository.AddAsync(vm.ToCreate());
               await _unitOfWork.SaveChangesAsync();

                return ServiceResult.SuccessResult("Enrollment created successfully." ,HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult.FailureResult(ex.Message);
            }

            
        }
    }
}
