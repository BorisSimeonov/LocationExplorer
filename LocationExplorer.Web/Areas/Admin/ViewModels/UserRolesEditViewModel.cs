using System.ComponentModel.DataAnnotations;

namespace LocationExplorer.Web.Areas.Admin.ViewModels
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class UserRolesEditViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public IEnumerable<string> UserRoles { get; set; }

        public IEnumerable<SelectListItem> AllRoles { get; set; }

        [Display(Name = "Select Role:")]
        public SelectListItem SelectedRole { get; set; }
    }
}
