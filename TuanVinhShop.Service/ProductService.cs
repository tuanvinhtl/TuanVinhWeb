using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TuanVinhShop.Model.Models;

namespace TuanVinhShop.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetPagin(string keyWord,int page,int pageSize,out int totalRow);
        Product Delete(int id);
        Product Add(Product product);
        void Update(Product product);
        Product GetById(int id);
        void SaveChange();
    }
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWOrk)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWOrk;
        }
        public Product Add(Product product)
        {
            return _productRepository.Add(product);
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }


        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public IEnumerable<Product> GetPagin(string keyWord, int page, int pageSize, out int totalRow)
        {
            var query = _productRepository.GetAll();
            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(x => x.Name.Contains(keyWord));
            }
            var model = query.OrderByDescending(x=>x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            totalRow = query.Count();
            return model;
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
        }
    }
}
