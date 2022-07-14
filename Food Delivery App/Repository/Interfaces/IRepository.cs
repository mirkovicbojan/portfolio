using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Delivery_App.Repository.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetById(Guid? id);

        T Save(T obj);

        T Edit(T obj);

        void Delete(T obj);
    }
}