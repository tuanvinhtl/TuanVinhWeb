namespace TuanVinhShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TeduShop.Data;
    using TuanVinhShop.Model.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TeduShop.Data.TuanVinhShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TeduShop.Data.TuanVinhShopDbContext context)
        {
            AddProductCategory(context);
            AddProduct(context);
            CreateUser(context);
        }
        private void AddProductCategory(TeduShop.Data.TuanVinhShopDbContext context)
        {
            if (context.ProductCategories.Count()==0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
                {
                    new ProductCategory()
                    {
                        Name="Category 1",Alias="Category-1",Status=true
                    },
                    new ProductCategory()
                    {
                        Name="Category 2",Alias="Category-2",Status=true
                    },
                    new ProductCategory()
                    {
                        Name="Category 3",Alias="Category-3",Status=true
                    },
                    new ProductCategory()
                    {
                        Name="Category 4",Alias="Category-4",Status=false
                    },
                    new ProductCategory()
                    {
                        Name="Category 5",Alias="Category-5",Status=false
                    }
                };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }
        private void AddProduct(TeduShop.Data.TuanVinhShopDbContext context) {
            if (context.Products.Count()==0)
            {
                List<Product> listProduct = new List<Product>()
                {
                    new Product ()
                    {
                        Name="Test Product 1",Alias="Test-Product-1",CategoryID=1,Price=100,Quantity=10,Status=true
                    },
                    new Product ()
                    {
                        Name="Test Product 2",Alias="Test-Product-2",CategoryID=1,Price=200,Quantity=20,Status=true
                    },
                    new Product ()
                    {
                        Name="Test Product 3",Alias="Test-Product-3",CategoryID=3,Price=300,Quantity=30,Status=true
                    }
                };
                context.Products.AddRange(listProduct);
                context.SaveChanges();
            }
        }
        private void CreateUser(TuanVinhShopDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TuanVinhShopDbContext()));
            if (manager.Users.Count() == 0)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TuanVinhShopDbContext()));

                var user = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "tuanvinhtl@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    FullName = "Nguyen Tuan Vinh",
                    Avatar = "/assets/images/img.jpg",
                };
                if (manager.Users.Count(x => x.UserName == "admin") == 0)
                {
                    manager.Create(user, "123456$");

                    if (!roleManager.Roles.Any())
                    {
                        roleManager.Create(new IdentityRole { Name = "Admin"});
                        roleManager.Create(new IdentityRole { Name = "Member"});
                    }

                    var adminUser = manager.FindByName("admin");

                    manager.AddToRoles(adminUser.Id, new string[] { "Admin", "Member" });
                }
            }
        }
    }
}
