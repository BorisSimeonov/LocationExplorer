namespace LocationExplorer.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Domain.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Manage;

    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly UserManager<User> userManager;

        public ManageController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            if (model.FirstName != user.FirstName)
            {
                user.FirstName = model.FirstName;
                var saveResult = await userManager.UpdateAsync(user);
                if (!saveResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting first name for user with ID '{user.Id}'.");
                }
            }

            if (model.LastName != user.LastName)
            {
                user.LastName = model.LastName;
                var saveResult = await userManager.UpdateAsync(user);
                if (!saveResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting last name for user with ID '{user.Id}'.");
                }
            }

            if (model.Birthday != user.Birthday)
            {
                user.Birthday = model.Birthday;
                var saveResult = await userManager.UpdateAsync(user);
                if (!saveResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting birthday for user with ID '{user.Id}'.");
                }
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Index));
        }
    }
}
