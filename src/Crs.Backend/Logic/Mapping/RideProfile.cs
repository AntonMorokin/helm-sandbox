using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using System;
using System.Linq.Expressions;

namespace Crs.Backend.Logic.Mapping
{
    internal sealed class RideProfile : Profile
    {
        public RideProfile()
        {
            Expression<Func<DateTime?, DateTime?>> toUniversalDateTimeTransformer =
                x => x.HasValue && x.Value.Kind != DateTimeKind.Utc
                    ? x.Value.ToUniversalTime()
                    : x;

            CreateMap<Data.Model.Ride, Model.Ride>()
                .ReverseMap()
                .ForMember(x => x.StartTime, x => x.AddTransform(toUniversalDateTimeTransformer))
                .ForMember(x => x.EndTime, x => x.AddTransform(toUniversalDateTimeTransformer));

            CreateMap<Data.Model.RideStatus, Model.RideStatus>().ConvertUsingEnumMapping().ReverseMap();
        }
    }
}
