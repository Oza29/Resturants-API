using Domain.Constants;
using Domain.Entites;
using InfraStructure.AppDbContext;
using Microsoft.AspNetCore.Identity;


namespace InfraStructure.Seeders
{
    internal class RestaurantSeeder(ApplicationDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    await dbContext.AddRangeAsync(GetRestaurants());
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.Roles.Any())
                {
                    await dbContext.AddRangeAsync(GetRoles());
                    await dbContext.SaveChangesAsync();
                }
            }
        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = [
            new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new ()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M,
                    },
                    new ()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                ],
                Address = new ()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                }
            },
            new ()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new Address()
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                }
            }
        ];
            return restaurants;
        }
        public IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> Roles = [
                new(UserRoles.User){
                    NormalizedName=UserRoles.User.ToUpper()
                },
                new(UserRoles.Admin){
                    NormalizedName=UserRoles.Admin.ToUpper()
                },
                new(UserRoles.Owner){
                    NormalizedName = UserRoles.Owner.ToUpper() 
                }
                ];
            return Roles;
        }

    }
}
