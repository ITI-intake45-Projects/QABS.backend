
using Microsoft.AspNetCore.Mvc;
using QABS.Repository;
using QABS.ViewModels;
using System.Net;
using Utilities;

namespace QABS.Service
{
    public class SessionService
    {

        private readonly UnitOfWork _unitOfWork;

        public SessionService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ServiceResult<List<SessionDetailsVM>>> GetAllSessionsByEnrollmentId(int id)
        {
            try
            {
                var sessions = await _unitOfWork._sessionRepository.GetSessionsByEnrollmentIdAsync(id);
                return ServiceResult<List<SessionDetailsVM>>.SuccessResult(sessions, "Sessions retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<List<SessionDetailsVM>>.FailureResult(ex.Message);
            }
        }

        public async Task<ServiceResult> CreateSession([FromForm] SessionCreateVM vm)
        {
            try
            {
                if (vm == null)
                {
                    return ServiceResult.FailureResult("Invalid session data.");
                }
                var session = _unitOfWork._sessionRepository.AddAsync(vm.ToCreate());
                await _unitOfWork.SaveChangesAsync();
                if (session == null)
                {
                    return ServiceResult.FailureResult("Failed to create session.");
                }
                return ServiceResult.SuccessResult("Session created successfully.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult.FailureResult(ex.Message);
            }
        }

        public async Task<ServiceResult> UpdateSession(SessionEditVM vm)
        {
            try
            {
                var session = await _unitOfWork._sessionRepository.GetByIdAsync(vm.Id);
                if (session == null)
                {
                    return ServiceResult.FailureResult("Session not found.");
                }
                await _unitOfWork._sessionRepository.UpdateAsync(vm.ToEdit(session));
                await _unitOfWork.SaveChangesAsync();
                return ServiceResult.SuccessResult("Session updated successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult.FailureResult(ex.Message);
            }
        }



    }
}
