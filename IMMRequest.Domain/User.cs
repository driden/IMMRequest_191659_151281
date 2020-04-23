using IMMRequest.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace IMMRequest.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private string _email;
        public string Email /* Unique */
        {
            get => _email;
            set
            {   // Validate email format
                if(Regex.Match(value, @"^[^@]+@[^@]+\.[a-zA-Z]{2,}$").Success)
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

        public User()
        {

        }
    }
}
