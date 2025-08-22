

using QABS.Models;

namespace QABS.ViewModels
{
    public static class StudentPaymentExt
    {
        public static StudentPayment ToCreate(this StudentPaymentCreateVM create)
        {

            return new StudentPayment
            {
                Amount = create.Amount,
                PaymentDate = create.PaymentDate,
                ImageUrl = create.ImageUrl,
                StudentId = create.StudentId,
                EnrollmentId = create.EnrollmentId,

            };
        }


        public static StudentPaymentDetailsVM ToDetails(this StudentPayment studentPayment)
        {
           
            return new StudentPaymentDetailsVM
            {
                Id = studentPayment.Id,
                Amount = studentPayment.Amount,
                PaymentDate = studentPayment.PaymentDate,
                ImageUrl = studentPayment.ImageUrl,
                StudentName = studentPayment.Student?.User.FirstName + " " + studentPayment.Student?.User.LastName, // Assuming Student has a Name property
                EnrollmentDetailsVM = studentPayment.Enrollment?.ToDetails() // Assuming Enrollment has a ToDetailsVM method
            };
        }


     
    }
}
