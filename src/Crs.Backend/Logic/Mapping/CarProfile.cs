using AutoMapper;

namespace Crs.Backend.Logic.Mapping
{
    internal sealed class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Data.Model.Car, Model.Car>().ReverseMap();
        }
    }
}
