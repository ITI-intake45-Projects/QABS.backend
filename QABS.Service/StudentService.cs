
using QABS.Repository;
using QABS.ViewModels;
using Utilities;

namespace QABS.Service
{
    public class StudentService
    {
        private readonly UnitOfWork _unitOfWork;

        public StudentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<PaginationVM<StudentDetailsVM>>> GetAllStudents()
        {
            try
            {
                var students = await _unitOfWork._studentRepository.SearchStudents();
                return ServiceResult<PaginationVM<StudentDetailsVM>>.SuccessResult(students, "Students retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<PaginationVM<StudentDetailsVM>>.FailureResult(ex.Message);
            }
        }
        public async Task<ServiceResult<PaginationVM<StudentDetailsVM>>> GetStudentsByName(string name)
        {
            try
            {
                var students = await _unitOfWork._studentRepository.SearchStudents(name);
                return ServiceResult<PaginationVM<StudentDetailsVM>>.SuccessResult(students, "Students retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<PaginationVM<StudentDetailsVM>>.FailureResult(ex.Message);
            }
        }


        public async Task<ServiceResult<PaginationVM<StudentPaymentDetailsVM>>> GetAllStudentPaymentsByStudentId(string id)
        {
            try
            {
                var studentPayments = await _unitOfWork._studentPaymentRepository.GetPaymentsByStudentIdAsync(id);
                return ServiceResult<PaginationVM<StudentPaymentDetailsVM>>.SuccessResult(studentPayments, "Student payments retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<PaginationVM<StudentPaymentDetailsVM>>.FailureResult(ex.Message);
            }
        }


        public async Task<ServiceResult<StudentPaymentDetailsVM>> GetStudentPaymentByEnrollmentId(int id)
        {
            try
            {
                var studentPayment = await _unitOfWork._studentPaymentRepository.GetStudentPaymentByEnrollmentId(id);
                if (studentPayment != null)
                {
                    return ServiceResult<StudentPaymentDetailsVM>.SuccessResult(studentPayment, "Student payment retrieved successfully.");
                }
                else
                {
                    return ServiceResult<StudentPaymentDetailsVM>.FailureResult("Student payment not found.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<StudentPaymentDetailsVM>.FailureResult(ex.Message);
            }
        }


        public async Task<ServiceResult> EditStudentPaymentStatus(StudentPaymentEditVM vm)
        {
            try
            {
                var studentPayment = await _unitOfWork._studentPaymentRepository.GetByIdAsync(vm.Id);

                if (studentPayment == null)
                {
                    return ServiceResult.FailureResult("Student payment not found.");
                }

                await _unitOfWork._studentPaymentRepository.UpdateAsync(vm.ToEdit(studentPayment));
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.SuccessResult("Student payment status updated successfully.");
            }
            catch
            {
                return ServiceResult.FailureResult("Failed to edit student payment status.");
            }
        }
    }
}
