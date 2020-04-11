using System.Linq;
using IMMRequest.DataAccess.Repositories;
using IMMRequest.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IMMRequest.DataAccess.Tests
{
    [TestClass]
    public class TopicRepositoryTests: IMMRequestTestBase
    {
        private TopicRepository _repository;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            _repository = new TopicRepository(_context);
        }

        [TestMethod]
        public void CanAddTopicToDatabase()
        {
            _repository.Add(NewTopic());

            Assert.AreEqual("TestTopic", _context.Set<Topic>().First().Name);
            Assert.AreEqual(1, _context.Set<Topic>().Count());
        }
    }
}
