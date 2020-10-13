using LeaveManager.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace LeaveManager
{
    public static class SeedData
    {
        public static void Seed(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManger)
        {
            SeedUsers(userManager);
            SeedRoles(roleManger);
        }

        private static void SeedUsers(UserManager<Employee> userManager)
        {
            if (!userManager.Users.Any()) {
                var adminUser = new Employee
                {
                    UserName = "admin@localhost.com",
                    Email = "admin@localhost.com"
                };
                var status = userManager.CreateAsync(adminUser).Result;
                if (status.Succeeded) {
                    userManager.AddToRoleAsync(adminUser, "Administrator").Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var newRole = new IdentityRole
                {
                    Name = "Administrator"
                };
                roleManager.CreateAsync(newRole).Wait();
            }

            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                var newRole = new IdentityRole
                {
                    Name = "Administrator"
                };
                roleManager.CreateAsync(newRole).Wait();
            }
        }
    }
}
