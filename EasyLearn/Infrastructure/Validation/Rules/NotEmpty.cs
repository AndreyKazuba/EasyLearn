﻿using EasyLearn.Data.Helpers;
using System;
using System.Globalization;
using System.Windows.Controls;

namespace EasyLearn.Infrastructure.Validation
{
    class NotEmpty : ValidationRule
    {
        #region Private fields
        private Guid currentRuleId;
        private ValidationRulesGroup group;
        #endregion

        public ValidationRulesGroup Group
        {
            set
            {
                group = value;
                currentRuleId = ValidationPool.Register(value);
            }
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is null || StringHelper.IsEmptyOrWhiteSpace((string)value))
            {
                ValidationPool.Set(group, currentRuleId, false);
                return new ValidationResult(false, "Необходимо заполнить");
            }
            else
            {
                ValidationPool.Set(group, currentRuleId, true);
                return ValidationResult.ValidResult;
            }
        }
    }
}
