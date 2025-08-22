
using QABS.Repository;
using QABS.ViewModels;
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



        public async Task<ServiceResult<PaginationVM<TeacherDetailsVM>>> GetAllStudents()
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
        public async Task<ServiceResult<PaginationVM<TeacherDetailsVM>>> GetStudentsByName(string name)
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
