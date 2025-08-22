

using QABS.Repository;

namespace QABS.Service
{
    public class AccountService
    {
        private readonly UnitOfWork _unitOfWork;

        public AccountService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



    }
}
