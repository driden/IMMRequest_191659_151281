using IMMRequest.Domain;
using IMMRequest.Domain.Fields;
using Microsoft.EntityFrameworkCore;

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
            builder.Entity<Field>().Property(p => p.Id).ValueGeneratedOnAdd();

            // Request can only have a max length of 2000
            builder.Entity<Request>().Property(req => req.Details).HasMaxLength(2000);

            // User email needs to be unique
            builder.Entity<User>().HasAlternateKey(user => user.Email);


            
        }

        // Users
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Citizen> Citizens { get; set; }

        // Fields
        public DbSet<IntegerField> IntegerFields{ get; set; }
        public DbSet<DateField> DateFields { get; set; }
        public DbSet<TextField> TextFields { get; set; }

        // Core
        public DbSet<Area> RequestAreas { get; set; }
        public DbSet<Topic> RequestTopics { get; set; }
        public DbSet<Type> RequestTypes { get; set; }

    }
}
