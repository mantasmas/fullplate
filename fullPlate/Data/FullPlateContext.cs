using fullPlate.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace fullPlate.Data
{
    public class FullPlateContext : IdentityDbContext<User>
    {
        public FullPlateContext(DbContextOptions<FullPlateContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>()
                .HasMany(x => x.Dishes)
                .WithOne(y => y.Restaurant)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LunchDay>()
                .Property(x => x.Enabled)
                .HasDefaultValue(false);

            modelBuilder.Entity<LunchDay>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.LunchDay);

            modelBuilder.Entity<LunchDish>()
                .HasKey(x => new {x.DishId, x.LunchDayId});

            modelBuilder.Entity<User>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.User);

            modelBuilder.Entity<User>()
                .Property(x => x.Disabled)
                .HasDefaultValue(false);

            modelBuilder.Entity<Order>()
                .HasKey(x => new { x.LunchId, x.SoupId, x.MainDishId, x.UserId });
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<LunchDay> LunchDays { get; set; }
        public DbSet<LunchDish> LunchDayDishes { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
