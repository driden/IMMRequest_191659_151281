using System.Collections.Generic;

namespace IMMRequest.Logic.Core
{
    using System.Linq;
    using DataAccess.Interfaces;
    using Domain;
    using Models.Topic;

    public class TopicsLogic
    {
        private readonly IRepository<Topic> _repository = null;

        public TopicsLogic(IRepository<Topic> repository)
        {
            this._repository = repository;
        }

        public IEnumerable<TopicModel> GetAllTopics()
        {
            var all = _repository.GetAll();
            return all.Select(Crear);

        }

        private TopicModel Crear(Topic topic)
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
