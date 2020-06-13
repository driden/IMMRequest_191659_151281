namespace IMMRequest.Logic.Interfaces
{
    using System.Collections.Generic;
    using Models.Topic;

    public interface ITopicsLogic
    {
        IEnumerable<TopicModel> GetAll(int areaId);
    }
}
