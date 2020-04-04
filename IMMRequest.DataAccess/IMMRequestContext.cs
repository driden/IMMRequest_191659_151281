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
