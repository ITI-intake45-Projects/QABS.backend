
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
        public async Task<ServiceResult<List<SessionDetailsVM>>> GetTodaySessionsAsync()
        {
            try
            {
                var today = DateTime.UtcNow.Date; // بنستخدم UTC عشان نتفادى مشاكل الـ TimeZone

                var sessions = await _unitOfWork._sessionRepository
                    .GetList(s => s.StartTime.HasValue && s.StartTime.Value.Date == today)
                    .Select(s => s.ToDetails())
                    .ToListAsync();

                if (sessions == null || !sessions.Any())
                {
                    return ServiceResult<List<SessionDetailsVM>>.FailureResult("No sessions found for today.");
                }

                return ServiceResult<List<SessionDetailsVM>>.SuccessResult(sessions, "Today's sessions retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResult<List<SessionDetailsVM>>.FailureResult(ex.Message);
            }
        }


        public async Task<ServiceResult<PaginationVM<SessionEnrollmentDetailsVM>>> SearchSessions(
            DateTime? startDate = null,
            bool descending = false,
            int pageSize = 10,
            int pageIndex = 1
)
        {
            try
            {
                // لو startDate قيمته null، هيتحدد تلقائياً على إنه تاريخ اليوم فقط.
                if (!startDate.HasValue)
                {
                    startDate = DateTime.Today;
                }
                var sessions = await _unitOfWork._sessionRepository.SearchSessions(
                    startDate,
                    descending,
                    pageSize,
                    pageIndex
                );

                return ServiceResult<PaginationVM<SessionEnrollmentDetailsVM>>.SuccessResult(
                    sessions,
                    "Sessions retrieved successfully."
                );
            }
            catch (Exception ex)
            {
                return ServiceResult<PaginationVM<SessionEnrollmentDetailsVM>>.FailureResult(ex.Message);
            }
        }



        public async Task<ServiceResult> CreateSessions(SessionCreateVM sessionsVm)
        {
            try
            {
                if (sessionsVm == null)
                {
                    return ServiceResult.FailureResult("Invalid session data.");
                }

                // Fetch the enrollment details based on the single object
                var enrollment = await _unitOfWork._enrollmentRepository.GetByIdAsync(sessionsVm.EnrollmentId);

                if (enrollment == null)
                {
                    return ServiceResult.FailureResult("Enrollment not found.");
                }

                // Calculate amount for the single session and map it to the entity
                sessionsVm.Amount = enrollment.Teacher.HourlyRate *
                                    (decimal)((int)enrollment.SubscriptionPlan.Duration / 60.00);

                // Convert the single ViewModel to a Session entity
                var session = sessionsVm.ToCreate();

                // Add the single session to the repository
                await _unitOfWork._sessionRepository.AddAsync(session);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.SuccessResult("Session created successfully.", HttpStatusCode.Created);
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
