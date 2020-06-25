namespace IMMRequest.DataAccess.Core
{
    using System;
    using System.Linq;
    using Domain;
    using Domain.Fields;
    using Domain.States;
    using Microsoft.EntityFrameworkCore;
    using Type = Domain.Type;
    public class IMMRequestContext : DbContext
    {
        public IMMRequestContext(DbContextOptions<IMMRequestContext> options) : base(options)
        {
        }

        public IMMRequestContext()
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

            // Area Name needs to be unique
            builder.Entity<Area>().HasKey(area => area.Id);

            // Entities that need to be stored in the database
            builder.Entity<IntegerField>();
            builder.Entity<TextField>();
            builder.Entity<DateField>();
            builder.Entity<BooleanField>();

            builder.Entity<IntRequestField>()
                .Property(field => field.Values)
                .HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToList());

            builder.Entity<TextRequestField>()
                .Property(field => field.Values)
                .HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList());

            builder.Entity<DateRequestField>()
                .Property(field => field.Values)
                .HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(DateTime.Parse)
                        .ToList());

            builder.Entity<BooleanRequestField>()
                .Property(field => field.Values)
                .HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(bool.Parse)
                        .ToList());

            // State
            builder.Entity<AcceptedState>();
            builder.Entity<CreatedState>();
            builder.Entity<DeniedState>();
            builder.Entity<DoneState>();
            builder.Entity<InReviewState>();

            // Nicer Names for range items
            builder.Entity<IntegerItem>().ToTable("IntegerRangeItems");
            builder.Entity<DateItem>().ToTable("DateRangeItems");
            builder.Entity<TextItem>().ToTable("TextRangeItems");
        }

        // Users
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Citizen> Citizens { get; set; }

        // Core
        public DbSet<Request> Requests { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Type> Types { get; set; }
    }
}
