namespace IMMRequest.DataAccess.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using IMMRequest.Domain;

    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        T Get(int id);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        void Remove(T entity);
        void Update(T entity);
        bool Exists(Expression<Func<T, bool>> predicate);
    }
}
