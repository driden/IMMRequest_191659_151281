namespace IMMRequest.Logic.Core
{
    using System.Linq;
    using Interfaces;
    using Models.Files;
    using RequestImporter;

    public class ImportsLogic : IImportsLogic
    {
        private readonly IRequestsLogic _requestsLogic;

        public ImportsLogic(IRequestsLogic requestsLogic)
        {
            this._requestsLogic = requestsLogic;
        }

        public int[] Import(FileRequestModel fileRequest)
        {
            var abstractImporter = new AbstractRequestImporter();
            CreateRequestList requestsInFile = abstractImporter.ParseFile(fileRequest.Content, fileRequest.FileType);

            var requestsToAdd = requestsInFile.Requests;

            return requestsToAdd.Select(_requestsLogic.Add).ToArray();

        }
    }
}
