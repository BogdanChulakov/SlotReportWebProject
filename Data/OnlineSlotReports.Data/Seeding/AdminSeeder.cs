﻿namespace OnlineSlotReports.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.VisualStudio.Services.Account;
    using OnlineSlotReports.Common;
    using OnlineSlotReports.Data.Models;

    internal class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var admin = await userManager.FindByNameAsync("admin@admin.admin");

            if (admin == null)
            {
                IdentityResult result = await userManager.CreateAsync(
                   new ApplicationUser
                   {
                       UserName = "admin@admin.admin",
                       Email = "admin@admin.admin",
                       EmailConfirmed = true,
                   }, "admin1111");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }

            var user = await userManager.FindByNameAsync("admin@admin.admin");

            await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
        }
    }
}
