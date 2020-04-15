using System.Collections.Generic;

namespace IMMRequest.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Remove(T entity);
        void Update(T entity);
    }
}
