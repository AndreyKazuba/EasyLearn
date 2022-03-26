using System;
using System.Globalization;
using System.Windows.Controls;

namespace EasyLearn.Infrastructure.ValidationRules
{
    public class MinAndMaxValidationRule : ValidationRule
    {
        private Guid validationRuleId;
        public int Min { get; set; }
        public int Max { get; set; }
        public MinAndMaxValidationRule()
        {
            this.validationRuleId = ValidationsPool.RegisterCommonRelationAddingWindowValidationRule(false);
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is null)
                return ValidationResult.ValidResult;
            string @string = (string)value;
            if (@string.Length < Min)
            {
                ValidationsPool.SetCommonRelationAddingWindowValidationRule(validationRuleId, false);
                return new ValidationResult(false, $"Поле должно содержать минимум {Min} {ValidationHelper.GetCharactersSubString(Min)}");
            }
            if (@string.Length > Max)
            {
                ValidationsPool.SetCommonRelationAddingWindowValidationRule(validationRuleId, false);
                return new ValidationResult(false, $"Поле может содержать максимум {Max} {ValidationHelper.GetCharactersSubString(Max)}");
            }
            else
            {
                ValidationsPool.SetCommonRelationAddingWindowValidationRule(validationRuleId, true);
                return ValidationResult.ValidResult;
            }
        }
    }
}
