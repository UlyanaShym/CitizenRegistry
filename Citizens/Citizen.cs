using System;
using System.Linq;

namespace Citizens.Tests.Helpers
{
    public class Citizen : ICitizen
    {
        public string FirstName { get; }
        public string LastName { get; }
        public Gender Gender { get; }
        public DateTime BirthDate { get; }
        public string VatId { get; set; }

        public Citizen(string firstName, string lastName, DateTime dateOfBirth, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            if (dateOfBirth > DateTime.Now)
                throw new ArgumentException();
            BirthDate = dateOfBirth;
            if (gender > Enum.GetValues(typeof(Gender)).Cast<Gender>().Max())
                throw new ArgumentOutOfRangeException();
            Gender = gender;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}