using System;
using System.Collections.Generic;

namespace Crs.Backend.Data.Model
{
    public class Client
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly BirthDate { get; set; }

        public List<Ride> Rides { get; set; }
    }
}
