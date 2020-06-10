namespace IMMRequest.Logic.Core
{
    using System.Collections.Generic;
    using Exceptions;
    using IMMRequest.DataAccess.Interfaces;
    using IMMRequest.Domain;
    using Interfaces;

    public class ReportsLogic : IReportsLogic
    {
        private readonly IRepository<Request> _requestRepo;
        
        public ReportsLogic(
            IRepository<Request> requestRepository
        )
        {
            _requestRepo = requestRepository;
        }
        public IEnumerable<Request> GetRequestByMail(string mail)
        {
            ValidateStringValueNotNullOrEmpty(mail);

            IEnumerable<Request> requests = _requestRepo.GetAllByCondition(req => req.Citizen.Name.Equals(mail));

            // _requestRepo.GetAllByCondition(req => req.Citizen.Name == mail);
            //_requestRepo.GetAllByCondition(req => req.Citizen.Exists(c => c.Name == mail)).Select(req => new GetAllRequestsByMail(req));


            // coniditions.GroupBy(c => typeof(c.State)))
            //    .Where(condiciones)
            //    .Select(g => new { Tipo = g.Key, Cantidad = g.Count() ... };

            return requests;
        }

        #region Validation Methods
        private static void ValidateStringValueNotNullOrEmpty(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidMailFormatException(
                    "value for mail cannot be empty or null");
            }
        }

        #endregion

    }
}
