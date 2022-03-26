using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLearn.Infrastructure.ValidationRules
{
    public static class ValidationsPool
    {
        private static Dictionary<Guid, bool> commonRelationAddingWindowValidationRules = new Dictionary<Guid, bool>();

        public static Guid RegisterCommonRelationAddingWindowValidationRule(bool isPassed = false)
        {
            Guid guid = Guid.NewGuid();
            commonRelationAddingWindowValidationRules.Add(guid, isPassed);
            return guid;
        }
        public static void SetCommonRelationAddingWindowValidationRule(Guid guid, bool isPassed) => commonRelationAddingWindowValidationRules[guid] = isPassed;
        public static bool IsAddingCommonRelationWindowInvalid => commonRelationAddingWindowValidationRules.Any(validation => !validation.Value);
        
    }
}
