using IMMRequest.Domain;
using IMMRequest.Domain.Fields;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMMRequest.DataAccess
{
    public class IMMRequestContext : DbContext
    {
        public IMMRequestContext(DbContextOptions<IMMRequestContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<DateField>().HasMany(dateField => dateField.Range);
        }

        // Users
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Citizen> Citizens { get; set; }

        // Fields
        public DbSet<IntegerField> IntegerFields{ get; set; }
        public DbSet<DateField> DateFields { get; set; }

        // Core
        public DbSet<RequestArea> RequestAreas { get; set; }
        public DbSet<RequestTopic> RequestTopics { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }

    }
}
