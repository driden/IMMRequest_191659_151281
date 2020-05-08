namespace IMMRequest.DataAccess.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Domain.Fields;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Type = Domain.Type;

    public class IMMRequestDBSeeder : IDbSeeder
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public IMMRequestDBSeeder(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Seed()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<DbContext>();

            context.Database.Migrate();
            if (!context.Set<Area>().Any())
            {
                context.Set<Area>().AddRange(SeededAreas);
                context.Set<Admin>().AddRange(SeededAdmins);
                context.SaveChanges();
            }
        }

        private Area[] SeededAreas
        {
            get
            {
                return new[]
                {
                    AreaTransporte,
                    new Area { Name = "Espacios publicos y calles"},
                    new Area { Name = "Limpieza" },
                    new Area { Name = "Saneamiento" }
                };
            }
        }
        private Area AreaTransporte => new Area { Name = "Transporte", Topics = SeededTopics.ToList() };

        private Topic AcosoSexual => new Topic { Name = "Acoso Sexual", Types = SeededTypes.ToList() };
        private Topic[] SeededTopics
        {
            get
            {
                return new[] { AcosoSexual };
            }
        }

        private Type TaxiAcoso => new Type
        {
            Name = "Taxi - Acoso",
            AdditionalFields = new List<AdditionalField>
            {
                TextFieldMatricula, DateFieldFechaYHora, IntegerFieldNroMovil
            }
        };
        private Type[] SeededTypes => new[] { TaxiAcoso };


        private IntegerField IntegerFieldNroMovil => new IntegerField { IsRequired = true, Name = "Nro de Movil", Range = new List<IntegerItem> { new IntegerItem { Value = 0 }, new IntegerItem { Value = 99999999 } } };

        private TextField TextFieldMatricula => new TextField { Name = "Matricula" };

        private DateField DateFieldFechaYHora => new DateField
        {
            Name = "Fecha y hora",
            Range = new List<DateItem>
            {
                new DateItem { Value = DateTime.Today.AddYears(-10) },
                new DateItem { Value = DateTime.Today.AddYears(10) }
            }
        };

        private Admin[] SeededAdmins
        {
            get
            {
                return new[]
                {
                    new Admin { Name = "Admin Foo" , Email = "admin@foo.com", Password = "pass", Token = new Guid()}
                };
            }
        }
    }

    public interface IDbSeeder
    {
        void Seed();
    }
}
