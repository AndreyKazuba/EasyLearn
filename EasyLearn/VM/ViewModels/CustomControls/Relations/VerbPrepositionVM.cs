using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.Helpers;
using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;
using System.Windows.Media;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class VerbPrepositionVM : ViewModel
    {
        #region Private fields
        private CardState state;
        #endregion

        #region Public props
        public int Id { get; private set; }
        public int OrderValue { get; private set; }
        #endregion

        #region Binding props
        public bool IsVisible { get; set; } = true;
        public string VerbValue { get; set; }
        public string PrepositionValue { get; set; }
        public string TranslationValue { get; set; }
        public string FirstExampleRussianValue { get; set; }
        public string FirstExampleEnglishValue { get; set; }
        public string SecondExampleRussianValue { get; set; }
        public string SecondExampleEnglishValue { get; set; }
        public bool IsFirstExampleVisible { get; set; }
        public bool IsSecondExampleVisible { get; set; }
        public bool IsStudiedMarkVisible { get; set; }
        public int Height { get; set; }
        public int RatingValue { get; set; }
        public Brush RatingProgressBarColor { get; set; }
        #endregion

#pragma warning disable CS8618
        public VerbPrepositionVM(VerbPreposition verbPreposition)
        {
            this.Id = verbPreposition.Id;
            Set(verbPreposition);
        }
#pragma warning restore CS8618

        #region Commands
        public Command OpenUpdateVerbPrepositionWindowCommand { get; private set; }
        protected override void InitCommands()
        {
            OpenUpdateVerbPrepositionWindowCommand = new Command(OpenUpdateVerbPrepositionWindow);
        }
        private void OpenUpdateVerbPrepositionWindow() => App.GetService<EditVerbPrepositionDictionaryPageVM>().UwOpenWindowCommand.Execute(Id);
        #endregion

        #region Public methods
        public void Set(VerbPreposition verbPreposition)
        {
            PrepositionValue = verbPreposition.Preposition.Value;
            VerbValue = StringHelper.NormalizeRegister(verbPreposition.Verb.Value);
            TranslationValue = StringHelper.NormalizeRegister(verbPreposition.Translation);
            IsFirstExampleVisible = verbPreposition.IsFirstExampleExist;
            IsSecondExampleVisible = verbPreposition.IsSecondExampleExist;
            FirstExampleRussianValue = verbPreposition.FirstExampleRussianValue.TryNormalizeRegister().EmptyIfNull();
            FirstExampleEnglishValue = verbPreposition.FirstExampleEnglishValue.TryNormalizeRegister().EmptyIfNull();
            SecondExampleRussianValue = verbPreposition.SecondExampleRussianValue.TryNormalizeRegister().EmptyIfNull();
            SecondExampleEnglishValue = verbPreposition.SecondExampleEnglishValue.TryNormalizeRegister().EmptyIfNull();
            RatingValue = verbPreposition.Rating;
            RatingProgressBarColor = verbPreposition.Rating.GetColorForRating();
            IsStudiedMarkVisible = verbPreposition.Studied;
            SetState(verbPreposition);
            SetHeight();
            SetOrder();
        }
        public void Collapse() => IsVisible = false;
        public void Show() => IsVisible = true;
        #endregion

        #region Private helpers
        private void SetState(VerbPreposition verbPreposition)
        {
            bool firstExampleExist = verbPreposition.IsFirstExampleExist;
            bool secondExampleExist = verbPreposition.IsSecondExampleExist;
            if (!firstExampleExist && !secondExampleExist)
                state = CardState.Without;
            else if (firstExampleExist && secondExampleExist)
                state = CardState.WithTwoExamples;
            else
                state = CardState.WithOneExample;
        }
        private void SetOrder() => OrderValue = (int)state;
        private void SetHeight()
        {
            switch (state)
            {
                case CardState.Without:
                    Height = 94;
                    break;
                case CardState.WithOneExample:
                    Height = 94 + 50;
                    break;
                case CardState.WithTwoExamples:
                    Height = 94 + 50 + 52;
                    break;
            }
        }
        #endregion

        #region Nested types
        private enum CardState
        {
            Without = 0,
            WithOneExample = 1,
            WithTwoExamples = 2,
        }
        #endregion
    }
}
