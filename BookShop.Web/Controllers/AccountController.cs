using EBook.Domain;

using EBook.Domain.Identity;

using EBook.Domain.DTO;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EBook.Service.Interface;

namespace EBook.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<EShopAppUser> userManager;
        private readonly SignInManager<EShopAppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserService userService;

        public AccountController(UserManager<EShopAppUser> userManager, SignInManager<EShopAppUser> signInManager, RoleManager<IdentityRole> roleManager, IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.userService = userService;
        }

        public IActionResult Register()
        {
            UserRegistrationDto model = new UserRegistrationDto();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await userManager.FindByEmailAsync(request.Email);
                if (userCheck == null)
                {
                    var user = new EShopAppUser
                    {
                        UserName = request.Email,
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        UserCart = new ShoppingCart()
                    };


                    var result = await userManager.CreateAsync(user, request.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, RoleName.Standard_User);
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(request);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(request);
                }
            }
            return View(request);

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            UserLoginDto model = new UserLoginDto();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(model);

                }
                if (await userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    return RedirectToAction("Index", "Books");
                }
                else if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }       


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ShowUsers()
        {
            string activeUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<UserDto> users = userService.GetAllUsers().Where(user => !user.Id.Equals(activeUserId)).ToList().ConvertAll(new Converter<EShopAppUser, UserDto>(user => new UserDto() { Id = user.Id, Email = user.Email }));
            return View(users);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;

            var user = await userManager.FindByIdAsync(userId);
            string activeUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with ID = {userId} cannot be found.";
                return View();
            }
            if (user.Id.Equals(activeUserId))
            {
                ViewBag.ErrorMessage = $"This is you!";
                return View();
            }

            var model = new List<UserRolesDto>();
            foreach (var role in roleManager.Roles)
            {
                var userRole = new UserRolesDto
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRole.IsSelected = true;
                }
                else
                {
                    userRole.IsSelected = false;
                }
                model.Add(userRole);
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesDto> model, string userId)
        {
            ViewBag.userId = userId;
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with ID = {userId} cannot be found.";
                return View("NotFound");
            }

            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles.");
                return View(model);
            }

            result = await userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user.");
                return View(model);
            }
            return RedirectToAction("Index", "Books");
        }



    }
}