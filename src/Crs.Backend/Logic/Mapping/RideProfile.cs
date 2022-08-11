using AutoMapper;
using AutoMapper.Extensions.EnumMapping;

namespace Crs.Backend.Logic.Mapping
{
    internal sealed class RideProfile : Profile
    {
        public RideProfile()
        {
            CreateMap<Data.Model.Ride, Model.Ride>();
            CreateMap<Data.Model.RideStatus, Model.RideStatus>().ConvertUsingEnumMapping().ReverseMap();
        }
    }
}
