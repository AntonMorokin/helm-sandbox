using AutoMapper;

namespace Crs.Backend.Logic.Mapping
{
    internal sealed class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Data.Model.Client, Model.Client>();
        }
    }
}
