using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Delivery_App.Contexts;
using Food_Delivery_App.Repository.Interfaces;

namespace Food_Delivery_App.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FoodAppContext _context;
        public Repository(FoodAppContext context)
        {
            this._context = context;
        }

        public IEnumerable<T> GetAll()
        {

            return _context.Set<T>().ToList();
        }

        public T GetById(Guid? id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
            T item = _context.Set<T>().Find(id)

;
            return item ?? null;
        }


        public T Save(T obj)
        {
            _context.Set<T>().Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public T Edit(T obj)
        {
            _context.Set<T>().Update(obj);
            _context.SaveChanges();
            return obj;
        }

        public void Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
            _context.SaveChanges();
        }
    }
}