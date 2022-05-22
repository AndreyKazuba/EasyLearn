using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class ShadowDictionaryVM : ViewModel
    {
        public Command AddDictionaryCommand { get; private set; }
        protected override void InitCommands()
        {
            this.AddDictionaryCommand = new Command(AddDictionary);
        }
        private void AddDictionary() => App.GetService<DictionariesPageVM>().OpenAddingDictionaryWindowCommand.Execute();
    }
}
