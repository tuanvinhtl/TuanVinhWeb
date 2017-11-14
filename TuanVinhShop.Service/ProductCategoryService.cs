using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TuanVinhShop.Model.Models;

namespace TuanVinhShop.Service
{
    public interface IProductCategoryService
    {
        IEnumerable<ProductCategory> GetAll();
        IEnumerable<ProductCategory> GetAll(string keyWord);
        ProductCategory Delete(int id);
        ProductCategory Add(ProductCategory product);
        void Update(ProductCategory product);
        ProductCategory GetById(int id);
        void SaveChange();
    }
    public class ProductCategoryService : IProductCategoryService
    {
        IProductCategoryRepository _productCategoryRepository;
        IUnitOfWork _unitOfWork;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._productCategoryRepository = productCategoryRepository;
            this._unitOfWork = unitOfWork;
        }
        public ProductCategory Add(ProductCategory product)
        {
            return _productCategoryRepository.Add(product);
        }

        public ProductCategory Delete(int id)
        {
            return _productCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _productCategoryRepository.GetAll();

        }

        public IEnumerable<ProductCategory> GetAll(string keyWord)
        {
            return _productCategoryRepository.GetMulti(x => x.Name.Contains(keyWord) || x.Descryption.Contains(keyWord));
        }

        public ProductCategory GetById(int id)
        {
            return _productCategoryRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategory product)
        {
            _productCategoryRepository.Update(product);
        }
    }
}
