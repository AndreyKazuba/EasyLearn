using EasyLearn.Data.Models;
using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class ExampleVM : ViewModel
    {
        #region Private fields
        private string russianTranslation;
        private string englishTranslation;
        #endregion

        #region Public fields
        public int Id { get; private set; }
        #endregion
        public string Content => $"{russianTranslation} - {englishTranslation}";

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
            russianTranslation = example.RussianValue;
            englishTranslation = example.EnglishValue;
        }
        public ExampleVM(string russianTranslation, string englishTranslation, int id)
        {
            this.Id = id;
            this.russianTranslation = russianTranslation;
            this.englishTranslation = englishTranslation;
        }
    }
}
