namespace LocationExplorer.Service.Models.AdminArea
{
    using System.Collections.Generic;
    using Infrastructure;

    public class PagedUserListingServiceModel
    {
        public IEnumerable<UserListingModelService> Users { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
