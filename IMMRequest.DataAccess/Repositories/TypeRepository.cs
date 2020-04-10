using Microsoft.EntityFrameworkCore;
using Type = IMMRequest.Domain.Type;

namespace IMMRequest.DataAccess.Repositories
{
    public class TypeRepository
    {
        public TypeRepository(DbContext context)
        {
            Context = context;
        }

        protected DbContext Context { get; set; }

        public virtual void Add(Type type)
        {
            Context.Set<Type>().Add(type);
            Context.SaveChanges();
        }

        public Type Get(int id)
        {
            return Context.Set<Type>().Find(id);
        }

        public void Update(Type type)
        {
            Context.Entry(type).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Remove(Type type)
        {
            Context.Remove(type);
            Context.SaveChanges();
        }
    }
}
