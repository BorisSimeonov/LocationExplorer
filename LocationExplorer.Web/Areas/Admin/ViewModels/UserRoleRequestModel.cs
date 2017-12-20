namespace LocationExplorer.Web.Areas.Admin.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserRoleRequestModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
