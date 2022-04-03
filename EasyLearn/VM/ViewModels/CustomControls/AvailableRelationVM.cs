using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.VM.Core;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class AvailableRelationVM : ViewModel
    {
        public string English { get; set; }
        public AvailableRelationVM(CommonRelation commonRelation)
        {
            this.English = commonRelation.EnglishUnit.Value.NormalizeRegister();
        }
    }
}
