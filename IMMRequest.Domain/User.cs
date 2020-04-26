namespace IMMRequest.Domain
{
    using System.Numerics;
    using System.Text.RegularExpressions;
    using Exceptions;

    public class User
    {
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {   // Validate name format, not null and more 3 caracters
                if(Regex.Match(value, @"^[\w\d_-][\s\w\d]{2,50}$").Success)
                {
                    _name = value;
                }
                else
                {
                    throw new InvalidNameFormatException("The name format is incorrect. More than 3 characters and less than 50");
                }
            }
        }

        private string _email;
        public string Email /* Unique */
        {
            get => _email;
            set
            {   // Validate email format
                if (Regex.Match(value, @"^[^@]+@[^@]+\.[a-zA-Z]{2,}$").Success)
                {
                    _email = value;
                }
                else
                {
                    throw new InvalidEmailException("The email format is not valid");
                }
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                // The phone can have characters such as " - ", "+", "(", ")", (among others),
                // in addition to numeric characters.
                // There is no maximum length.
                if (Regex.Match(value, @"^[+]?[(]?[0-9]+[)]?[-0-9]*[0-9]$").Success)
                {
                    _phoneNumber = value;
                }
                else
                {
                    throw new InvalidPhoneNumberException("The phone format is not valid");
                }
            }
        }
    }
}
