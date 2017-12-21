namespace LocationExplorer.Web.Infrastructure
{
    public static class WebConstants
    {
        public const string TempDataSuccessMessageKey = "SuccessMessage";
        public const string TempDataErrorMessageKey = "ErrorMessage";

        public const string AdministrationArea = "Admin";
        public const string WriterArea = "Writer";

        public const int DestinationMaxTagCount = 10;
        public const string InvalidTagsCountErrorMessage = "Selected tags for a destination cannot be more that {0}.";
    }
}
