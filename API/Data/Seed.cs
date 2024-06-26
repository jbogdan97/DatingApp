using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, 
        RoleManager<AppRole> roleManager)
        {
            if(await userManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

var roles = new List<AppRole>
{
    new AppRole{Name = "Member"},
    new AppRole{Name = "Admin"},
    new AppRole{Name = "Moderator"}
};

foreach(var role in roles)
{
    await roleManager.CreateAsync(role);
}

            foreach(var user in users)
            {
                // using var hmac = new HMACSHA512();

                user.UserName =  user.UserName.ToLower();

               user.Photos.First().isApproved = true;

                // user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("parola"));
                // user.PasswordSalt = hmac.Key;

                // context.Users.Add(user);

    await userManager.CreateAsync(user, "Parola1");
    await userManager.AddToRoleAsync(user, "Member");
            }
            // await context.SaveChangesAsync();
        
var admin = new AppUser
{
    UserName = "admin"
};

await userManager.CreateAsync(admin, "Parola1");
await userManager.AddToRolesAsync(admin, new[] {"Admin", "Moderator"});

        }

    }
}