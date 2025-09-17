
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QABS.Models;
using QABS.Repository;
using QABS.ViewModels;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Globalization;
using System.Net;
using Utilities;

namespace QABS.Service
{
    public class EnrollmentService
    {
        private readonly UnitOfWork _unitOfWork;

        public EnrollmentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task<ServiceResult> CreateEnrollment([FromForm] EnrollmentCreateVM vm)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (vm == null)
                    return ServiceResult.FailureResult("Invalid enrollment data.");

                if (vm.studentPayment == null)
                    return ServiceResult.FailureResult("Failed to create enrollment.");

                // ✅ إنشاء Enrollment
                var enrollment = vm.ToCreate();
                enrollment.EndDate = vm.StartDate; // مؤقت
                await _unitOfWork._enrollmentRepository.AddAsync(enrollment);
                await _unitOfWork.SaveChangesAsync(); // لازم عشان يطلع الـ Id

                // ✅ إنشاء Student Payment
                vm.studentPayment.ImageUrl = await UploadMedia.AddImageAsync(vm.studentPayment.ImageFile);
                vm.studentPayment.EnrollmentId = enrollment.Id;

                await _unitOfWork._studentPaymentRepository.UpdateAsync(vm.studentPayment.ToCreate());

                // ✅ معالجة Subscription Plan
                var subscriptionPlan = await _unitOfWork._subscribtionPlanRepository.GetByIdAsync(vm.SubscriptionPlanId);
                if (subscriptionPlan != null)
                {
                    int totalSessions = (int)subscriptionPlan.Type;

                    // تحقق من عدد الأيام المطلوبة
                    int requiredDaysCount = subscriptionPlan.Type switch
                    {
                        SubscriptionType.EightSessions => 2,
                        SubscriptionType.TwelveSessions => 3,
                        SubscriptionType.SixteenSessions => 4,
                        _ => 0
                    };

                    if (vm.DaysOfWeek == null || vm.DaysOfWeek.Count != requiredDaysCount)
                    {
                        await transaction.RollbackAsync();

                        return ServiceResult.FailureResult($"يجب اختيار {requiredDaysCount} أيام في الأسبوع لهذه الخطة.");

                    }

                    var teacher = await _unitOfWork._teacherRepository.GetByIdAsync(vm.TeacherId);
                    if (teacher == null)
                        throw new Exception("Teacher not found");

                    // ✅ إنشاء Sessions
                    var sessions = GenerateSessions(
                        enrollment.Id,
                        vm.StartDate,
                        vm.DaysOfWeek,
                        vm.StartTime,
                        totalSessions,
                        subscriptionPlan.Duration,
                        teacher.HourlyRate
                    );

                    await _unitOfWork._sessionRepository.AddRangeAsync(sessions);
                    enrollment.EndDate = sessions.Max(s => s.StartTime)?.Date;
                    await _unitOfWork._enrollmentRepository.UpdateAsync(enrollment);
                }

                // ✅ حفظ كل التغييرات مرة واحدة
                await _unitOfWork.SaveChangesAsync();

                // ✅ تأكيد العملية
                await transaction.CommitAsync();

                return ServiceResult.SuccessResult("Enrollment and Student Payment created successfully.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                // ❌ في حالة أي خطأ → Rollback
                await transaction.RollbackAsync();
                return ServiceResult.FailureResult(ex.Message);
            }
        }


        
        private List<Session> GenerateSessions(
           int enrollmentId,
           DateTime startDate,
           List<DayOfWeek> daysOfWeek,
           TimeSpan startTime,
           int totalSessions,
           SessionDurationType duration,
           decimal? hourlyRate)
        {
            var sessions = new List<Session>();
            int created = 0;
            DateTime currentDate = startDate;
            //var sessionCreate = new SessionCreateVM();

            while (created < totalSessions)
            {
                if (daysOfWeek.Contains(currentDate.DayOfWeek))
                {
                    
                    sessions.Add(new Session
                    {
                        EnrollmentId = enrollmentId,
                        StartTime = currentDate.Date + startTime,
                        Status = SessionStatus.Scheduled,
                        Amount = hourlyRate * (decimal)((int)duration / 60.0)
                    });

                    created++;
                }

                currentDate = currentDate.AddDays(1);
            }

            return sessions;
        }

