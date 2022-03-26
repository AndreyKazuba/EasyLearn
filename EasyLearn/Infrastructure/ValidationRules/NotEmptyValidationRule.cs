using EasyLearn.Data.Helpers;
using System;
using System.Globalization;
using System.Windows.Controls;

namespace EasyLearn.Infrastructure.ValidationRules
{
    class NotEmptyValidationRule : ValidationRule
    {
        private Guid validationRuleId;
        public NotEmptyValidationRule()
        {
            this.validationRuleId  = ValidationsPool.RegisterCommonRelationAddingWindowValidationRule(false);
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is null || StringHelper.IsEmptyOrWhiteSpace((string)value))
            {
                ValidationsPool.SetCommonRelationAddingWindowValidationRule(validationRuleId, false);
                return new ValidationResult(false, "Поле необходимо заполнить");

            }
            else
            {
                ValidationsPool.SetCommonRelationAddingWindowValidationRule(validationRuleId, true);
                return ValidationResult.ValidResult;
            }
        }
    }
}
