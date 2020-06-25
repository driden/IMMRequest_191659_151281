namespace IMMRequest.Logic.Interfaces
{
    using System.Collections.Generic;
    using Models.Topic;

    public interface ITopicsLogic
    {
        int Add(TopicModel createTopicModel);
        IEnumerable<TopicModel> GetAll(int areaId);
    }
}
