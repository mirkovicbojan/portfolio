using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Receipt_Micro_Service.DataAccess;
using Receipt_Micro_Service.Repisotory.Interfaces;

namespace Receipt_Micro_Service.Repisotory
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ReceiptDbContext _context;

        public Repository(ReceiptDbContext context)
        {
            _context = context;
        }

        public T Save(T obj)
        {
            _context.Set<T>().Add(obj);
            _context.SaveChanges();
            return obj;
        }
    }
}