namespace IMMRequest.Logic.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess.Interfaces;
    using Domain;
    using Interfaces;
    using Logic.Exceptions;
    using Models.State;

    public class ReportsLogic : IReportsLogic
    {
        private readonly IRepository<Request> _requestRepository;

        public ReportsLogic(
            IRepository<Request> requestRepository
        )
        {
            _requestRepository = requestRepository;
        }

        public IEnumerable<StateReportModel> GetRequestByMail(string mail, DateTime startDate, DateTime endDate)
        {
            ValidateStringValueNotNullOrEmpty(mail);
            ValidateDateRange(startDate, endDate);

            var requests = _requestRepository.GetAll();

            var status = requests
                .Where(request => request.Citizen.Email == mail)
                .Where(request => request.CreationDateTime <= endDate && request.CreationDateTime >= startDate)
                .GroupBy(s => s.Status.Description)
                .Select(group => new StateReportModel
                {
                    StateName = group.Key,
                    Quantity = group.Count(),
                    Ids = group.ToArray().Select(request => request.Id)
                });

            return status;
        }

        private void ValidateStringValueNotNullOrEmpty(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidMailFormatException(
                    "Value for mail cannot be empty or null");
            }
        }

        private void ValidateDateRange(DateTime startDate, DateTime endDate) 
        {
            if ((startDate > endDate) || (endDate > System.DateTime.Today))
            {
                throw new InvalidDateRageException(
                    "Invalid range for date start and end");
            }
        }
    }
}