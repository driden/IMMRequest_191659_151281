namespace IMMRequest.JsonRequestImporter
{
    using System;
    using System.IO;
    using Newtonsoft.Json;
    using RequestImporter;

    public class JsonRequestImporter : IRequestsImportable
    {
        public CreateRequestList Import(string fileContent)
        {
            if (string.IsNullOrEmpty(fileContent))
            {
                throw new Exception($"parameter {nameof(fileContent)} can't be null nor empty");
            }

            using (var streamReader = new StringReader(fileContent))
            {
                string json = streamReader.ReadToEnd();
                var requests = JsonConvert.DeserializeObject<CreateRequestList>(json);
                return requests;
            }
        }
    }
}
