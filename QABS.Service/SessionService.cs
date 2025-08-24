
using Microsoft.AspNetCore.Mvc;
using QABS.Models;
using QABS.Repository;
using QABS.ViewModels;
using System.Data.Entity;
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

        public async Task<ServiceResult> CreateSessions(List<SessionCreateVM> sessionsVm)
        {
            try
            {
                if (sessionsVm == null || !sessionsVm.Any())
                {
                    return ServiceResult.FailureResult("Invalid session data.");
                }

                // Assuming all sessions belong to the same enrollment
                var enrollmentId = sessionsVm.First().EnrollmentId;
                var enrollment = await _unitOfWork._enrollmentRepository.GetByIdAsync(enrollmentId);

                if (enrollment == null)
                {
                    return ServiceResult.FailureResult("Enrollment not found.");
                }

                // Calculate amount for each session and map to entity
                var sessions = sessionsVm.Select(vm =>
                {
                    vm.Amount = enrollment.Teacher.HourlyRate *
                                (decimal)((int)enrollment.SubscriptionPlan.Duration / 60.00);
                    return vm.ToCreate();
                }).ToList();

                await _unitOfWork._sessionRepository.AddRangeAsync(sessions);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.SuccessResult($"{sessions.Count} sessions created successfully.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
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

        public async Task HangFireUpdateSessions()
        {
            try
            {
                var nowUtc = DateTime.UtcNow;
                var sessions = await _unitOfWork._sessionRepository.GetList(s => s.Status == SessionStatus.Scheduled && s.StartTime < nowUtc).ToListAsync();

                if (sessions == null || !sessions.Any())
                {
                    return; // No sessions to update
                }

                foreach(var session in sessions)
                {
                    session.Status = SessionStatus.Completed; // Update status to Completed
                }

                await _unitOfWork._sessionRepository.UpdateRangeAsync(sessions);
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error updating sessions: " + ex.Message);
            }
        }

    }
}
