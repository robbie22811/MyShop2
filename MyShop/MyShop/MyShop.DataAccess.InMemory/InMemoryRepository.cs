﻿using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T toUpdate = items.Find(i => i.Id == t.Id);
            if (toUpdate != null)
                toUpdate = t;
            else
                throw new Exception(className + " Not Found");

        }

        public T Find(string id)
        {
            T t = items.Find(i => i.Id == id);
            if (t != null)
                return t;
            else
                throw new Exception(className + " Not Found");

        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string id)
        {
            T toDelete = items.Find(i => i.Id == id);
            if (toDelete != null)
                items.Remove(toDelete);
            else
                throw new Exception(className + " Not Found");

        }
    }
}
