using System.Collections.Generic;

namespace IMMRequest.Logic.Core
{
    using System.Linq;
    using DataAccess.Interfaces;
    using Domain;
    using Interfaces;
    using Models.Topic;

    public class TopicsLogic : ITopicsLogic
    {
        private readonly IRepository<Topic> _repository = null;

        public TopicsLogic(IRepository<Topic> repository)
        {
            this._repository = repository;
        }

        public IEnumerable<TopicModel> GetAllTopics(int areaId)
        {
            var all = _repository.GetAll().Where(topic => topic.AreaId == areaId);
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
