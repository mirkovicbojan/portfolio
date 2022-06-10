using TimeSheet.Contexts;
using TimeSheet.Repository.Interfaces;

namespace TimeSheet.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TimeSheetContext context;

        public Repository(TimeSheetContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> GetAll()
        {

            return context.Set<T>().ToList();
        }

        public T GetById(int? id)
        {
            context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
            T item = context.Set<T>().Find(id)

;
            return item ?? null;
        }

        public T Save(T obj)
        {
            context.Set<T>().Add(obj);
            context.SaveChanges();
            return obj;
        }

        public T Edit(T obj)
        {
            context.Set<T>().Update(obj);
            context.SaveChanges();
            return obj;
        }

        public void Delete(T obj)
        {
            context.Set<T>().Remove(obj);
            context.SaveChanges();
        }
    }
}