using System.Collections.Generic;
using System.Linq;
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
        IEnumerable<ProductCategory> GetPagin(string keyWord, int page, int pageSize, out int totalRow);
       
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
        public IEnumerable<ProductCategory> GetPagin(string keyWord, int page, int pageSize, out int totalRow)
        {
            var query = _productCategoryRepository.GetAll();
            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(x => x.Name.Contains(keyWord));
            }
            var model = query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            totalRow = query.Count();
            return model;
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
