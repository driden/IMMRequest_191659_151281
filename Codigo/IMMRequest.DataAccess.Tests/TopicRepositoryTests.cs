namespace IMMRequest.DataAccess.Tests
{
    using System.Linq;
    using Core.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TopicRepositoryTests : IMMRequestTestBase
    {
        private TopicRepository _repository;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            _repository = new TopicRepository(_context);
        }

        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();
        }

        [TestMethod]
        public void CanAddTopicToDatabase()
        {
            _repository.Add(NewTopic());

            Assert.AreEqual("TestTopic", _context.Topics.First().Name);
            Assert.AreEqual(1, _context.Topics.Count());
        }

        [TestMethod]
        public void CanModifyATopicIntheDatabase()
        {
            var topic = NewTopic();
            _context.Topics.Add(topic);
            _context.SaveChanges();

            topic.Name = "NewTopic";
            _repository.Update(topic);
            Assert.AreEqual("NewTopic", _context.Topics.First().Name);
            Assert.AreEqual(1, _context.Topics.Count());
        }

        [TestMethod]
        public void CanGetATopicFromtheDatabase()
        {
            var topic = NewTopic();
            _context.Topics.Add(topic);
            _context.SaveChanges();

            Assert.AreEqual(topic, _repository.Get(topic.Id));
        }

        [TestMethod]
        public void CanGetAllTopicsFromTheDatabase()
        {
            _context.Topics.AddRange(NewTopic(), NewTopic());
            _context.SaveChanges();

            Assert.AreEqual(2, _repository.GetAll().Count());
        }

        [TestMethod]
        public void CanDeleteATopicIntheDatabase()
        {
            var topic = NewTopic();
            _context.Topics.Add(topic);
            _context.SaveChanges();
            _repository.Remove(topic);
            Assert.AreEqual(0, _context.Topics.Count());
        }
    }
}
