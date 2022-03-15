using AutoMapper;
using Stonks.Models;

namespace Stonks.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AggregateBarResult, StockPrice>()             
                .ForMember(dest => dest.TradingVolume, opt => opt.MapFrom(src => src.v))
                .ForMember(dest => dest.VolumeWeightedAveragePrice, opt => opt.MapFrom(src => src.vw))
                .ForMember(dest => dest.OpenPrice, opt => opt.MapFrom(src => src.o))
                .ForMember(dest => dest.ClosePrice, opt => opt.MapFrom(src => src.c))
                .ForMember(dest => dest.HighestPrice, opt => opt.MapFrom(src => src.h))
                .ForMember(dest => dest.LowestPrice, opt => opt.MapFrom(src => src.l))
                .ForMember(dest => dest.NumberOfTransactions, opt => opt.MapFrom(src => src.n))
                .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.d));
        }
    }
}
