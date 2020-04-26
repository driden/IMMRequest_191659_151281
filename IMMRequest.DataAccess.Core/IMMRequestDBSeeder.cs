namespace IMMRequest.DataAccess.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Domain.Fields;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

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
                context.Set<Citizen>().AddRange(SeededCitizens);
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


        private IntegerField IntegerFieldNroMovil => new IntegerField { IsRequired = true, Name = "Nro de Movil" };
        private IntegerField[] SeededAdditionalIntegerFields
        {
            get
            {
                return new[] { IntegerFieldNroMovil };
            }
        }

        private TextField TextFieldMatricula => new TextField { Name = "Matricula" };
        private TextField[] SeededAdditionalTextFields { get { return new[] { TextFieldMatricula }; } }
        private DateField DateFieldFechaYHora => new DateField { Name = "Fecha y hora" };
        private DateField[] SeededAdditionalDateFields { get { return new[] { DateFieldFechaYHora }; } }


        private Admin[] SeededAdmins
        {
            get { return new Admin[] { }; }
        }

        private Citizen[] SeededCitizens
        {
            get { return new Citizen[] { }; }
        }
    }

    public interface IDbSeeder
    {
        void Seed();
    }
}
