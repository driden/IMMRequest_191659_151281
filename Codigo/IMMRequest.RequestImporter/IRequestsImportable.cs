namespace IMMRequest.RequestImporter
{
    using System.Collections.Generic;
    using Logic.Models.Request;

    public interface IRequestsImportable
    {
        List<CreateRequest> Import(string filePath);
    }
}
