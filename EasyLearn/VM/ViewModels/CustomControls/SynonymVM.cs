using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.Enums;
using EasyLearn.VM.Core;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class SynonymVM : ViewModel
    {
        #region Binding props
        public string Value { get; set; }
        #endregion

        public SynonymVM(CommonRelation commonRelation, DictationDirection direction)
        {
            Value = direction == DictationDirection.Directly
                ? commonRelation.EnglishUnit.Value.NormalizeRegister()
                : commonRelation.RussianUnit.Value.NormalizeRegister();
        }
    }
}
