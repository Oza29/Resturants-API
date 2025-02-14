namespace Domain.Entites
{
    public class Dish
    {
        public int ID { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int? KiloCalories { get; set; }
        public int RestaurantID { get; set; }

    }
}
