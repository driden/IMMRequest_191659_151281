namespace IMMRequest.Logic.Tests
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using Domain.Fields;
    using Models;
    using Type = Domain.Type;

    public class IMMRequestLogicTestBase
    {
        protected Request NewRequest()
        {
            return new Request
            {
                Citizen = new Citizen { Email = "citizen@mail.com", Name = "Name", PhoneNumber = "555-5555555" },
                Details = "Request Details",
                Type = NewType(),
                Id = 1
            };
        }

        protected List<RequestField> GetSomeAdditionaFields()
        {
            return new List<RequestField>
            {
                new IntRequestField{ Name = "num", Value = 4},
                new TextRequestField { Name = "text", Value = "some text"},
                new DateRequestField { Name = "date", Value = DateTime.Today}
            };
        }

        protected Type NewType()
        {
            DateField dateFieldFechaYHora = new DateField
            {
                Name = "TestAdditionalDateField",
                Range = new List<DateItem>
                {
                    new DateItem { Value = DateTime.Today.AddDays(-1) },
                    new DateItem { Value = DateTime.Today.AddDays(1) },
                }
            };

            return new Type
            {
                Name = "TestType",
                AdditionalFields = new List<AdditionalField> { dateFieldFechaYHora }
            };
        }

        protected Topic NewTopic()
        {
            return new Topic
            {
                Name = "TestTopic",
                Types = new List<Type> { NewType() }
            };
        }

        protected Area NewArea()
        {
            return new Area
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
            Phone = "555-555555",
        };

        protected CreateRequest NewCreateRequestBody()
        {
            return new CreateRequest
            {
                Email = "test@mail.com",
                Details = "test details",
                Name = "test name",
                Phone = "phone number",
                AdditionalFields = new List<FieldRequestModel>()
            };
        }
    }
}
