namespace LocationExplorer.Web.Infrastructure.ValidationAttributes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class MaxCollectionSizeAttribute : ValidationAttribute
    {
        private int maxSize;

        public MaxCollectionSizeAttribute(int maxSize)
        {
            this.maxSize = maxSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var collection = validationContext.ObjectInstance as IEnumerable<object>;

            if (collection != null && collection.Count() > maxSize)
            {
                return new ValidationResult(ErrorMessageString);
            }

            return ValidationResult.Success;
        }
    }
}
