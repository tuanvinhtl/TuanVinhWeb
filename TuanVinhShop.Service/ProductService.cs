using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TuanVinhShop.Model.Models;

namespace TuanVinhShop.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAll(string keyWord);
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

        public IEnumerable<Product> GetAll(string keyWord)
        {
            return _productRepository.GetMulti(x => x.Name.Contains(keyWord) || x.Descryption.Contains(keyWord));
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
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
