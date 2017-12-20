namespace LocationExplorer.Web.Infrastructure.ViewModelConstants
{
    public static class CountryConstants
    {
        public const string CountryNameRegex = "[A-Z][a-z]+(\\s{1}[A-z][a-z]+){0,}";
        public const string CountryNameRegexErrorMessage = "Country name should contain one or more words starting a upper case letter. (ex.'New Zealand')";
    }
}
