using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace fullPlate.Data
{
    public class DataSeeder
    {
        private readonly FullPlateContext _dbContext;
        private readonly UserManager<User> _userManager;

        public DataSeeder(FullPlateContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task SeedRoles()
        {
            var roleStore = new RoleStore<IdentityRole>(_dbContext);

            if (!_dbContext.Roles.Any(r => r.Name == "ADMIN"))
            {
                Console.WriteLine("lel plef");
                await roleStore.CreateAsync(new IdentityRole { Name = "ADMIN", NormalizedName = "ADMIN" });
            }

            if (!_dbContext.Roles.Any(r => r.Name == "EMPLOYEE"))
            {
                Console.WriteLine("lel kek");
                await roleStore.CreateAsync(new IdentityRole { Name = "EMPLOYEE", NormalizedName = "EMPLOYEE" });
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task SeedUsers()
        {
            if (!_dbContext.Users.Any(r => r.UserName == "admin"))
            {
                User user = new User
                {
                    Email = "adminMail@fullPlate.ne",
                    UserName = "admin",
                    FirstName = "admin",
                    LastName = "admin"
                };

                var adminPass = "admin";

                await _userManager.CreateAsync(user, adminPass);

                User usr = await _userManager.FindByNameAsync(user.UserName);
                await _userManager.AddToRoleAsync(usr, "ADMIN");
            }

            if (!_dbContext.Users.Any(r => r.UserName == "jonasp"))
            {
                User user = new User
                {
                    Email = "jonasp@fullPlate.ne",
                    UserName = "jonasp",
                    FirstName = "Jonas",
                    LastName = "Petraitis"
                };

                var userPass = "user";

                await _userManager.CreateAsync(user, userPass);

                User usr = await _userManager.FindByNameAsync(user.UserName);
                await _userManager.AddToRoleAsync(usr, "EMPLOYEE");
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
