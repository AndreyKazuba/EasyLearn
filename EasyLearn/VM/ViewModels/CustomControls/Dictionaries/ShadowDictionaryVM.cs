using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;

#pragma warning disable CS8618
namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class ShadowDictionaryVM : ViewModel
    {
        #region Commands
        public Command AddDictionaryCommand { get; private set; }
        protected override void InitCommands()
        {
            AddDictionaryCommand = new Command(AddDictionary);
        }
        private void AddDictionary() => App.GetService<DictionariesPageVM>().OpenAddingDictionaryWindowCommand.Execute();
        #endregion
    }
}
