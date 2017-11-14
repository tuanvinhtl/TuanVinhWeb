using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TuanVinhShop.Model.Models;

namespace TuanVinhShop.Service
{
    public interface IErrorService
    {
        Error Add(Error error);
        Error Delete(int id);
        void Update(Error error);
        void SaveChange();
    }
    public class ErrorService : IErrorService
    {
        public IErrorRepository _errorRepository;
        public IUnitOfWork _unitOfWork;
        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
        {
            this._errorRepository = errorRepository;
            this._unitOfWork = unitOfWork;
        }
        public Error Add(Error error)
        {
            return _errorRepository.Add(error);
        }

        public Error Delete(int id)
        {
            return _errorRepository.Delete(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Error error)
        {
            _errorRepository.Update(error);
        }
    }
}
