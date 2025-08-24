
using Microsoft.AspNetCore.Mvc;
using QABS.Models;
using QABS.Repository;
using QABS.ViewModels;
using System.Net;
using Utilities;

namespace QABS.Service
{
    public class TeacherService
    {

        private readonly UnitOfWork _unitOfWork;

        public TeacherService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task<ServiceResult<PaginationVM<TeacherDetailsVM>>> GetAllTeachers()
        {
            try
            {
                var teachers = await _unitOfWork._teacherRepository.SearchTeachers();
                return ServiceResult<PaginationVM<TeacherDetailsVM>>.SuccessResult(teachers, "Teachers retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<PaginationVM<TeacherDetailsVM>>.FailureResult(ex.Message);
            }
        }
        public async Task<ServiceResult<PaginationVM<TeacherDetailsVM>>> GetTeachersByName(string name)
        {
            try
            {
                var teachers = await _unitOfWork._teacherRepository.SearchTeachers(name);
                return ServiceResult<PaginationVM<TeacherDetailsVM>>.SuccessResult(teachers, "Teachers retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<PaginationVM<TeacherDetailsVM>>.FailureResult(ex.Message);
            }
        }

        public async Task<ServiceResult<List<SessionDetailsVM>>> GetSessionsByTeacherPayoutId(int teacherPayoutId)
        {
            try
            {
                var sessions = await _unitOfWork._sessionRepository.GetSessionsByTeacherPayoutIdAsync(teacherPayoutId);
                return ServiceResult<List<SessionDetailsVM>>.SuccessResult(sessions, "Sessions retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<List<SessionDetailsVM>>.FailureResult(ex.Message);
            }
        }

        public async Task<ServiceResult<PaginationVM<StudentDetailsVM>>> GetEnrolledStudentsById(string id )
        {
            try
            {
                var students = await _unitOfWork._enrollmentRepository.GetEnrolledStudentsByTeacherIdAsync(id);
                return ServiceResult<PaginationVM<StudentDetailsVM>>.SuccessResult(students, "Students retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<PaginationVM<StudentDetailsVM>>.FailureResult(ex.Message);
            }
        }

        public async Task<ServiceResult> PayTeacherByCompletedSessions(TeacherPayoutCreateVM vm)
        {
            try
            {
                var completedSessions = await _unitOfWork._sessionRepository.GetCompletedSessionsByTeacherId(vm.TeacherId);

                if (completedSessions == null || !completedSessions.Any())
                {
                    return ServiceResult.FailureResult("No completed sessions found for the teacher.");
                }
                vm.TotalAmount = completedSessions.Sum(s => s.Amount);
                vm.TotalHours = completedSessions.Sum(s => (decimal)((int)s.Enrollment.SubscriptionPlan.Duration / 60));

                vm.ImageUrl = UploadMedia.addimage(vm.ImageFile);

                var teacherPayout = vm.ToCreate();
                await _unitOfWork._teacherPayoutRepositroy.AddAsync(teacherPayout);
                await _unitOfWork.SaveChangesAsync();

                //Like ForEach Loop
                var updatedSessions = completedSessions
                    .Select(session =>
                    {
                        session.TeacherPayoutId = teacherPayout.Id;
                        session.Status = SessionStatus.Paid;
                        return session;
                    }).ToList();

                // Update all sessions in bulk if your repository supports it
                await _unitOfWork._sessionRepository.UpdateRangeAsync(updatedSessions);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.SuccessResult("Teacher payout processed successfully.", HttpStatusCode.Created);

            }
            catch
            {
                // Log the exception or handle it as needed
                return ServiceResult.FailureResult("An error occurred while processing the payment.");
            }
        }
        public async Task<ServiceResult<PaginationVM<TeacherPayoutDetailsVM>>> GetPayoutByMonthAsync(DateTime month, int pagesize = 10, int pageindex = 1)
        {
            try
            {
                var payouts = _unitOfWork._teacherPayoutRepositroy.GetPayoutByMonthAsync(month, pagesize, pageindex);
                return ServiceResult<PaginationVM<TeacherPayoutDetailsVM>>.SuccessResult(payouts.Result, "Teacher payouts retrieved successfully.");
            }
            catch
            {
                return ServiceResult<PaginationVM<TeacherPayoutDetailsVM>>.FailureResult("Teacher payouts Failed to retrieve.");
            }
        }

        public async Task<ServiceResult> CreateTeacherAvaliability([FromBody] List<TeacherAvailabilityCreateVM> vm)
        {
            try
            {
                if (vm == null || !vm.Any())
                {
                    return ServiceResult.FailureResult("Invalid avaliability data.");
                }

                // Calculate amount for each session and map to entity
                var createdAvaliabilities = vm.Select(vm =>
                {
                    return vm.ToCreate();
                }).ToList();

                await _unitOfWork._teacherAvailabilityRepository.AddRangeAsync(createdAvaliabilities);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.SuccessResult($"{createdAvaliabilities.Count} avaliabilites created successfully.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return ServiceResult.FailureResult(ex.Message);
            }
        }

        public async Task<ServiceResult> UpdateTeacherAvaliability([FromBody] TeacherAvailabilityEditVM vm)
        {
            try
            {
                if (vm == null )
                {
                    return ServiceResult.FailureResult("Invalid avaliability data.");
                }

                var old = await _unitOfWork._teacherAvailabilityRepository.GetByIdAsync(vm.Id);
                if (old == null)
                {
                    return ServiceResult.FailureResult("Avaliability not found.");
                }


                await _unitOfWork._teacherAvailabilityRepository.UpdateAsync(vm.ToEdit(old));
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.SuccessResult("Avaliability updated successfully.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ServiceResult.FailureResult(ex.Message);
            }
        }





        //public async Task<ServiceResult<PaginationVM<StudentPaymentDetailsVM>>> GetAllStudentPaymentsByStudentId(string id)
        //{
        //    try
        //    {
        //        var studentPayments = await _unitOfWork._studentPaymentRepository.GetPaymentsByStudentIdAsync(id);
        //        return ServiceResult<PaginationVM<StudentPaymentDetailsVM>>.SuccessResult(studentPayments, "Student payments retrieved successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it as needed
        //        return ServiceResult<PaginationVM<StudentPaymentDetailsVM>>.FailureResult(ex.Message);
        //    }
        //}
    }
}
