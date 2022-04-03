using System;
using System.Globalization;
using System.Windows.Controls;

namespace EasyLearn.Infrastructure.Validation
{
    public class InRange : ValidationRule
    {
        private Guid currentRuleId;
        private ValidationRulesGroup group;
        public int Min { get; set; }
        public int Max { get; set; }
        public ValidationRulesGroup Group
        {
            set
            {
                this.group = value;
                this.currentRuleId = ValidationPool.Register(value);
            }
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is null)
                return ValidationResult.ValidResult;
            string @string = (string)value;
            if (@string.Length < Min)
            {
                ValidationPool.Set(group, currentRuleId, false);
                return new ValidationResult(false, $"Минимум {Min} {ValidationHelper.GetCharactersSubString(Min)}");
            }
            if (@string.Length > Max)
            {
                ValidationPool.Set(group, currentRuleId, false);
                return new ValidationResult(false, $"Максимум {Max} {ValidationHelper.GetCharactersSubString(Max)}");
            }
            else
            {
                ValidationPool.Set(group, currentRuleId, true);
                return ValidationResult.ValidResult;
            }
        }
    }
}
