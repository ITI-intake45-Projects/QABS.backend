
using Microsoft.AspNetCore.Mvc;
using QABS.Models;
using QABS.Repository;
using QABS.ViewModels;
using System.Data.Entity;
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



        public async Task<ServiceResult<List<TeacherDetailsVM>>> GetAllTeachers()
        {
            try
            {
                var teachers = await _unitOfWork._teacherRepository.GetAllAsync();
                var result = teachers.Select(t => t.ToDetails()).ToList();
                return ServiceResult<List<TeacherDetailsVM>>.SuccessResult(result, "Teachers retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<List<TeacherDetailsVM>>.FailureResult(ex.Message);
            }
        }

        //Teacher List 
        public async Task<ServiceResult<List<TeacherListVM>>> GetAllTeacherList()
        {
            try
            {
                var teachers = await _unitOfWork._teacherRepository.GetAllAsync();
                var result = teachers.Select(t => t.ToList()).ToList();
                return ServiceResult<List<TeacherListVM>>.SuccessResult(result, "Teachers retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<List<TeacherListVM>>.FailureResult(ex.Message);
            }
        }

        //Teacher List More Info
        public async Task<ServiceResult<List<TeacherListMoreInfoVM>>> GetAllTeacherListMore()
        {
            try
            {
                var teachers = await _unitOfWork._teacherRepository.GetAllAsync();
                var result = teachers.Select(t => t.ToListMore()).ToList();
                return ServiceResult<List<TeacherListMoreInfoVM>>.SuccessResult(result, "Teachers retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<List<TeacherListMoreInfoVM>>.FailureResult(ex.Message);
            }
        }

        public async Task<ServiceResult<TeacherDetailsVM>> GetTeacherById(string teacherId)
        {
            try
            {
                var teacher = await _unitOfWork._teacherRepository.GetByIdAsync(teacherId);
                if(teacher == null)
                {
                    return ServiceResult<TeacherDetailsVM>.FailureResult("Teacher not found");
                }
                var restult = teacher.ToDetails();
                return ServiceResult<TeacherDetailsVM>.SuccessResult(restult, "Teacher retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<TeacherDetailsVM>.FailureResult(ex.Message);
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

        public async Task<ServiceResult<List<StudentListVM>>> GetEnrolledStudentsById(string id )
        {
            try
            {
                var students = await _unitOfWork._enrollmentRepository.GetEnrolledStudentsByTeacherIdAsync(id);
                return ServiceResult<List<StudentListVM>>.SuccessResult(students, "Students retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<List<StudentListVM>>.FailureResult(ex.Message);
            }
        }

        public async Task<ServiceResult> CreatePayTeacherByCompletedSessions(TeacherPayoutCreateVM vm)
        {
            try
            {
                var completedSessions = await _unitOfWork._sessionRepository.GetCompletedSessionsByTeacherId(vm.TeacherId);

                if (completedSessions == null || !completedSessions.Any())
                {
                    return ServiceResult.FailureResult("No completed sessions found for the teacher.");
                }
                vm.TotalAmount = completedSessions.Sum(s => s.Amount);
                vm.TotalHours = completedSessions.Sum(s => ((decimal)s.Enrollment.SubscriptionPlan.Duration / 60));

                vm.ImageUrl = await UploadMedia.AddImageAsync(vm.ImageFile);

                var updatedSessions = completedSessions
             .Select(session =>
             {
                 session.Status = SessionStatus.Paid;
                 return session;
             }).ToList();

                vm.sessionDetails = updatedSessions.Select(session => session.ToDetails()).ToList();
                var teacherPayout = vm.ToCreate();
                await _unitOfWork._teacherPayoutRepositroy.AddAsync(teacherPayout);
                await _unitOfWork.SaveChangesAsync();

                //Like ForEach Loop
               



                var updated = updatedSessions
                    .Select(session =>
                    {
                        session.TeacherPayoutId = teacherPayout.Id;
                        
                        return session;
                    }).ToList();
                

                // Update all sessions in bulk if your repository supports it
                await _unitOfWork._sessionRepository.UpdateRangeAsync(updated);
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

        public async Task<ServiceResult> CreateTeacherAvaliability(TeacherAvailabilityCreateVM vm)
        {
            try
            {
                if (vm == null)
                {
                    return ServiceResult.FailureResult("Invalid availability data.");
                }

                // Map VM → Entity
                var entity = vm.ToCreate();

                await _unitOfWork._teacherAvailabilityRepository.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.SuccessResult("Availability created successfully.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return ServiceResult.FailureResult(ex.Message);
            }
        }


        public async Task<ServiceResult> DeleteTeacherAvaliability(int id)
        {
            try
            {

                var TeacherAvailability = await _unitOfWork._teacherAvailabilityRepository.GetByIdAsync(id);
                if (TeacherAvailability == null)
                {
                    return ServiceResult.FailureResult("not found TeacherAvailability.");
                }

                await _unitOfWork._teacherAvailabilityRepository.Delete(TeacherAvailability);

                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.SuccessResult("Availability Deleted Successfully.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ServiceResult.FailureResult(ex.Message);
            }
        }


        public async Task<ServiceResult> UpdateTeacherAvaliability(TeacherAvailabilityEditVM vm)
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

        //public async Task<ServiceResult> DeleteTeacherAsync(string teacherId)
        //{
        //    try
        //    {
        //        var teacher = await _unitOfWork._teacherRepository
        //            .GetByIdAsync(teacherId);

        //        if (teacher == null)
        //            return ServiceResult.FailureResult("Teacher not found");

        //        // 🗑️ امسح الـ Enrollments المرتبطة
        //        if (teacher.Enrollments != null && teacher.Enrollments.Any())
        //        {
        //            foreach (var enrollment in teacher.Enrollments)
        //            {
        //                // ✅ جيب الـ StudentPayment الخاص بالـ Enrollment
        //                var payment = _unitOfWork._studentPaymentRepository
        //                    .GetList(p => p.EnrollmentId == enrollment.Id)
        //                    .FirstOrDefault();

        //                if (payment != null)
        //                {
        //                    await _unitOfWork._studentPaymentRepository.Delete(payment);
        //                }
        //            }

        //            // بعد كده امسح الـ Enrollments نفسها
        //            await _unitOfWork._enrollmentRepository.DeleteRange(teacher.Enrollments);
        //        }

        //        // 🗑️ امسح Teacher نفسه
        //        await _unitOfWork._teacherRepository.Delete(teacher);

        //        await _unitOfWork.SaveChangesAsync();

        //        return ServiceResult.SuccessResult("Teacher deleted successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return ServiceResult.FailureResult(ex.Message);
        //    }
        //}


        //public async Task<ServiceResult> DeleteTeacherAsync(string teacherId)
        //{
        //    try
        //    {
        //        // ✅ هات الـ Teacher مع Enrollments و StudentPayments
        //        var teacher = await _unitOfWork._teacherRepository
        //            .GetList(t => t.UserId == teacherId)
        //            .Include(t => t.Enrollments)
        //                .ThenInclude(e => e.StudentPayments)
        //            .FirstOrDefaultAsync();

        //        if (teacher == null)
        //            return ServiceResult.FailureResult("Teacher not found");

        //        // 🗑️ امسح StudentPayments المرتبطة
        //        if (teacher.Enrollments != null && teacher.Enrollments.Any())
        //        {
        //            foreach (var enrollment in teacher.Enrollments)
        //            {
        //                if (enrollment.StudentPayments != null && enrollment.StudentPayments.Any())
        //                {
        //                    await _unitOfWork._studentPaymentRepository.DeleteRange(enrollment.StudentPayments);
        //                }
        //            }

        //            // 🗑️ امسح Enrollments نفسها
        //            await _unitOfWork._enrollmentRepository.DeleteRange(teacher.Enrollments);
        //        }

        //        // 🗑️ امسح Teacher نفسه
        //        await _unitOfWork._teacherRepository.Delete(teacher);

        //        await _unitOfWork.SaveChangesAsync();

        //        return ServiceResult.SuccessResult("Teacher deleted successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return ServiceResult.FailureResult(ex.Message);
        //    }
        //}










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
