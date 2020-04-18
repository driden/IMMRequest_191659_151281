using IMMRequest.Domain;
using IMMRequest.Domain.Fields;
using IMMRequest.Logic.Models;
using System.Collections.Generic;
using Type = IMMRequest.Domain.Type;

namespace IMMRequest.Logic.Tests
{
    public class IMMRequestLogicTestBase
    {
        protected Request NewRequest()
        {
            return new Request
            {
                Citizen = new Citizen { Email = "citizen@mail.com", Name = "Name", PhoneNumber = "555-5555555" },
                Details = "Request Details",
            };
        }

        protected Type NewType()
        {
            DateField dateFieldFechaYHora = new DateField
            {
                Name = "TestAdditionalDateField",
                Range = new List<DateItem>
                {
                    new DateItem { Value = System.DateTime.Today.AddDays(-1) },
                    new DateItem { Value = System.DateTime.Today.AddDays(1) },
                }
            };

            return new Type
            {
                Name = "TestType",
                AdditionalFields = new List<AdditionalField>() { dateFieldFechaYHora }
            };
        }

        protected Topic NewTopic()
        {
            return new Topic
            {
                Name = "TestTopic",
                Types = new List<Type>() { NewType() }
            };
        }

        protected Area NewArea()
        {
            return new Area()
            {
                Id = 1,
                Name = "TestArea",
                Topics = new List<Topic> { NewTopic() }
            };
        }

        protected IEnumerable<AdditionalField> ExtraFields => new List<AdditionalField>
        {
            new IntegerField { IsRequired = true, Name = "Nro de Movil" },
             new TextField { Name = "Matricula" }
        };

        protected CreateRequest CreateRequest => new CreateRequest
        {
            Email = "test@mail.com",
            Details = "test details",
            Name = "test name",
            Phone = "phone number",
        };

    };
}
