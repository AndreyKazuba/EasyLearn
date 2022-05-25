using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.Enums;
using EasyLearn.VM.Core;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class AvailableRelationVM : ViewModel
    {
        public string Value { get; set; }
        public AvailableRelationVM(CommonRelation commonRelation, DictationDirection direction)
        {
            Value = direction == DictationDirection.Directly 
                ? commonRelation.EnglishUnit.Value.NormalizeRegister() 
                : commonRelation.RussianUnit.Value.NormalizeRegister();
        }
    }
}
