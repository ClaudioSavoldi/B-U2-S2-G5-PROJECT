using B_U2_S2_G5_PROJECT.Models.Entity;
using B_U2_S2_G5_PROJECT.Models.Dto;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

// fare primo user con
// amministratore@admin.com

namespace B_U2_S2_G5_PROJECT.Controllers
{

    [Authorize]
    public class AspNetUserController : Controller
    {

        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AspNetUserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }

        [AllowAnonymous]
        public IActionResult Login()
        {

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginSave(Models.Dto.LoginRequest loginRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  
                    ApplicationUser user = await this._userManager.FindByNameAsync(loginRequest.Email); 

                    if (user is not null)
                    {
                       
                        Microsoft.AspNetCore.Identity.SignInResult result = await this._signInManager.PasswordSignInAsync(
                            user,
                            loginRequest.Password,
                            isPersistent: false,
                            lockoutOnFailure: false

                            );
                        if (result.Succeeded)
                        {
                      
                            return RedirectToAction("Index", "Home");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return View("Login");
        }

       

        [AllowAnonymous]
        public IActionResult Register()
        {
            
            return View();

        }
   
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Models.Dto.RegisterRequest registerRequest)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                
                    ApplicationUser user = new ApplicationUser()
                    {
                        UserName = registerRequest.Email,
                        Email = registerRequest.Email,
                        Name=registerRequest.Name,
                        Surname = registerRequest.Surname,
                        PhoneNumber = registerRequest.PhoneNumber,
                        CreatedAt = DateTime.Now,
                        Birthday = registerRequest.Birthday,
                        Id = Guid.NewGuid().ToString(),
                        IsDeleted = false,
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                      
                    };
                   
                    IdentityResult result = await _userManager.CreateAsync(user, registerRequest.Password);
                    if (result.Succeeded)
                    {
                        
                        var roleExist = await this._roleManager.RoleExistsAsync("User");


                        if (!roleExist)
                        {
                            
                            await this._roleManager.CreateAsync(new IdentityRole("User"));
                            await this._roleManager.CreateAsync(new IdentityRole("Admin"));
                            await this._roleManager.CreateAsync(new IdentityRole("Receptionsit"));

                        }
                       
                        if (registerRequest.Email == "amministratore@admin.com")
                        {
                            await this._userManager.AddToRoleAsync(user, "Admin");
                        }
                        else
                        { 
                            await this._userManager.AddToRoleAsync(user, "User");
                        }
                        return RedirectToAction("Login");

                    }
                    else
                    {

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }
            return View("Register");
        }



        //TENTATIVO DI LOGOUT
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Home","Index");
        //}
    }

}