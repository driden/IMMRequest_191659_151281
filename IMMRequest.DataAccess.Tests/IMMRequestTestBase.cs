using System.Collections.Generic;
using IMMRequest.Domain;
using IMMRequest.Domain.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Type = IMMRequest.Domain.Type;

namespace IMMRequest.DataAccess.Tests
{
    [TestClass]
    public class IMMRequestTestBase
    {
        protected IMMRequestContext _context;

        [TestInitialize]
        public virtual void Setup()
        {
            var dbContextOptions =
                new DbContextOptionsBuilder<IMMRequestContext>().UseInMemoryDatabase("Types");
            _context = new IMMRequestContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        protected Type Newtype()
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
                Types = new List<Type>() { Newtype() }
            };
        }

        protected Area NewArea()
        {
            return new Area()
            {
                Name = "TestArea",
                Topics = new List<Topic> { NewTopic() }
            };
        }
        protected IEnumerable<AdditionalField> ExtraFields => new List<AdditionalField>
        {
            new IntegerField { IsRequired = true, Name = "Nro de Movil" },
             new TextField { Name = "Matricula" }
        };
    };
}

