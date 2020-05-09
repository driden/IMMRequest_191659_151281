using System;

namespace IMMRequest.Logic.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.Fields;
    using Exceptions;
    using Exceptions.CreateTopic;
    using Interfaces;
    using Models;

    public class TypesLogic : ITypesLogic
    {
        private readonly IRepository<Topic> _topicsRepository;
        private readonly IRepository<Type> _typesRepository;

        public TypesLogic(
            IRepository<Topic> topicsRepository,
            IRepository<Type> typesRepository)
        {
            this._topicsRepository = topicsRepository;
            this._typesRepository = typesRepository;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(CreateTypeRequest createTypeRequest)
        {
            ValidateTopicIdNumber(createTypeRequest.TopicId);
            ValidateAdditionalFieldsType(createTypeRequest);
            var topic = this._topicsRepository.Get(createTypeRequest.TopicId);
            ValidateTopic(createTypeRequest.TopicId, topic);
            ValidateTopicName(createTypeRequest, topic);

            var newType = new Type
            {
                IsActive = true,
                Name = createTypeRequest.Name,
                TopicId = createTypeRequest.TopicId,
                AdditionalFields = new List<AdditionalField>()
            };

            foreach (var additionalField in createTypeRequest.AdditionalFields)
            {
                var fieldType = GetFieldType(additionalField.FieldType);

                switch (fieldType)
                {
                    case FieldType.Date:
                        var range = additionalField.Range.ToList();
                        var dateRangeValues = range.Select(x => TryToParseDateValue(x.Value));
                        var dateField = new DateField
                        {
                            FieldType = FieldType.Date,
                            IsRequired = additionalField.IsRequired,
                            Name = additionalField.Name,
                        };

                        dateField.Range = dateRangeValues.Select(rangeValue => new DateItem { Value = rangeValue });
                        dateField.ValidateRangeIsCorrect();
                        newType.AdditionalFields.Add(dateField);
                        break;

                    case FieldType.Integer:
                        var intRange = additionalField.Range.ToList();
                        var intRangeValues = intRange.Select(x => TryToParseIntValue(x.Value));
                        var intField = new IntegerField
                        {
                            FieldType = FieldType.Date,
                            IsRequired = additionalField.IsRequired,
                            Name = additionalField.Name,
                        };

                        intField.Range = intRangeValues.Select(rangeValue => new IntegerItem { Value = rangeValue });
                        intField.ValidateRangeIsCorrect();
                        newType.AdditionalFields.Add(intField);
                        break;

                    case FieldType.Text:
                        var textRange = additionalField.Range.ToList();
                        var textRangeValues = textRange.Select(x => x.Value);
                        var textField = new TextField
                        {
                            FieldType = FieldType.Date,
                            IsRequired = additionalField.IsRequired,
                            Name = additionalField.Name,
                        };

                        textField.Range = textRangeValues.Select(rangeValue => new TextItem { Value = rangeValue });
                        textField.ValidateRangeIsCorrect();
                        newType.AdditionalFields.Add(textField);
                        break;
                }
            }

            _typesRepository.Add(newType);
        }

        #region Utilities
        private int TryToParseIntValue(string intValue)
        {
            if (!int.TryParse(intValue, out var num))
            {
                throw new InvalidFieldValueCastForFieldTypeException(
                    $"value '{intValue}' cannot be read as an integer");
            }

            return num;
        }

        private DateTime TryToParseDateValue(string dateValue)
        {
            if (!DateTime.TryParse(dateValue, out var parseDate))
            {
                throw new InvalidFieldValueCastForFieldTypeException(
                    $"value '{dateValue}' for field cannot be read as a date");
            }

            return parseDate;
        }

        public FieldType GetFieldType(string fieldTypeStr)
        {
            switch (fieldTypeStr)
            {
                case "int": return FieldType.Integer;
                case "text": return FieldType.Text;
                default: return FieldType.Date;
            }
        }

        #endregion Utilities


        #region Validations
        public void ValidateTopicIdNumber(int topicId)
        {
            if (topicId < 1)
            {
                throw new InvalidTopicIdException($"topicId {topicId} is invalid.");
            }
        }

        public void ValidateTopic(int topicId, Topic topic)
        {
            if (topic == null)
            {
                throw new NoSuchTopicException($"No topic with id {topicId} exists.");
            }
        }

        public void ValidateAdditionalFieldsType(CreateTypeRequest request)
        {
            var validTypes = new[] { "int", "text", "date" };
            var invalidTypes = request.AdditionalFields.Select(field => field.FieldType).Distinct().Except(validTypes).ToList();

            if (invalidTypes.Any())
            {
                throw new InvalidFieldTypeException($"{string.Join(',', invalidTypes)} are not valid field types");
            }
        }

        private void ValidateTopicName(CreateTypeRequest createTypeRequest, Topic topic)
        {
            if (string.IsNullOrWhiteSpace(createTypeRequest.Name))
            {
                throw new EmptyTypeNameException("Provided type Name cannot be empty.");
            }

            if (createTypeRequest.Name == topic.Name)
            {
                throw new ExistingTypeNameException(
                    $"Name \"{createTypeRequest.Name}\" is already taken, pick another one.");
            }
        }

        #endregion Validations
    }
}
