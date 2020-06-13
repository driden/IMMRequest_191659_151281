namespace IMMRequest.Logic.Interfaces
{
    using System.Collections.Generic;

    using Models.Area;

    public interface IAreasLogic
    {
        IEnumerable<AreaModel> GetAll();
    }
}
