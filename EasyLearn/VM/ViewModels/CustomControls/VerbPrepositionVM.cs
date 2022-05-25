using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class VerbPrepositionVM : ViewModel
    {
        private CardState state;
        public int Id { get; private set; }
        public string VerbValue { get; set; }
        public int OrderValue { get; set; }
        public string PrepositionValue { get; set; }
        public string TranslationValue { get; set; }
        public int CardHeight { get; set; }
        public bool IsFirstExampleVisible { get; set; }
        public bool IsSecondExampleVisible { get; set; }
        public string? FirstExampleRussianValue { get; set; }
        public string? FirstExampleEnglishValue { get; set; }
        public string? SecondExampleRussianValue { get; set; }
        public string? SecondExampleEnglishValue { get; set; }
        public VerbPrepositionVM(VerbPreposition verbPreposition)
        {
            this.Id = verbPreposition.Id;
            UpdateVM(verbPreposition);
        }
        public void UpdateVM(VerbPreposition verbPreposition)
        {
            this.PrepositionValue = verbPreposition.Preposition.Value;
            this.VerbValue = StringHelper.NormalizeRegister(verbPreposition.Verb.Value);
            this.TranslationValue = StringHelper.NormalizeRegister(verbPreposition.Translation);
            this.IsFirstExampleVisible = verbPreposition.IsFirstExampleExist;
            this.IsSecondExampleVisible = verbPreposition.IsSecondExampleExist;
            this.FirstExampleRussianValue = verbPreposition.FirstExampleRussianValue;
            this.FirstExampleEnglishValue = verbPreposition.FirstExampleEnglishValue;
            this.SecondExampleRussianValue = verbPreposition.SecondExampleRussianValue;
            this.SecondExampleEnglishValue = verbPreposition.SecondExampleEnglishValue;
            SetState(verbPreposition);
            SetHeight();
            SetOrder();
        }
        public Command OpenSettingsCommand { get; private set; }
        protected override void InitCommands()
        {
            this.OpenSettingsCommand = new Command(OpenSettings);
        }
        private void OpenSettings() => App.GetService<EditVerbPrepositionDictionaryPageVM>().UwOpenWindowCommand.Execute(Id);
        private void SetState(VerbPreposition verbPreposition)
        {
            bool firstExampleExist = verbPreposition.IsFirstExampleExist;
            bool secondExampleExist = verbPreposition.IsSecondExampleExist;
            if (!firstExampleExist && !secondExampleExist)
                this.state = CardState.Without;
            else if (firstExampleExist && secondExampleExist)
                this.state = CardState.WithTwoExamples;
            else
                this.state = CardState.WithOneExample;
        }
        private void SetOrder()
        {
            this.OrderValue = (int)this.state;
        }
        private void SetHeight()
        {
            switch (this.state)
            {
                case CardState.Without:
                    this.CardHeight = 94;
                    break;
                case CardState.WithOneExample:
                    this.CardHeight = 94 + 50;
                    break;
                case CardState.WithTwoExamples:
                    this.CardHeight = 94 + 50 + 52;
                    break;
            }
        }

        private enum CardState
        {
            Without = 0,
            WithOneExample = 1,
            WithTwoExamples = 2,
        }
    }
}
