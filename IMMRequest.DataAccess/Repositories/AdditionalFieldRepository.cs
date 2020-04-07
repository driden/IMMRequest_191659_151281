using System;
using System.Collections.Generic;
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
    }
}
