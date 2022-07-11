using AutoMapper;
using HotelListing.Data;
using HotelListing.Models;

namespace HotelListing.Mappers
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
            CreateMap<ApiUser, ApiUserDTO>().ReverseMap();
        }
    }
}
