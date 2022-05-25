using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLearn.Infrastructure.Validation
{
    public static class ValidationPool
    {
        private static Dictionary<ValidationRulesGroup, Dictionary<Guid, bool>> validationRules;
        static ValidationPool()
        {
            validationRules = new Dictionary<ValidationRulesGroup, Dictionary<Guid, bool>>();
            validationRules.Add(ValidationRulesGroup.AddVerbPreposition, new Dictionary<Guid, bool>());
            validationRules.Add(ValidationRulesGroup.AddCommonRelation, new Dictionary<Guid, bool>());
            validationRules.Add(ValidationRulesGroup.AddNewUser, new Dictionary<Guid, bool>());
            validationRules.Add(ValidationRulesGroup.AddNewDictionary, new Dictionary<Guid, bool>());
            validationRules.Add(ValidationRulesGroup.UpdateCommonRelation, new Dictionary<Guid, bool>());
            validationRules.Add(ValidationRulesGroup.UpdateVerbPrepsotion, new Dictionary<Guid, bool>());
        }
        public static Guid Register(ValidationRulesGroup group, bool isPassed = false)
        {
            Guid guid = Guid.NewGuid();
            validationRules[group].Add(guid, isPassed);
            return guid;
        }
        public static void Set(ValidationRulesGroup group, Guid guid, bool isPassed) => validationRules[group][guid] = isPassed;
        public static bool IsValid(ValidationRulesGroup group) => !validationRules[group].Any(rule => !rule.Value);
    }
}
