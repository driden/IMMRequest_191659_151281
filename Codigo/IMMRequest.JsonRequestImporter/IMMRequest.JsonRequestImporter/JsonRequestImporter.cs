namespace IMMRequest.JsonRequestImporter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Newtonsoft.Json;
    using RequestImporter;

    public class JsonRequestImporter : IRequestsImportable
    {
        public CreateRequestList Import(string filePath)
        {
            var fullPath = Path.Join(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), filePath);
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception($"parameter {nameof(filePath)} can't be null nor empty");
            }

            if (!File.Exists(filePath))
            {
                throw new Exception($"parameter {nameof(filePath)} needs to be a valid json file");
            }


            using (var streamReader = new StreamReader(fullPath))
            {
                string json = streamReader.ReadToEnd();
                var requests = JsonConvert.DeserializeObject<CreateRequestList>(json);
                return requests;
            }
        }
    }
}
