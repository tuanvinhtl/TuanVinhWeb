using AutoMapper;
using TuanVinhShop.Model.Models;
using TuanVinhShop.Web.Models;

namespace TuanVinhShop.Web.Mappings
{
    public static class ConfigAutoMapper
    {
        public static void ConfigMap()
        {
            Mapper.CreateMap<Product, ProductViewModel>();
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<ProductCategory, ProductCategoryViewModel>();
            Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            Mapper.CreateMap<Tag, TagViewModel>();
            Mapper.CreateMap<ProductTag, ProductTagViewModel>();
            Mapper.CreateMap<Order, OrderViewModel>();
            Mapper.CreateMap<OrderDetail, OrderDetailViewModel>();
            Mapper.CreateMap<PostTag, PostTagViewModel>();
        }
    }
}