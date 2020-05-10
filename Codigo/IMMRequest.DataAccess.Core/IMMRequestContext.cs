namespace IMMRequest.DataAccess.Core
{
    using Domain;
    using Domain.Fields;
    using Domain.States;
    using Microsoft.EntityFrameworkCore;

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

            // User email needs to be unique
            //builder.Entity<User>().HasAlternateKey(user => user.Email);

            // Area Name needs to be unique
            builder.Entity<Area>().HasKey(area => area.Id);

            // Entities that need to be stored in the database
            builder.Entity<IntegerField>();
            builder.Entity<TextField>();
            builder.Entity<DateField>();

            builder.Entity<IntRequestField>();
            builder.Entity<TextRequestField>();
            builder.Entity<DateRequestField>();

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
