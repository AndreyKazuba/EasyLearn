using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;

#pragma warning disable CS8618
namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class ShadowCommonRelationVM : ViewModel
    {
        #region Commands
        public Command AddRelationCommand { get; private set; }
        protected override void InitCommands()
        {
            AddRelationCommand = new Command(AddRelation);
        }
        private void AddRelation() => App.GetService<EditCommonDictionaryPageVM>().AwOpenWindowCommand.Execute();
        #endregion
    }
}
