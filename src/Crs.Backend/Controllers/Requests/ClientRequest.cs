using System;

namespace Crs.Backend.Controllers.Requests
{
    public sealed class ClientRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirdthDate { get; set; }
    }
}
