using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class ShadowVerbPrepositionVM : ViewModel
    {
        public Command AddVerbPrepositionCommand { get; private set; }
        protected override void InitCommands()
        {
            this.AddVerbPrepositionCommand = new Command(AddVerbPreposition);
        }
        public void AddVerbPreposition() => App.GetService<EditVerbPrepositionDictionaryPageVM>().OpenNewVerbPrepositionAddingWindowCommand.Execute();
    }
}
