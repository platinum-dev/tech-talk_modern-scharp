using System;

namespace ModernCsharp
{
    public class Person
    {
        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value ??
                    throw new ArgumentNullException(nameof(value), "Cannon set name to null");
            }
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value ?? throw new ArgumentNullException(nameof(value), "Cannon set last name to null");
            }
        }

        public Person(string firstName, string lastName)
        {
            _firstName = firstName ?? throw new ArgumentNullException(nameof(firstName),
                    "Name should not be null");
            _lastName = lastName ?? throw new ArgumentNullException(nameof(firstName),
                    "Last name should not be null");
        }

        public string HyphenateForPartner(Person partner)
        {
            _ = partner ??
                throw new ArgumentNullException(nameof(partner),
                "Partner should not be null");
            return $"{partner.LastName} - {this.LastName}";
        }
    }
}
