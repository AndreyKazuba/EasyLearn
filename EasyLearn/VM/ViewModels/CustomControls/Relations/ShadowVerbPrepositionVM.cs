using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;

#pragma warning disable CS8618
namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class ShadowVerbPrepositionVM : ViewModel
    {
        #region Commands
        public Command AddVerbPrepositionCommand { get; private set; }
        protected override void InitCommands()
        {
            AddVerbPrepositionCommand = new Command(AddVerbPreposition);
        }
        public void AddVerbPreposition() => App.GetService<EditVerbPrepositionDictionaryPageVM>().AwOpenWindowCommand.Execute();
        #endregion
    }
}
