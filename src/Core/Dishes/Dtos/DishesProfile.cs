using AutoMapper;
using Core.Dishes.Command.CreateDish;
using Domain.Entites;

namespace Core.Dishes.Dtos
{
    public class DishesProfile:Profile
    {
        public DishesProfile()
        {
            CreateMap<CreateDishCommand, Dish>();
            CreateMap<Dish, DishDTO>();
        }
        
    }
}
