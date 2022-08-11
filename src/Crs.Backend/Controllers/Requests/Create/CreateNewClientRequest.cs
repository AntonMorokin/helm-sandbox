using System;

namespace Crs.Backend.Controllers.Requests.Create
{
    public sealed class CreateNewClientRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirdthDate { get; set; }
    }
}
