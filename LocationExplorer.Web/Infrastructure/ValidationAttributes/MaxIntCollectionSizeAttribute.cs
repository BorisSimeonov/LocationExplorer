namespace LocationExplorer.Web.Infrastructure.ValidationAttributes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class MaxIntCollectionSizeAttribute : ValidationAttribute
    {
        private readonly int maxSize;

        public MaxIntCollectionSizeAttribute(int maxSize)
        {
            this.maxSize = maxSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IEnumerable<int> valueCasted && valueCasted.Count() > maxSize)
            {
                return new ValidationResult(string.Format(ErrorMessage, maxSize), 
                    new List<string>{ validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
