using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class ShadowCommonRelationVM : ViewModel
    {
        public Command AddRelationCommand { get; private set; }
        protected override void InitCommands()
        {
            this.AddRelationCommand = new Command(AddRelation);
        }
        private void AddRelation() => App.GetService<EditCommonDictionaryPageVM>().AwOpenWindowCommand.Execute();
    }
}
