using System;

namespace Crs.Backend.Host.Controllers.Responses
{
    public sealed class ClientResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
