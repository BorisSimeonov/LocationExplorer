namespace LocationExplorer.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using BaseControllers;
    using Domain.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Service.Interfaces.AdminArea;
    using ViewModels;
    using static Data.Infrastructure.DataConstants;
    using static Infrastructure.WebConstants;

    public class UsersController : BaseAdminController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IAdminService adminService;

        public UsersController(
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAdminService adminService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int page = 1)
            => View(await adminService.AllUsersAsync(page));

        [HttpGet]
        public async Task<IActionResult> EditRoles(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound($"No user found by id '{id}'.");
            }

            var userRoles = await userManager.GetRolesAsync(user);
            var allRoles = await roleManager.Roles
                .OrderBy(x => x.Name)
                .Select(r => new SelectListItem { Value = r.Id, Text = r.Name })
                .ToListAsync();

            return View(new UserRolesEditViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(UserRoleRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectOnInvalidStateInRoleEdit(model.UserId);
            }

            var actionResult = await ChangeRole(model);

            if (actionResult != null)
            {
                return actionResult;
            }

            return RedirectToAction(nameof(EditRoles), new { Id = model.UserId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(UserRoleRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectOnInvalidStateInRoleEdit(model.UserId);
            }

            var actionResult = await ChangeRole(model, true);

            if (actionResult != null)
            {
                return actionResult;
            }

            return RedirectToAction(nameof(EditRoles), new { Id = model.UserId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                TempData.AddErrorMessage("Invalid User.");
                return RedirectToAction(nameof(All));
            }

            var isAdministrator = await userManager.IsInRoleAsync(user, AdministratorRole);

            var currentUserId = userManager.GetUserId(User);

            if (currentUserId == user.Id)
            {
                TempData.AddErrorMessage("Cannot delete yourself. :)");
            }
            else if (isAdministrator)
            {
                TempData.AddErrorMessage(DeleteAdministratorErrorMessage);
                return RedirectToAction(nameof(All));
            }
            else
            {
                var success = await adminService.DeleteUserAsync(user.Id);
                if (!success)
                {
                    TempData.AddErrorMessage();
                }
                else
                {
                    TempData.AddSuccessMessage();
                }
            }

            return RedirectToAction(nameof(All));
        }

        private async Task RefreshCurrentUserCookie(string userId)
        {
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser.Id == userId)
            {
                await signInManager.RefreshSignInAsync(currentUser);
            }
        }

        private IActionResult RedirectOnInvalidStateInRoleEdit(string userId)
        {
            var error = ModelState.SelectMany(x => x.Value.Errors).FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.ErrorMessage))?.ErrorMessage;

            TempData.AddErrorMessage($"Operation Failed. {error}".Trim());

            if (string.IsNullOrWhiteSpace(userId))
            {
                return RedirectToAction(nameof(All), "Users");
            }

            return RedirectToAction(nameof(EditRoles), new { Id = userId });
        }

        private async Task<IActionResult> ChangeRole(UserRoleRequestModel model, bool removeRole = false)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                TempData.AddErrorMessage("Invalid user Id.");
                return RedirectToAction(nameof(All), "Users");
            }

            var role = await roleManager.FindByIdAsync(model.Role);

            if (role == null)
            {
                TempData.AddErrorMessage("Invalid role Id.");
                return RedirectToAction(nameof(EditRoles), new { Id = model.UserId });
            }

            var result = removeRole ? 
                await userManager.RemoveFromRoleAsync(user, role.Name) 
                : await userManager.AddToRoleAsync(user, role.Name);

            if (!result.Succeeded)
            {
                TempData.AddErrorMessage(result.Errors.FirstOrDefault()?.Description ?? "Something went wrong.");
            }

            await RefreshCurrentUserCookie(model.UserId);
            TempData.AddSuccessMessage();

            return null;
        }
    }
}
