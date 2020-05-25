using System.Collections.Generic;
using System.Linq;
using IMMRequest.DataAccess.Interfaces;
using IMMRequest.Domain;
using IMMRequest.Logic.Exceptions;
using IMMRequest.Logic.Interfaces;
using IMMRequest.Logic.Models;

namespace IMMRequest.Logic.Core
{
    public class ReportsLogic : IReportsLogic
    {
        private readonly IRepository<Request> _requestRepo;

        public ReportsLogic(IRepository<Request> requestRepository)
        {
            _requestRepo = requestRepository;
        }
        public IEnumerable<GetAllRequestsByMail> ReportsRequestByMail(string mail)
        {
            ValidateStringValueNotNullOrEmpty(mail);

            return null;
            //_requestRepo.GetAllByCondition(req => req.Citizen.Exists(c => c.Name == mail)).Select(req => new GetAllRequestsByMail(req));
        }

        #region Validation Methods
        private static void ValidateStringValueNotNullOrEmpty(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidMailFormatException(
                    $"value for mail cannot be empty or null");
            }
        }

        #endregion

    }
}
