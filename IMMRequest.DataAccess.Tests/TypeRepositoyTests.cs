using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMMRequest.DataAccess.Repositories;
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
        public void CanRemoveATypeFromDatabase()
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

            Domain.Type taxyType = new Domain.Type
            {
                Name = "Taxi - Acoso",
                AdditionalFields = new List<AdditionalField>() { integerFieldNroMovil, textFieldMatricula, dateFieldFechaYHora }
            };

            _context.Types.Add(taxyType);
            _context.SaveChanges();

            Assert.AreEqual(1, _context.Set<Domain.Type>().Count());
            Assert.AreEqual(3, _context.Set<Domain.Type>().First().AdditionalFields.Count());
            Assert.AreEqual(2, _context.Set<DateItem>().Count());

            _context.Types.Remove(taxyType);
            _context.SaveChanges();

            Assert.AreEqual(0, _context.Set<Domain.Type>().Count());
            Assert.AreEqual(0, _context.Set<AdditionalField>().Count());
            Assert.AreEqual(0, _context.Set<DateItem>().Count());

        }

    }
}
