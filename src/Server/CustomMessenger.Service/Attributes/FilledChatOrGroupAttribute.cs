using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMessenger.Service.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FilledChatOrGroupAttribute : ValidationAttribute
    {
        public FilledChatOrGroupAttribute()
        {
            ErrorMessage = "Either ChatId or GroupId must be filled.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult(ErrorMessage);

            var chatIdProperty = validationContext.ObjectType.GetProperty("ChatId");
            var groupIdProperty = validationContext.ObjectType.GetProperty("GroupId");

            if (chatIdProperty == null || groupIdProperty == null)
                throw new ArgumentException("Properties 'ChatId' or 'GroupId' not found.");

            var chatIdValue = chatIdProperty.GetValue(validationContext.ObjectInstance) as Guid?;
            var groupIdValue = groupIdProperty.GetValue(validationContext.ObjectInstance) as Guid?;

            // Check if at least one of the properties is filled
            if (chatIdValue.HasValue || groupIdValue.HasValue)
                return ValidationResult.Success;

            return new ValidationResult(ErrorMessage);
        }
    }
}
