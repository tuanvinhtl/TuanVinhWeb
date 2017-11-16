using System;
using TuanVinhShop.Model.Models;
using TuanVinhShop.Web.Models;

namespace TuanVinhShop.Web.Inflastructure.Extensions
{
    public static class EntityExtension
    {
        public static void UpdateProduct(this Product product, ProductViewModel productViewModel)
        {
            product.ID = productViewModel.ID;
            product.Name = productViewModel.Name;
            product.Alias = productViewModel.Alias;
            product.CategoryID = productViewModel.CategoryID;
            product.Images = productViewModel.Images;
            product.MoreImages = productViewModel.MoreImages;
            product.Price = productViewModel.Price;
            product.OriginalPrice = productViewModel.OriginalPrice;
            product.Quantity = productViewModel.Quantity;
            product.Promotion = productViewModel.Promotion;
            product.Waranty = productViewModel.Waranty;
            product.Content = productViewModel.Content;
            product.HomeFlag = productViewModel.HomeFlag;
            product.TopHot = productViewModel.TopHot;
            product.Tags = productViewModel.Tags;
            product.ViewCount = productViewModel.ViewCount;
            product.CreatedDate = productViewModel.CreatedDate;
            product.CreatedBy = productViewModel.CreatedBy;
            product.UpdateDate = productViewModel.UpdateDate;
            product.MetaKeywork = productViewModel.MetaKeywork;
            product.Descryption = productViewModel.Descryption;
            product.DisplayOrder = productViewModel.DisplayOrder;
            product.Status = productViewModel.Status;
        }
        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryViewModel)
        {
            productCategory.ID = productCategoryViewModel.ID;
            productCategory.Name = productCategoryViewModel.Name;
            productCategory.Alias = productCategoryViewModel.Alias;
            productCategory.ParentID = productCategoryViewModel.ParentID;
            productCategory.Images = productCategoryViewModel.Images;
            productCategory.HomeFlag = productCategoryViewModel.HomeFlag;
            productCategory.CreatedDate = productCategoryViewModel.CreatedDate;
            productCategory.CreatedBy = productCategoryViewModel.CreatedBy;
            productCategory.UpdateDate = productCategoryViewModel.UpdateDate;
            productCategory.UpdateBy = productCategoryViewModel.UpdateBy;
            productCategory.MetaKeywork = productCategoryViewModel.MetaKeywork;
            productCategory.Descryption = productCategoryViewModel.Descryption;
            productCategory.DisplayOrder = productCategoryViewModel.DisplayOrder;
            productCategory.Status = productCategoryViewModel.Status;
        }
        public static void UpdateOrder(this Order order, OrderViewModel orderViewModel)
        {
            order.ID = orderViewModel.ID;
            order.CustumerName = orderViewModel.CustumerName;
            order.CustummerEmail = orderViewModel.CustummerEmail;
            order.CustummerAddress = orderViewModel.CustummerAddress;
            order.CustummerMessage = orderViewModel.CustummerMessage;
            order.Status = false;
            order.CustummerMobile = orderViewModel.CustummerMobile;
            order.PaymentStatus = orderViewModel.PaymentStatus;
            order.PaymentMethod = orderViewModel.PaymentMethod;
            order.CreateBy = orderViewModel.CreateBy;
            order.CreatedDate = DateTime.Now;
        }
    }
}