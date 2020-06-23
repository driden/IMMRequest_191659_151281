namespace IMMRequest.Logic.Interfaces
{
    using Models.Files;

    public interface IImportsLogic
    {
        int[] Import(FileRequestModel fileRequest);
    }
}
