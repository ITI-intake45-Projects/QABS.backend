
using QABS.Repository;
using QABS.ViewModels;
using System.Data.Entity;
using Utilities;
using QABS.Models;

namespace QABS.Service
{
    public class StudentService
    {
        private readonly UnitOfWork _unitOfWork;

        public StudentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<PaginationVM<StudentListMoreInfoVM>>> StudentsSearch
            (
            string name = "",
            bool descending = false,
            int pageSize = 10,
            int pageIndex = 1

            )
        {
            try
            {
                var students = await _unitOfWork._studentRepository.SearchStudents(
                    name, 
                    descending, 
                    pageSize,
                    pageIndex
                    );
                return ServiceResult<PaginationVM<StudentListMoreInfoVM>>.SuccessResult(students, "Students retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<PaginationVM<StudentListMoreInfoVM>>.FailureResult(ex.Message);
            }
        }



        public async Task<ServiceResult<List<StudentDetailsVM>>> GetAllStudents()
        {
            try
            {
                //var students = await _unitOfWork._studentRepository.GetList().Select(s => s.ToDetails()).ToListAsync();
                var students = await _unitOfWork._studentRepository.GetAllAsync();
                var result = students.Select(s => s.ToDetails()).ToList();

                if (result != null)
                {
                    return ServiceResult<List<StudentDetailsVM>>.SuccessResult(result, "Students retrieved successfully.");

                }
                return ServiceResult<List<StudentDetailsVM>>.FailureResult("Students not found.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<List<StudentDetailsVM>>.FailureResult(ex.Message);
            }
        }

        public async Task<ServiceResult<List<StudentListVM>>> GetAllStudentList()
        {
            try
            {
                //var students = await _unitOfWork._studentRepository.GetList().Select(s => s.ToDetails()).ToListAsync();
                var students = await _unitOfWork._studentRepository.GetAllAsync();
                var result = students.Select(s => s.ToList()).ToList();

                if (result != null)
                {
                    return ServiceResult<List<StudentListVM>>.SuccessResult(result, "Students retrieved successfully.");

                }
                return ServiceResult<List<StudentListVM>>.FailureResult("Students not found.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<List<StudentListVM>>.FailureResult(ex.Message);
            }
        }

        public async Task<ServiceResult<PaginationVM<StudentListMoreInfoVM>>> GetStudentsByName(string name)
        {
            try
            {
                var students = await _unitOfWork._studentRepository.SearchStudents(name);
                return ServiceResult<PaginationVM<StudentListMoreInfoVM>>.SuccessResult(students, "Students retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<PaginationVM<StudentListMoreInfoVM>>.FailureResult(ex.Message);
            }
        }


        public async Task<ServiceResult<StudentDetailsVM>> GetStudentById(string id)
        {
            try
            {
                var student = await _unitOfWork._studentRepository.GetByIdAsync(id);
                
                if (student != null)
                {
                    
                    return ServiceResult<StudentDetailsVM>.SuccessResult(student.ToDetails(), "Student retrieved successfully.");

                }
                return ServiceResult<StudentDetailsVM>.FailureResult("Student not found.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<StudentDetailsVM>.FailureResult(ex.Message);
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

        public async Task<ServiceResult> EditStudentPaymentStatus(int id)
        {
            try
            {
                var studentPayment = await _unitOfWork._studentPaymentRepository.GetByIdAsync(id);

                if (studentPayment == null)
                {
                    return ServiceResult.FailureResult("Student payment not found.");
                }
                studentPayment.Status = StudentPaymentStatus.Recieved;


                await _unitOfWork._studentPaymentRepository.UpdateAsync(studentPayment);
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
