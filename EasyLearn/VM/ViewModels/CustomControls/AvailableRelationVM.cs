using EasyLearn.Data.Models;
using EasyLearn.VM.Core;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class AvailableRelationVM : ViewModel
    {
        public string English { get; set; }
        public string Russian { get; set; }
        public AvailableRelationVM(CommonRelation commonRelation)
        {
            this.English = commonRelation.EnglishUnit.Value;
            this.Russian = commonRelation.RussianUnit.Value;
        }
    }
}
