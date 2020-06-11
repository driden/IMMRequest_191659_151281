using System.Collections.Generic;

namespace IMMRequest.Logic.Interfaces
{
    using Models.Area;

    public interface IAreasLogic
    {
        IEnumerable<AreaModel> GetAll();
    }
}
