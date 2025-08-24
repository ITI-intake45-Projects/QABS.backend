
using Microsoft.AspNetCore.Mvc;
using QABS.Repository;
using QABS.ViewModels;
using System.Data.Entity;
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
            try
            {
                if (vm == null)
                {
                    return ServiceResult.FailureResult("Invalid enrollment data.");
                }

                if (vm.studentPayment == null)
                {
                    return ServiceResult.FailureResult("Failed to create enrollment.");
                }

                //var subscriptionPlan = await _unitOfWork._subscribtionPlanRepository.GetByIdAsync(vm.SubscriptionPlanId);

                //if (subscriptionPlan == null)
                //{
                //    return ServiceResult.FailureResult("Subscription plan not found.");
                //}

                //if (subscriptionPlan.MonthlyFee != vm.studentPayment.Amount)
                //{
                //    return ServiceResult.FailureResult("The amount does not match the subscription plan fee.");
                //}

                var enrollment = vm.ToCreate();
                await _unitOfWork._enrollmentRepository.AddAsync(enrollment);
                await _unitOfWork.SaveChangesAsync();


                
                vm.studentPayment.ImageUrl = UploadMedia.addimage(vm.studentPayment.ImageFile);

                vm.studentPayment.EnrollmentId = enrollment.Id;
                await _unitOfWork._studentPaymentRepository.UpdateAsync(vm.studentPayment.ToCreate());
                await _unitOfWork.SaveChangesAsync();



                return ServiceResult.SuccessResult("Enrollment and Student Payment created successfully.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult.FailureResult(ex.Message);
            }


        }

        public async Task<ServiceResult<PaginationVM<EnrollmentDetailsVM>>> GetAllEnrollments()
        {
            try
            {
                var enrollments = await _unitOfWork._enrollmentRepository.SearchEnrollmentsByDate();

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
                var enrollment = await _unitOfWork._enrollmentRepository.GetList(e => e.Id == id).Select(e => e.ToDetails()).FirstOrDefaultAsync();

                if (enrollment == null)
                {
                    return ServiceResult<EnrollmentDetailsVM>.FailureResult("Enrollment not found.");
                }
                return ServiceResult<EnrollmentDetailsVM>.SuccessResult(enrollment, "Enrollment retrieved successfully.");
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

        public async Task<ServiceResult<PaginationVM<EnrollmentDetailsVM>>> SearchEnrollmentsByDate(DateTime date)
        {
            try
            {
                var enrollments = await _unitOfWork._enrollmentRepository.SearchEnrollmentsByDate(date);

                return ServiceResult<PaginationVM<EnrollmentDetailsVM>>.SuccessResult(enrollments, "Enrollments retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<PaginationVM<EnrollmentDetailsVM>>.FailureResult(ex.Message);
            }
        }


    }
}
