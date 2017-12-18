namespace LocationExplorer.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using BaseControllers;
    using Domain.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class UsersController : BaseAdminController
    {
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly UserManager<User> UserManager;

        public UsersController(
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            //var users = await Service.AllAsync();
            var roles = await RoleManager.Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();
            return View();
            //return View(new AdminUserListingsViewModel { Users = users, Roles = roles });
        }

        //[HttpPost]
        //public async Task<IActionResult> AddToRole(AddUserToRoleFormModel model)
        //{
        //    var roleExists = await RoleManager.RoleExistsAsync(model.Role);
        //    var student = await UserManager.FindByIdAsync(model.UserId);

        //    if (!roleExists || student == null)
        //    {
        //        ModelState.AddModelError(string.Empty, "Invalid identity details.");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction(nameof(All));
        //    }

        //    await UserManager.AddToRoleAsync(student, model.Role);

        //    TempData.AddSuccessMessage($"User '{student.UserName}' successfully added to role '{model.Role}'.");
        //    return RedirectToAction(nameof(All));
        //}
    }
}
