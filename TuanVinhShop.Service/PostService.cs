using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TuanVinhShop.Model.Models;

namespace TuanVinhShop.Service
{
    public interface IPostService
    {
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetAll(string keyWord);
        Post Delete(int id);
        Post Add(Post post);
        void Update(Post post);
        Post GetById(int id);
        void SaveChange();
    }
    public class PostService : IPostService
    {
        IPostRepository _postRepository;
        IUnitOfWork _unitOfWork;
        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            this._postRepository = postRepository;
            this._unitOfWork = unitOfWork;
        }
        public Post Add(Post post)
        {
           return _postRepository.Add(post);
        }

        public Post Delete(int id)
        {
            return _postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return  _postRepository.GetAll();
        }

        public IEnumerable<Post> GetAll(string keyWord)
        {
            return _postRepository.GetMulti(x => x.Name.Contains(keyWord));
        }

        public Post GetById(int id)
        {
            return _postRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
        }
    }
}
