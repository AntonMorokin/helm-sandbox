using System;
using System.Collections.Generic;

namespace Crs.Backend.Model
{
    public class Client
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly BirdthDate { get; set; }

        public Client(string firstName, string lastName, DateOnly birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirdthDate = birthDate;
        }
    }
}
