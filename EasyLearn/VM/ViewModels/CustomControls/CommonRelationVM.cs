using System.Windows;
using System.Windows.Media;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class CommonRelationVM : ViewModel
    {
        private CardState state;

        public int Id { get; private set; }
        public int OrderValue { get; private set; }
        public string RussianValue { get; set; }
        public string EnglishValue { get; set; }
        public string RussianUnitType { get; set; }
        public string EnglishUnitType { get; set; }
        public Brush RussianUnitTypeColor { get; set; }
        public Brush EnglishUnitTypeColor { get; set; }
        public string? Comment { get; set; }
        public bool IsCommentVisible { get; set; }
        public int CardHeight { get; set; }
        public Thickness VerticalExpanderMargin { get; set; }
        public bool IsFirstExampleVisible { get; set; }
        public bool IsSecondExampleVisible { get; set; }
        public string? FirstExampleRussianValue { get; set; }
        public string? FirstExampleEnglishValue { get; set; }
        public string? SecondExampleRussianValue { get; set; }
        public string? SecondExampleEnglishValue { get; set; }
        public bool IsSeporatorVisible { get; set; }
        public CommonRelationVM(CommonRelation commonRelation)
        {
            this.Id = commonRelation.Id;
            UpdateVM(commonRelation);
        }
        public void UpdateVM(CommonRelation commonRelation)
        {
            this.RussianValue = StringHelper.NormalizeRegister(commonRelation.RussianUnit.Value);
            this.EnglishValue = StringHelper.NormalizeRegister(commonRelation.EnglishUnit.Value);
            this.RussianUnitType = commonRelation.RussianUnit.Type.GetRussianValue();
            this.EnglishUnitType = commonRelation.EnglishUnit.Type.GetRussianValue();
            this.RussianUnitTypeColor = commonRelation.RussianUnit.Type.GetColor();
            this.EnglishUnitTypeColor = commonRelation.EnglishUnit.Type.GetColor();
            this.Comment = StringHelper.TryNormalizeRegister(commonRelation.Comment);
            this.IsCommentVisible = !string.IsNullOrEmpty(this.Comment);
            this.IsFirstExampleVisible = commonRelation.IsFirstExampleExist;
            this.IsSecondExampleVisible = commonRelation.IsSecondExampleExist;
            this.FirstExampleRussianValue = commonRelation.FirstExampleRussianValue;
            this.FirstExampleEnglishValue = commonRelation.FirstExampleEnglishValue;
            this.SecondExampleRussianValue = commonRelation.SecondExampleRussianValue;
            this.SecondExampleEnglishValue = commonRelation.SecondExampleEnglishValue;
            SetState(commonRelation);
            SetHeight();
            SetOrder();
            SetVerticalExpanderMargin(commonRelation);
        }

        #region Commands
        public Command DeleteRelationCommand { get; private set; }
        public Command OpenSettingsCommand { get; private set; }
        protected override void InitCommands()
        {
            this.DeleteRelationCommand = new Command(DeleteRelation);
            this.OpenSettingsCommand = new Command(OpenSettings);
        }
        #endregion
        private void SetVerticalExpanderMargin(CommonRelation commonRelation)
        {
            bool shouldBeExpanded = IsCommentVisible || commonRelation.IsFirstExampleExist || commonRelation.IsSecondExampleExist;
            this.VerticalExpanderMargin = shouldBeExpanded ? new Thickness(0.3, 6, 0.3, 0) : new Thickness(0.3, 6, 0.3, 6);
            this.IsSeporatorVisible = shouldBeExpanded;
        }
        private void DeleteRelation()
        {
            App.GetService<EditCommonDictionaryPageVM>().DeleteCommonRelationCommand.Execute(Id);
        }
        private void OpenSettings()
        {
            App.GetService<EditCommonDictionaryPageVM>().OpenCommonRelationSettingsWindowCommand.Execute(Id);
        }
        private void SetState(CommonRelation commonRelation)
        {
            bool firstExampleExist = commonRelation.IsFirstExampleExist;
            bool secondExampleExist = commonRelation.IsSecondExampleExist;
            bool isCommentExist = this.IsCommentVisible;
            if (!isCommentExist && !firstExampleExist)
                this.state = CardState.Without;
            else if (isCommentExist && !firstExampleExist)
                this.state = CardState.JustComment;
            else if (!isCommentExist && firstExampleExist && !secondExampleExist)
                this.state = CardState.JustOneExample;
            else if (!isCommentExist && secondExampleExist)
                this.state = CardState.JustTwoExamples;
            else if (isCommentExist && firstExampleExist && !secondExampleExist)
                this.state = CardState.CommentAndOneExample;
            else
                this.state = CardState.CommentAndTwoExamples;
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
                    this.CardHeight = 75;
                    break;
                case CardState.JustComment:
                    this.CardHeight = 75 + 48;
                    break;
                case CardState.JustOneExample:
                    this.CardHeight = 75 + 63;
                    break;
                case CardState.JustTwoExamples:
                    this.CardHeight = 75 + 63 + 52;
                    break;
                case CardState.CommentAndOneExample:
                    this.CardHeight = 75 + 48 + 52;
                    break;
                case CardState.CommentAndTwoExamples:
                    this.CardHeight = 75 + 48 + 52 + 52;
                    break;
            }
        }
        private enum CardState
        {
            Without = 0,
            JustComment = 1,
            JustOneExample = 2,
            JustTwoExamples = 4,
            CommentAndOneExample = 3,
            CommentAndTwoExamples = 5,
        }
    }
}
