namespace IMMRequest.Logic.Interfaces
{
    using Models;

    public interface ITypesLogic
    {
        void Remove(int id);
        int Add(CreateTypeRequest createTypeRequest);
    }
}