        public async Task<ServiceResult<PaginationVM<EnrollmentListVM>>> SearchEnrollmentList
            (
              string? studentId = "",
              string? teacherId = "",
              DateTime? startDate = null,
              int? day = null,
              EnrollmentStatus? status = null,
              bool descending = false,
              int pageSize = 5,
              int pageIndex = 1
            )
        {
            try
            {
                var enrollments = await _unitOfWork._enrollmentRepository.SearchEnrollmentList(
                    studentId, teacherId, startDate,day,status, descending, pageSize, pageIndex);

                return ServiceResult<PaginationVM<EnrollmentListVM>>.SuccessResult(enrollments, "Enrollments retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<PaginationVM<EnrollmentListVM>>.FailureResult(ex.Message);
            }
        }


        public async Task<ServiceResult<PaginationVM<EnrollmentDetailsVM>>> GetAllEnrollments()
        {
            try
            {
                var enrollments = await _unitOfWork._enrollmentRepository.GetAllEnrollmentsAsync();

                return ServiceResult<PaginationVM<EnrollmentDetailsVM>>.SuccessResult(enrollments, "Enrollments retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<PaginationVM<EnrollmentDetailsVM>>.FailureResult(ex.Message);
            }
        }

        public async Task<ServiceResult<EnrollmentDetailsVM>> GetEnrollmentById(int id)
        {
            try
            {
                var enrollment = _unitOfWork._enrollmentRepository
                    .GetList(e => e.Id == id)
                    .FirstOrDefault();

                if (enrollment == null)
                {
                    return ServiceResult<EnrollmentDetailsVM>.FailureResult("Enrollment not found.");
                }

                var result = enrollment.ToDetails();

                return ServiceResult<EnrollmentDetailsVM>.SuccessResult(result, "Enrollment retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<EnrollmentDetailsVM>.FailureResult(ex.Message);
            }
        }


        public async Task<ServiceResult<PaginationVM<EnrollmentDetailsVM>>> GetEnrollmentsByStudentId(string studentId)
        {
            try
            {
                var enrollments = await _unitOfWork._enrollmentRepository.SearchEnrollmentsByStudentId(studentId);
                return ServiceResult<PaginationVM<EnrollmentDetailsVM>>.SuccessResult(enrollments, "Enrollment retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<PaginationVM<EnrollmentDetailsVM>>.FailureResult(ex.Message);
            }
        }
       
        public async Task<ServiceResult<PaginationVM<EnrollmentDetailsVM>>> GetEnrollmentsByTeacherId(string teacherId)
        {
            try
            {
                var enrollments = await _unitOfWork._enrollmentRepository.SearchEnrollmentsByTeacherId(teacherId);
                return ServiceResult<PaginationVM<EnrollmentDetailsVM>>.SuccessResult(enrollments, "Enrollment retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<PaginationVM<EnrollmentDetailsVM>>.FailureResult(ex.Message);
            }
        }
        // Hang Fire : 
        public async Task HangFireUpdateEnrollments()
        {
            try
            {
                // Get all active enrollments
                var enrollments =  _unitOfWork._enrollmentRepository
                    .GetList(e => e.Status == EnrollmentStatus.Active);
                 enrollments.ToList();
               
                if (enrollments == null || !enrollments.Any())
                return;

                foreach (var enrollment in enrollments)
                {
                    if (enrollment.Sessions != null && enrollment.Sessions.Any())
                    {
                        // Check if all sessions are in Completed, Paid, or Cancelled
                        bool allFinished = enrollment.Sessions.Any(s =>
                            s.Status == SessionStatus.Scheduled);

                        if (!allFinished)
                        {
                            enrollment.Status = EnrollmentStatus.Completed;
                        }
                    }
                }

                await _unitOfWork._enrollmentRepository.UpdateRangeAsync(enrollments);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating enrollments: " + ex.Message);
            }
        }

        //public async Task<ServiceResult<PaginationVM<EnrollmentListVM>>> SearchEnrollmentsByDate(DateTime date)
        //{
        //    try
        //    {
        //        var enrollments = await _unitOfWork._enrollmentRepository.SearchEnrollmentList(date);

        //        return ServiceResult<PaginationVM<EnrollmentListVM>>.SuccessResult(enrollments, "Enrollments retrieved successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it as needed
        //        return ServiceResult<PaginationVM<EnrollmentListVM>>.FailureResult(ex.Message);
        //    }
        //}


    }
}
