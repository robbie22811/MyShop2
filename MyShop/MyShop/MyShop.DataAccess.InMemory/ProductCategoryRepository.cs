﻿using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        public void Update(ProductCategory product)
        {
            ProductCategory productToUpdate = productCategories.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
                productToUpdate = product;
            else
            {
                throw new Exception("Product Category Not Found");
            }
        }

        public ProductCategory Find(string id)
        {
            ProductCategory product = productCategories.Find(p => p.Id == id);

            if (product != null)
                return product;
            else
            {
                throw new Exception("Product Category Not Found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string id)
        {
            ProductCategory productToDelete = productCategories.Find(p => p.Id == id);

            if (productToDelete != null)
                productCategories.Remove(productToDelete);
            else
            {
                throw new Exception("Product Category Not Found");
            }
        }

    }
}
