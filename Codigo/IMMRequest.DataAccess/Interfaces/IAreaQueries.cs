namespace IMMRequest.DataAccess.Interfaces
{
    using Domain;

    public interface IAreaQueries
    {
        Area FindWithTypeId(int typeId);
    }
}
