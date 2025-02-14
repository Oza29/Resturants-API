using AutoMapper;
using Core.Restaurants.Commands.CreateRestaurant;
using Core.Restaurants.Commands.UpdateRestaurant;
using Domain.Entites;
using System.Xml.Serialization;

namespace Core.Restaurants.Dtos
{
    public class RestaurantsProfile:Profile
    {
        public RestaurantsProfile()
        {
            CreateMap<UpdateRestaurantCommand, Restaurant>();

            CreateMap<CreateRestaurantCommand, Restaurant>().ForMember(m => m.Address, o => o.MapFrom(
                src => new Address
                {
                    Street = src.Street,
                    City = src.City,
                    PostalCode = src.PostalCode
                }));


            CreateMap<Restaurant, RestaurantDTO>()
            .ForMember(m => m.City,
                o => o.MapFrom(src => src.Address == null ? null : src.Address.City)).
            ForMember(m => m.Street,
                o => o.MapFrom(src => src.Address == null ? null : src.Address.Street)).
            ForMember(m => m.PostalCode,
                o => o.MapFrom(src => src.Address == null ? null:src.Address.PostalCode)).
             ForMember(m=>m.Dishes,
                o=>o.MapFrom(src=>src.Dishes)); 
            

            

        }

    }
}
