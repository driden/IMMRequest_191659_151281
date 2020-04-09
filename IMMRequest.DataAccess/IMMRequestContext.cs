using IMMRequest.Domain;
using IMMRequest.Domain.Fields;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IMMRequest.DataAccess
{
    public class IMMRequestContext : DbContext
    {
        public IMMRequestContext(DbContextOptions<IMMRequestContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Auto generating Ids
            builder.Entity<Request>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Area>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Topic>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Type>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<AdditionalField>().Property(p => p.Id).ValueGeneratedOnAdd();
            
            // Request can only have a max length of 2000
            builder.Entity<Request>().Property(req => req.Details).HasMaxLength(2000);

            // User email needs to be unique
            builder.Entity<User>().HasAlternateKey(user => user.Email);

            // Area Name needs to be unique
            builder.Entity<Area>().HasAlternateKey(area => area.Name);

            builder.Entity<IntegerField>();
            builder.Entity<TextField>();
            builder.Entity<DateField>();

        }

        // Users
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Citizen> Citizens { get; set; }

        // Fields
        //public DbSet<IntegerField> IntegerFields { get; set; }
        //public DbSet<DateField> DateFields { get; set; }
        //public DbSet<TextField> TextFields { get; set; }

        // Core
        public DbSet<Request> Requests { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Type> Types { get; set; }


    }
}
