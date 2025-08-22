
using Microsoft.AspNetCore.Mvc;
using QABS.Repository;
using QABS.ViewModels;
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
                var enrollment = _unitOfWork._enrollmentRepository.AddAsync(vm.ToCreate());
                await _unitOfWork.SaveChangesAsync();

                if (vm.studentPayment == null)
                {
                    return ServiceResult.FailureResult("Failed to create enrollment.");
                }

                vm.studentPayment.EnrollmentId = enrollment.Id;
                await _unitOfWork._studentPaymentRepository.UpdateAsync(vm.studentPayment.ToCreate());



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

        public async Task<ServiceResult<PaginationVM<EnrollmentDetailsVM>>> GetAllEnrollments(DateTime date)
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
