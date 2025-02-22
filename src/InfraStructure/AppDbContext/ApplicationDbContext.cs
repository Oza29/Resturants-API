using Domain.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace InfraStructure.AppDbContext
{
        internal class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Restaurant>().OwnsOne(n => n.Address);
            modelBuilder.Entity<Restaurant>().HasMany(n => n.Dishes).WithOne().HasForeignKey(n => n.RestaurantID);

            modelBuilder.Entity<User>().HasMany(o=>o.OwnedRestaurants).WithOne(r=>r.Owner).HasForeignKey(d=>d.OwnerId);   
        }
        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

    }
}
