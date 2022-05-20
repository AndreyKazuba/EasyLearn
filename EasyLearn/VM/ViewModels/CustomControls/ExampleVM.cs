using EasyLearn.Data.Models;
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
        public string Content => $"{RussianValue} - {EnglishValue}";
        #endregion

        #region Commands
        public Command RemoveCommand { get; private set; }
        protected override void InitCommands()
        {
            this.RemoveCommand = new Command(Remove);
        }
        #endregion

        #region Command logic methods
        private void Remove()
        {
            App.GetService<EditCommonDictionaryPageVM>().RemoveExampleViewCommand.Execute(Id);
        }
        #endregion
        public ExampleVM(Example example)
        {
            this.Id = example.Id;
            RussianValue = example.RussianValue;
            EnglishValue = example.EnglishValue;
        }
        public ExampleVM(string russianTranslation, string englishTranslation, int id)
        {
            this.Id = id;
            this.RussianValue = russianTranslation;
            this.EnglishValue = englishTranslation;
        }
    }
}
