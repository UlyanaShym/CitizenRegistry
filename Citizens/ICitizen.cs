using System;
using Citizens.Tests.Helpers;

namespace Citizens
{
    public interface ICitizen
    {
        string FirstName { get; }
        string LastName { get; }
        Gender Gender { get; }
        DateTime BirthDate { get; }
        string VatId { get; set; }
        object Clone();
    }
}