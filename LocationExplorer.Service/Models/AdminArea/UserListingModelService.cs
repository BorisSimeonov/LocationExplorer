﻿namespace LocationExplorer.Service.Models.AdminArea
{
    using Common.Mapping;
    using Domain.Models;

    public class UserListingModelService : IMapFrom<User>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }
    }
}
