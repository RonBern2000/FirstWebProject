using Microsoft.AspNetCore.Identity;

namespace WebProject.Data
{
    public class SeedData
    {
        // Because we want to implement a more complex init we have to take it aside to a different class and not in OnModelCreating.
        public static async Task Initialize(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Create or find users
            var ron = await userManager.FindByNameAsync("Ron");
            if (ron == null)
            {
                ron = new IdentityUser { UserName = "Ron", Email = "ron@example.com"};
                await userManager.CreateAsync(ron, "Password123!");
            }
            await userManager.AddToRoleAsync(ron, "Visitor");

            var daniel = await userManager.FindByNameAsync("Daniel");
            if (daniel == null)
            {
                daniel = new IdentityUser { UserName = "Daniel", Email = "daniel@example.com" };
                await userManager.CreateAsync(daniel, "Password123456789!");
            }
            await userManager.AddToRoleAsync(daniel, "Visitor");

            var amir = await userManager.FindByNameAsync("Amir");
            if (amir == null)
            {
                amir = new IdentityUser { UserName = "Amir", Email = "amir@example.com" };
                await userManager.CreateAsync(amir, "Password321!");
            }
            await userManager.AddToRoleAsync(amir, "Admin");
        }
    }
}
