using System.Linq;
using IMMRequest.DataAccess.Core.Repositories;
using IMMRequest.Domain;
using IMMRequest.Domain.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IMMRequest.DataAccess.Tests
{
    [TestClass]
    public class TypeRepositoyTests : IMMRequestTestBase
    {
        private TypeRepository _repository;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            _repository = new TypeRepository(_context);
        }

        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();
        }

        [TestMethod]
        public void CanAddTypeToDatabase()
        {
            Type taxiType = NewType();
            foreach (var additionalField in ExtraFields)
            {
                taxiType.AdditionalFields.Add(additionalField);
            }

            _repository.Add(taxiType);

            Assert.AreEqual(1, _context.Set<Domain.Type>().Count());
            Assert.AreEqual(3, _context.Set<Domain.Type>().First().AdditionalFields.Count());
            Assert.AreEqual(2, _context.Set<DateItem>().Count());
        }

        [TestMethod]
        public void CanRemoveATypeFromDatabase()
        {
            Type taxiType = NewType();
            foreach (var additionalField in ExtraFields)
            {
                taxiType.AdditionalFields.Add(additionalField);
            }

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
            Type taxiType = NewType();
            foreach (var additionalField in ExtraFields)
            {
                taxiType.AdditionalFields.Add(additionalField);
            }

            _context.Types.Add(taxiType);
            _context.SaveChanges();

            taxiType.AdditionalFields.RemoveAt(1);
            var dateField = (DateField)taxiType.AdditionalFields[0];
            dateField.Name = "Fecha";
            dateField.Range = dateField.Range.Skip(1).ToList();

            _repository.Update(taxiType);

            Assert.AreEqual(2, _context.Set<Domain.Type>().First().AdditionalFields.Count);
            Assert.AreEqual("Fecha", _context.Set<Domain.Type>().First().AdditionalFields[0].Name);
            Assert.AreEqual(1, ((DateField)_context.Set<Domain.Type>().First().AdditionalFields[0]).Range.Count());
        }

        [TestMethod]
        public void CanModifyTypeData()
        {
            Type taxiType = NewType();

            _context.Types.Add(taxiType);
            _context.SaveChanges();

            taxiType.Name = "Updated Taxi";

            _repository.Update(taxiType);

            Assert.AreEqual("Updated Taxi", _context.Set<Domain.Type>().First().Name);
        }

        [TestMethod]
        public void CanReadATypeFromDatabase()
        {
            Type taxiType = NewType();

            _context.Types.Add(taxiType);
            _context.SaveChanges();

            Assert.AreEqual(taxiType, _repository.Get(taxiType.Id));
        }

        [TestMethod]
        public void CanGetAllTypesFromTheDatabase()
        {
            _context.Set<Type>().AddRange(NewType(), NewType());
            _context.SaveChanges();

            Assert.AreEqual(2, _repository.GetAll().Count());
        }
    }
}
