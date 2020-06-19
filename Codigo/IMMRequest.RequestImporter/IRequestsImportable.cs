namespace IMMRequest.RequestImporter
{
    public interface IRequestsImportable
    {
        CreateRequestList Import(string filePath);
    }
}
