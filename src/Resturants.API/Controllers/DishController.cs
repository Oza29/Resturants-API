using Core.Dishes.Command.CreateDish;
using Core.Dishes.Command.DeleteAllDishes;
using Core.Dishes.Dtos;
using Core.Dishes.Query;
using Core.Dishes.Query.GetAllDishesForRestaurantQuery;
using Core.Dishes.Query.GetDishForRestaurantQuery;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Resturants.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/dishes")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DishController(IMediator mediator)
        {
            _mediator= mediator;
        }
        [HttpPost]
        public async Task <IActionResult> CreateDish(int restaurantId, CreateDishCommand command)
        {
            command.RestaurantID = restaurantId;
         int dishId=  await _mediator.Send(command);
            return CreatedAtAction(nameof(GetDishByResturantId), new { restaurantId, dishId },null);
            
        }
        [HttpGet]
        public async Task <ActionResult<IEnumerable<DishDTO>>>GetAllDishes(int restaurantId)
        {
           var dishes = await _mediator.Send(new GetAllDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<IActionResult> GetDishByResturantId(int restaurantId,int dishId)
        {
           var dish= await _mediator.Send(new GetDishForRestaurantQuery(restaurantId,dishId));
            return Ok(dish);
        }

        [HttpDelete]
        public async Task<IActionResult>DeleteAllDishesFromResturant(int restaurantId)
        {
           await _mediator.Send(new DeleteAllDishesFromResturantCommand(restaurantId));
            return NoContent();
        }
    }
}
