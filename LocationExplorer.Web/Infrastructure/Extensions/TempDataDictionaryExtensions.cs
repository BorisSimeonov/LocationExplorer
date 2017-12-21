namespace LocationExplorer.Web.Infrastructure.Extensions
{
    using LocationExplorer.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public static class TempDataDictionaryExtensions
    {
        private const string DefaultSuccessMessage = "Operation succeeded.";
        private const string DefaultErrorMessage = "Operation failed.";

        public static void AddSuccessMessage(this ITempDataDictionary tempData, string message = DefaultSuccessMessage)
        {
            tempData[WebConstants.TempDataSuccessMessageKey] = message;
        }

        public static void AddErrorMessage(this ITempDataDictionary tempData, string message = DefaultErrorMessage)
        {
            tempData[WebConstants.TempDataErrorMessageKey] = message;
        }
    }
}
