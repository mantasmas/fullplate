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

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
