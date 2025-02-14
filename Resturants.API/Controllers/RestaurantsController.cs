using Core.Restaurants.Commands.CreateRestaurant;
using Core.Restaurants.Commands.DeleteRestaurant;
using Core.Restaurants.Commands.UpdateRestaurant;
using Core.Restaurants.Dtos;
using Core.Restaurants.Query.GetAllRestaurants;
using Core.Restaurants.Query.GetRestaurantByID;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Resturants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RestaurantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetAll(GetAllRestaurantQuery query)
        {
            var restaurants = await _mediator.Send(query);
            return Ok(restaurants);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDTO>> GetById(int id)
        {

            RestaurantDTO? restaurant = await _mediator.Send(new GetRestaurantByIdQuery(id));
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }
        [HttpPost]
        public async Task<IActionResult> AddRestaurant(CreateRestaurantCommand restuarant)
        {
            
            int id = await _mediator.Send(restuarant);
            return CreatedAtAction(nameof(GetById), new { ID = id }, null);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            await _mediator.Send(new DeleteRestaurantCommand(id));
           
                return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id ,UpdateRestaurantCommand updateRestaurant)
        {
            updateRestaurant.ID = id;
           await _mediator.Send(updateRestaurant);
          
                return NoContent();
        }


    }
}
