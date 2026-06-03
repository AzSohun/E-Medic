using E_Medic.Data;
using E_Medic.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Medic.Services
{
    public class DbInitializer
    {
        public static async Task SeedDataAsync(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {

            string[] roles = { "Admin", "Doctor", "Patient" };
            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid> { Name = roleName, NormalizedName = roleName.ToUpper() });
                }
            }

            if (context.Doctors.Any()) return;

            var docUser1 = new User
            {
                FullName = "Dr. Asif Rahman",
                UserName = "asif@emedic.com",
                Email = "asif@emedic.com",
                PhoneNumber = "01711223344",
                Gender = "Male",
                Role = "Doctor",
                IsEmailVerified = true,
                EmailConfirmed = true
            };
            if (await userManager.FindByEmailAsync(docUser1.Email) == null)
            {
                await userManager.CreateAsync(docUser1, "Doctor@123");
                await userManager.AddToRoleAsync(docUser1, "Doctor");

                context.Doctors.Add(new Doctor
                {
                    UserId = docUser1.Id,
                    Specialty = "General Medicine",
                    Qualifications = "MBBS, BCS (Health)",
                    AvailableHours = "04:00 PM - 07:00 PM",
                    IsApprovedByAdmin = true,
                    AverageRating = 4.9m,
                    TotalRatingsCount = 120
                });
            }

            var docUser2 = new User
            {
                FullName = "Dr. Sarah Jenkins",
                UserName = "sarah@emedic.com",
                Email = "sarah@emedic.com",
                PhoneNumber = "01811223344",
                Gender = "Female",
                Role = "Doctor",
                IsEmailVerified = true,
                EmailConfirmed = true
            };
            if (await userManager.FindByEmailAsync(docUser2.Email) == null)
            {
                await userManager.CreateAsync(docUser2, "Doctor@123");
                await userManager.AddToRoleAsync(docUser2, "Doctor");

                context.Doctors.Add(new Doctor
                {
                    UserId = docUser2.Id,
                    Specialty = "Pediatrics",
                    Qualifications = "MBBS, FCPS",
                    AvailableHours = "06:00 PM - 09:00 PM",
                    IsApprovedByAdmin = true,
                    AverageRating = 4.8m,
                    TotalRatingsCount = 95
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
