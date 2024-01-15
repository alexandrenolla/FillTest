// using System.ComponentModel.DataAnnotations;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;

// public class LoginModel : PageModel
// {
//     private readonly SignInManager<IdentityUser> _signInManager;

//     public LoginModel(SignInManager<IdentityUser> signInManager)
//     {
//         _signInManager = signInManager;
//     }

//     [BindProperty]
//     public InputModel Input { get; set; }

//     public class InputModel
//     {
//         [Required]
//         [EmailAddress]
//         public string Email { get; set; }

//         [Required]
//         [DataType(DataType.Password)]
//         public string Password { get; set; }
//     }

//     public async Task<IActionResult> OnPostAsync(string returnUrl = null)
//     {
//         returnUrl ??= Url.Content("~/");

//         if (ModelState.IsValid)
//         {
//             var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, false);

//             if (result.Succeeded)
//             {
//                 return LocalRedirect(returnUrl);
//             }
//             else
//             {
//                 ModelState.AddModelError(string.Empty, "Invalid login attempt.");
//                 return Page();
//             }
//         }

//         return Page();
//     }
// }
