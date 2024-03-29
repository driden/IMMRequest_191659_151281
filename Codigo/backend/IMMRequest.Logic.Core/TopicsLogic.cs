namespace IMMRequest.Logic.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess.Interfaces;
    using Domain;
    using Interfaces;
    using Models.Topic;

    public class TopicsLogic : ITopicsLogic
    {
        private readonly IRepository<Topic> _topicRepository = null;

        public TopicsLogic(IRepository<Topic> topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public int Add(TopicModel createTopicModel)
        {
            var newTopic = new Topic
            {
                AreaId = createTopicModel.AreaId,
                Name = createTopicModel.Name
            };

            _topicRepository.Add(newTopic);
            return newTopic.Id;
        }

        public IEnumerable<TopicModel> GetAll(int areaId)
        {
            var all = _topicRepository.GetAll().Where(topic => topic.AreaId == areaId);
            return all.Select(CreateModel);

        }

        private TopicModel CreateModel(Topic topic)
        {
            var model =
              new TopicModel
              {
                  AreaId = topic.AreaId,
                  Id = topic.Id,
                  Name = topic.Name,
                  Types = topic.Types.Select(t => t.Id).ToList()
              };
            return model;
        }
    }
}
