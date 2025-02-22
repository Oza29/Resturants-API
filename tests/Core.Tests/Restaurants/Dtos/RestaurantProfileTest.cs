using AutoMapper;
using Core.Restaurants.Dtos;
using Domain.Entites;
using FluentAssertions;

namespace Core.Tests.Restaurants.Dtos
{
    public class RestaurantProfileTest
    {
        [Fact]
        public void CreateMap_ForRestaurantToRestaurantDto_ShouldMapCorrectly()
        {
            //arrange 
            var configuration = new MapperConfiguration(ctg =>
            {
                ctg.AddProfile<RestaurantsProfile>();
            });
            var mapper = configuration.CreateMapper();
             var restaurant = new Restaurant
            {
                ID = 1,
                Name = "Test",
                Description = "this is a description",
                HasDelivery = true,
                ContactEmail = "test@example.com",
                Category = "test category",
                ContactNumber="123456789",
                Address = new Address
                {
                    City = "cairo",
                    Street = "307",
                    PostalCode = "11511"
                }
            };
            //act
         var restaurantDto=   mapper.Map<RestaurantDTO>(restaurant);
            //assert
            restaurantDto.Should().NotBeNull();
            restaurantDto.ID.Should().Be(restaurant.ID);
            restaurantDto.Name.Should().Be(restaurant.Name);
            restaurantDto.Street.Should().Be(restaurant.Address.Street);
            restaurantDto.City.Should().Be(restaurant.Address.City);
            restaurantDto.Category.Should().Be(restaurant.Category);
            restaurantDto.Description.Should().Be(restaurant.Description);
            restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);
        }
    }
}
