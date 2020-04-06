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
            base.OnModelCreating(builder);

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

            builder.Entity<AdditionalField>().ToTable("AdditionalFields");

        }

        protected void Seed(ModelBuilder builder)
        {
            // Seed Data
            builder.Entity<IntegerField>().HasData(this.IntegerFields);
            builder.Entity<DateField>().HasData(this.DateFields);
            builder.Entity<TextField>().HasData(this.TextFields);
            builder.Entity<Type>().HasData(this.SeededTypes);
            builder.Entity<Topic>().HasData(this.SeededTopics);
            builder.Entity<Area>().HasData(this.SeededAreas);
        }

        // Users
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Citizen> Citizens { get; set; }

        // Fields
        public DbSet<IntegerField> IntegerFields { get; set; }
        public DbSet<DateField> DateFields { get; set; }
        public DbSet<TextField> TextFields { get; set; }

        // Core
        public DbSet<Request> Requests { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Type> Types { get; set; }


        private Area[] SeededAreas
        {
            get
            {
                return new Area[]
                {
                    AreaTransporte,
                    new Area() { Name = "Espacios publicos y calles"},
                    new Area() { Name = "Limpieza" },
                    new Area() { Name = "Saneamiento" }
                };
            }
        }
        private Area AreaTransporte => new Area { Name = "Transporte", Topics = this.SeededTopics.ToList() };

        private Topic AcosoSexual => new Topic { Name = "Acoso Sexual" };
        private Topic[] SeededTopics
        {
            get
            {
                return new Topic[] { AcosoSexual };
            }
        }


        private Type TaxiAcoso => new Type
        {
            Name = "Taxi - Acoso",
            AdditionalFields = new List<AdditionalField>()
            {
                TextFieldMatricula, DateFieldFechaYHora, IntegerFieldNroMovil
            }
        };



        private Type[] SeededTypes => new Type[] { TaxiAcoso };


        private IntegerField IntegerFieldNroMovil => new IntegerField { IsRequired = true, Name = "Nro de Movil" };
        private IntegerField[] SeededAdditionalIntegerFields
        {
            get
            {
                return new IntegerField[] { IntegerFieldNroMovil };
            }
        }

        private TextField TextFieldMatricula => new TextField { Name = "Matricula" };
        private TextField[] SeededAdditionalTextFields { get { return new TextField[] { TextFieldMatricula }; } }
        private DateField DateFieldFechaYHora => new DateField { Name = "Fecha y hora" };
        private DateField[] SeededAdditionalDateFields { get { return new DateField[] { DateFieldFechaYHora }; } }


        private Admin[] SeededAdmins
        {
            get { return new Admin[] { }; }
        }

        private Citizen[] SeededCitizens
        {
            get { return new Citizen[] { }; }
        }
    }
}
