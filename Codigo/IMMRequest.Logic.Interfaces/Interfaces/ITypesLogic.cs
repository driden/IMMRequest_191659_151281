namespace IMMRequest.Logic.Interfaces
{
    using System.Collections.Generic;
    using Models.Type;

    public interface ITypesLogic
    {
        void Remove(int id);
        int Add(CreateTypeRequest createTypeRequest);
        IEnumerable<TypeModel> GetAll(int topicId);
    }
}
