using System.Collections.Generic;
using System.Linq;
using IMMRequest.DataAccess.Repositories;
using IMMRequest.Domain;
using IMMRequest.Domain.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IMMRequest.DataAccess.Tests
{
    [TestClass]
    public class TypeRepositoyTests
    {
        private IMMRequestContext _context;
        private TypeRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var dbContextOptions =
                new DbContextOptionsBuilder<IMMRequestContext>().UseInMemoryDatabase("Types");
            _context = new IMMRequestContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();
            _repository = new TypeRepository(_context);
        }

        [TestCleanup]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void CanAddTypeToDatabase()
        {
            Type taxiType = TaxiType();

            _repository.Add(taxiType);

            Assert.AreEqual(1, _context.Set<Domain.Type>().Count());
            Assert.AreEqual(3, _context.Set<Domain.Type>().First().AdditionalFields.Count());
            Assert.AreEqual(2, _context.Set<DateItem>().Count());
        }

        [TestMethod]
        public void CanRemoveATypeFromDatabase()
        {
            Type taxiType = TaxiType();

            _context.Types.Add(taxiType);
            _context.SaveChanges();

            Assert.AreEqual(1, _context.Set<Domain.Type>().Count());
            Assert.AreEqual(3, _context.Set<Domain.Type>().First().AdditionalFields.Count());
            Assert.AreEqual(2, _context.Set<DateItem>().Count());

            _repository.Remove(taxiType);
            _context.SaveChanges();

            Assert.AreEqual(0, _context.Set<Domain.Type>().Count());
            Assert.AreEqual(0, _context.Set<AdditionalField>().Count());
            Assert.AreEqual(0, _context.Set<DateItem>().Count());
        }

        [TestMethod]
        public void CanModifyTheExitingFieldsInAType()
        {
            Type taxiType = TaxiType();

            _context.Types.Add(taxiType);
            _context.SaveChanges();

            taxiType.AdditionalFields.RemoveAt(0);
            var dateField = (DateField)taxiType.AdditionalFields[1];
            dateField.Name = "Fecha";
            dateField.Range = dateField.Range.Skip(1).ToList();

            _repository.Update(taxiType);

            Assert.AreEqual(2, _context.Set<Domain.Type>().First().AdditionalFields.Count);
            Assert.AreEqual("Fecha", _context.Set<Domain.Type>().First().AdditionalFields[1].Name);
            Assert.AreEqual(1, ((DateField) _context.Set<Domain.Type>().First().AdditionalFields[1]).Range.Count());
        }

        [TestMethod]
        public void CanModifyTypeData()
        {
            Type taxiType = TaxiType();

            _context.Types.Add(taxiType);
            _context.SaveChanges();

            taxiType.Name = "Updated Taxi";

            _repository.Update(taxiType);

            Assert.AreEqual("Updated Taxi", _context.Set<Domain.Type>().First().Name);
        }

        [TestMethod]
        public void CanReadATypeFromDatabase()
        {
            Type taxiType = TaxiType();

            _context.Types.Add(taxiType);
            _context.SaveChanges();

            Assert.AreEqual(taxiType,_repository.Get(taxiType.Id));
        }

        private Domain.Type TaxiType()
        {
            IntegerField integerFieldNroMovil = new IntegerField { IsRequired = true, Name = "Nro de Movil" };
            TextField textFieldMatricula = new TextField { Name = "Matricula" };
            DateField dateFieldFechaYHora = new DateField
            {
                Name = "Fecha y hora",
                Range = new List<DateItem>
                {
                    new DateItem { Value = System.DateTime.Today.AddDays(-1) },
                    new DateItem { Value = System.DateTime.Today.AddDays(1) },
                }
            };

            Domain.Type taxiType = new Domain.Type
            {
                Name = "Taxi - Acoso",
                AdditionalFields = new List<AdditionalField>() { integerFieldNroMovil, textFieldMatricula, dateFieldFechaYHora }
            };

            return taxiType;
        }

    }
}
