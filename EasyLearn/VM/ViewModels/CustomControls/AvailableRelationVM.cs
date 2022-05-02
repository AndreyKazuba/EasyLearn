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
            if (direction == DictationDirection.Directly)
                this.Value = commonRelation.EnglishUnit.Value.NormalizeRegister();
            else
                this.Value = commonRelation.RussianUnit.Value.NormalizeRegister();
        }
    }
}
