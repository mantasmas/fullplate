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
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Dish> Dishes { get; set; }
    }
}
