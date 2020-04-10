using IMMRequest.Domain;
using IMMRequest.Domain.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace IMMRequest.DataAccess
{
    public class IMMRequestDBSeeder : IDbSeeder
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public IMMRequestDBSeeder(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public void Seed()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<IMMRequestContext>();

            context.Database.Migrate();
            //context.IntegerFields.AddRange(SeededAdditionalIntegerFields);
            //context.DateFields.AddRange(SeededAdditionalDateFields);
            //context.TextFields.AddRange(SeededAdditionalTextFields);
            context.Areas.AddRange(SeededAreas);
            context.Citizens.AddRange(SeededCitizens);
            context.Admins.AddRange(SeededAdmins);
            context.SaveChanges();
        }

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
        private Area AreaTransporte => new Area { Name = "Transporte", Topics = SeededTopics.ToList() };

        private Topic AcosoSexual => new Topic { Name = "Acoso Sexual", Types = SeededTypes.ToList() };
        private Topic[] SeededTopics
        {
            get
            {
                return new Topic[] { AcosoSexual };
            }
        }


        private Domain.Type TaxiAcoso => new Domain.Type
        {
            Name = "Taxi - Acoso",
            AdditionalFields = new List<AdditionalField>()
            {
                TextFieldMatricula, DateFieldFechaYHora, IntegerFieldNroMovil
            }
        };
        private Domain.Type[] SeededTypes => new Domain.Type[] { TaxiAcoso };


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

    public interface IDbSeeder
    {
        void Seed();
    }
}
