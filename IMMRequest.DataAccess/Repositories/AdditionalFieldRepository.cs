using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using IMMRequest.Domain.Fields;
using Microsoft.EntityFrameworkCore;

namespace IMMRequest.DataAccess.Repositories
{
    public class AdditionalFieldRepository
    {
        public AdditionalFieldRepository(DbContext context)
        {
            Context = context;
        }

        protected DbContext Context { get; set; }

        public virtual void Add(AdditionalField additionalField)
        {
            Context.Set<AdditionalField>().Add(additionalField);
            Context.SaveChanges();
        }

        public AdditionalField Get(int id)
        {
            return Context.Set<AdditionalField>().Find(id);
        }

        public void Update(AdditionalField additionalField)
        {
            Context.Entry(additionalField).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Remove(AdditionalField additionalField)
        {
            switch (additionalField.FieldType)
            {
                case FieldType.Date:
                    Context.Set<DateField>().Include(x => x.Range);
                    Context.Set<DateField>().Remove((DateField) additionalField);
                    break;
                case FieldType.Integer:
                    Context.Set<IntegerField>().Include(x => x.Range);
                    Context.Set<IntegerField>().Remove((IntegerField) additionalField);
                    break;
                case FieldType.Text:
                    Context.Set<TextField>().Include(x => x.Range);
                    Context.Set<TextField>().Remove((TextField) additionalField); 
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Context.SaveChanges();
        }
    }
}
