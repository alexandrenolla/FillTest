// using Microsoft.AspNetCore.Identity;
// using Microsoft.Extensions.DependencyInjection;
// using System;
// using System.Threading.Tasks;

// public static class SeedData
// {
//     public static async Task EnsureSeedData(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager)
//     {
//         var user = await userManager.FindByEmailAsync("alexandrenolla@gmail.com");

//         if (user == null)
//         {
//             user = new IdentityUser
//             {
//                 UserName = "alexandrenolla@gmail.com",
//                 Email = "alexandrenolla@gmail.com",
//             };

//             await userManager.CreateAsync(user, "fullstack123");
//         }
//     }
// }
