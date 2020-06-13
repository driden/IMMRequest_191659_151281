namespace IMMRequest.Logic.Core
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.Fields;
    using Exceptions;
    using Exceptions.CreateTopic;
    using Exceptions.RemoveType;
    using Interfaces;
    using Models.Type;
    using Type = Domain.Type;

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
            ValidateTypeIdNumber(id);
            var typeInDb = _typesRepository.Get(id);
            ValidateTypeCanBeDeleted(id, typeInDb);

            _typesRepository.Remove(typeInDb);
        }

        public int Add(CreateTypeRequest createTypeRequest)
        {
            ValidateTopicIdNumber(createTypeRequest.TopicId);
            ValidateAdditionalFieldsNames(createTypeRequest);
            ValidateAdditionalFieldsType(createTypeRequest);
            var topic = this._topicsRepository.Get(createTypeRequest.TopicId);
            ValidateTopic(createTypeRequest.TopicId, topic);
            ValidateTypeName(createTypeRequest, topic);

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
                        var dateRangeValues = range.Select(TryToParseDateValue);
                        var dateField = new DateField
                        {
                            FieldType = FieldType.Date,
                            IsRequired = additionalField.IsRequired,
                            Name = additionalField.Name,
                        };

                        dateField.Range = dateRangeValues.Select(rangeValue => new DateItem { Value = rangeValue }).ToList();
                        dateField.ValidateRangeIsCorrect();
                        newType.AdditionalFields.Add(dateField);
                        break;

                    case FieldType.Integer:
                        var intRange = additionalField.Range.ToList();
                        var intRangeValues = intRange.Select(TryToParseIntValue);
                        var intField = new IntegerField
                        {
                            FieldType = FieldType.Date,
                            IsRequired = additionalField.IsRequired,
                            Name = additionalField.Name,
                        };

                        intField.Range = intRangeValues.Select(rangeValue => new IntegerItem { Value = rangeValue }).ToList();
                        intField.ValidateRangeIsCorrect();
                        newType.AdditionalFields.Add(intField);
                        break;

                    case FieldType.Text:
                        var textRange = additionalField.Range.ToList();
                        var textField = new TextField
                        {
                            FieldType = FieldType.Date,
                            IsRequired = additionalField.IsRequired,
                            Name = additionalField.Name,
                        };

                        textField.Range = textRange.Select(rangeValue => new TextItem { Value = rangeValue }).ToList();
                        textField.ValidateRangeIsCorrect();
                        newType.AdditionalFields.Add(textField);
                        break;
                }
            }

            _typesRepository.Add(newType);

            return newType.Id;
        }

        public IEnumerable<TypeModel> GetAll(int topicId)
        {
            var allTypes = _typesRepository.GetAll();
            return allTypes?
                .Where(type => type.IsActive && type.TopicId == topicId)
                .Select(type => new TypeModel
                {
                    Name = type.Name,
                    Id = type.Id,
                    IsActive = type.IsActive,
                    TopicId = type.TopicId,
                    AdditionalFields = type.AdditionalFields
                        .Select(additionalField => new Models.Type.AdditionalFieldModel
                        {
                            Name = additionalField.Name,
                            Id = additionalField.Id,
                            FieldType = additionalField.FieldType == FieldType.Integer
                                ? "integer"
                                : additionalField.FieldType == FieldType.Date
                                    ? "date"
                                    : "text",
                            IsRequired = additionalField.IsRequired,
                            Range = additionalField.FieldType == FieldType.Integer ? ((IntegerField)additionalField).Range.Select(intItem => intItem.Value.ToString())
                                : additionalField.FieldType == FieldType.Date
                                    ? ((DateField)additionalField).Range.Select(dateItem => dateItem.ToString())
                                    : ((TextField)additionalField).Range.Select(textItem => textItem.Value)
                        })
                        .ToList()
                });
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
            if (!DateTime.TryParse(dateValue, new CultureInfo("es-ES"), DateTimeStyles.None, out var parseDate))
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

        public void ValidateTypeCanBeDeleted(int id, Type type)
        {
            if (type == null || !type.IsActive)
            {
                throw new NoSuchTypeException($"No type with id exists {id}");
            }
        }

        public void ValidateTypeIdNumber(int typeId)
        {
            if (typeId < 1)
            {
                throw new InvalidIdException($"typeId \"{typeId}\" is invalid.");
            }
        }

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

        private void ValidateAdditionalFieldsNames(CreateTypeRequest createTypeRequest)
        {
            var names = createTypeRequest.AdditionalFields.Select(af => af.Name).ToList();

            if (names.Any(string.IsNullOrWhiteSpace))
            {
                throw new InvalidNameForAdditionalFieldException("Cannot provide an empty additional field name");
            }
            var repeated = names
                .GroupBy(x => x)
                .Where(group => group.Count() > 1)
                .Select(x => x.Key).ToList();

            if (repeated.Any())
            {
                throw new InvalidAdditionalFieldForTypeException($"Some of the additional field names are repeated \"{string.Join(',', repeated)}\"");
            }
        }

        private void ValidateTypeName(CreateTypeRequest createTypeRequest, Topic topic)
        {
            if (string.IsNullOrWhiteSpace(createTypeRequest.Name))
            {
                throw new EmptyTypeNameException("Provided type Name cannot be empty.");
            }

            var existingTypeNames = topic.Types?.Select(type => type.Name);
            if (existingTypeNames != null && existingTypeNames.Contains(createTypeRequest.Name))
            {
                throw new ExistingTypeNameException(
                    $"Name \"{createTypeRequest.Name}\" is already taken, pick another one.");
            }
        }

        #endregion Validations
    }
}
