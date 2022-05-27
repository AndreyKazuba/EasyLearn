using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class ExampleVM : ViewModel
    {
        #region Public props
        public int Id { get; private set; }
        public string RussianValue { get; private set; }
        public string EnglishValue { get; private set; }
        #endregion

        #region Binding props
        public string Content => $"{RussianValue} - {EnglishValue}";
        #endregion

#pragma warning disable CS8618
        public ExampleVM(string russianValue, string englishValue, int exampleId)
        {
            this.Id = exampleId;
            this.RussianValue = russianValue;
            this.EnglishValue = englishValue;
        }
#pragma warning restore CS8618

        #region Commands
        public Command RemoveCommand { get; private set; }
        protected override void InitCommands()
        {
            RemoveCommand = new Command(Remove);
        }
        private void Remove()
        {
            App.GetService<EditVerbPrepositionDictionaryPageVM>().RemoveExampleViewCommand.Execute(Id);
            App.GetService<EditCommonDictionaryPageVM>().RemoveExampleViewCommand.Execute(Id);
        }
        #endregion
    }
}
