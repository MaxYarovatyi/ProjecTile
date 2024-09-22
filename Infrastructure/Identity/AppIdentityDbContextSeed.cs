using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AccountUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AccountUser
                {
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}