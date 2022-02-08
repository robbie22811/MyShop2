﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        public ProductManagerController()
        {
            context = new ProductRepository();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string id)
        {
            Product product = context.Find(id);
            if(product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, string id)
        {
            Product productToEdit = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            productToEdit.Category = product.Category;
            productToEdit.Description = product.Description;
            productToEdit.Image = product.Image;
            productToEdit.Name = product.Name;
            productToEdit.Price = product.Price;

            context.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            Product productToDel = context.Find(id);
            if (productToDel == null)
            {
                return HttpNotFound();
            }

            return View(productToDel);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            Product productToDel = context.Find(id);

            if (productToDel == null)
            {
                return HttpNotFound();
            }

            context.Delete(id);
            context.Commit();
            return RedirectToAction("Index");
        }
    }
}