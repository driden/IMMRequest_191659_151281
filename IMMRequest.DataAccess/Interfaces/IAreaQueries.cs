using IMMRequest.Domain;

namespace IMMRequest.DataAccess.Interfaces
{
    public interface IAreaQueries
    {
        Area FindWithTopicId(int typeId);
    }
}
