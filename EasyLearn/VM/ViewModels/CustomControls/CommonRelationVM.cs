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
        #region Private fields
        private CardState state;
        #endregion

        #region Public props
        public int Id { get; private set; }
        public int OrderValue { get; private set; }
        #endregion

        #region Binding props
        public bool IsVisible { get; set; } = true;
        public string RussianValue { get; set; }
        public string EnglishValue { get; set; }
        public string CommentValue { get; set; }            
        public string RussianUnitTypeText { get; set; } 
        public string EnglishUnitTypeText { get; set; }
        public string FirstExampleRussianValue { get; set; }    
        public string FirstExampleEnglishValue { get; set; }    
        public string SecondExampleRussianValue { get; set; }   
        public string SecondExampleEnglishValue { get; set; }   
        public bool CommentIsVisible { get; set; }  
        public bool FirstExampleIsVisible { get; set; } 
        public bool SecondExampleIsVisible { get; set; }    
        public bool HorisontalSeporatorIsVisible { get; set; }    
        public Brush RussianUnitTypeColor { get; set; }
        public Brush EnglishUnitTypeColor { get; set; }
        public int Height { get; set; }
        public Thickness VerticalExpanderMargin { get; set; }
        #endregion

#pragma warning disable CS8618
        public CommonRelationVM(CommonRelation commonRelation)
        {
            this.Id = commonRelation.Id;
            Set(commonRelation);
        }
#pragma warning restore CS8618

        #region Commands
        public Command DeleteRelationCommand { get; private set; }
        public Command OpenUpdateRelationWindowCommand { get; private set; }
        protected override void InitCommands()
        {
            this.DeleteRelationCommand = new Command(DeleteRelation);
            this.OpenUpdateRelationWindowCommand = new Command(OpenUpdateRelationWindow);
        }
        private void DeleteRelation() => App.GetService<EditCommonDictionaryPageVM>().DeleteCommonRelationCommand.Execute(Id);
        private void OpenUpdateRelationWindow() => App.GetService<EditCommonDictionaryPageVM>().UwOpenWindowCommand.Execute(Id);
        #endregion

        #region Public methods
        public void Set(CommonRelation commonRelation)
        {
            this.RussianValue = StringHelper.NormalizeRegister(commonRelation.RussianUnit.Value);
            this.EnglishValue = StringHelper.NormalizeRegister(commonRelation.EnglishUnit.Value);
            this.RussianUnitTypeText = commonRelation.RussianUnit.Type.GetRussianValue();
            this.EnglishUnitTypeText = commonRelation.EnglishUnit.Type.GetRussianValue();
            this.RussianUnitTypeColor = commonRelation.RussianUnit.Type.GetColor();
            this.EnglishUnitTypeColor = commonRelation.EnglishUnit.Type.GetColor();
            this.CommentValue = commonRelation.Comment.TryNormalizeRegister().EmptyIfNull();
            this.CommentIsVisible = !string.IsNullOrEmpty(this.CommentValue);
            this.FirstExampleIsVisible = commonRelation.IsFirstExampleExist;
            this.SecondExampleIsVisible = commonRelation.IsSecondExampleExist;
            this.FirstExampleRussianValue = commonRelation.FirstExampleRussianValue.TryNormalizeRegister().EmptyIfNull();
            this.FirstExampleEnglishValue = commonRelation.FirstExampleEnglishValue.TryNormalizeRegister().EmptyIfNull();
            this.SecondExampleRussianValue = commonRelation.SecondExampleRussianValue.TryNormalizeRegister().EmptyIfNull();
            this.SecondExampleEnglishValue = commonRelation.SecondExampleEnglishValue.TryNormalizeRegister().EmptyIfNull();
            SetState(commonRelation);
            SetHeight();
            SetOrder();
            SetVerticalExpanderMargin(commonRelation);
        }
        public void Collapse() => this.IsVisible = false;
        public void Show() => this.IsVisible = true;
        #endregion

        #region Private methods
        private void SetState(CommonRelation commonRelation)
        {
            bool firstExampleExist = commonRelation.IsFirstExampleExist;
            bool secondExampleExist = commonRelation.IsSecondExampleExist;
            bool isCommentExist = this.CommentIsVisible;
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
        private void SetHeight()
        {
            switch (this.state)
            {
                case CardState.Without:
                    this.Height = 75;
                    break;
                case CardState.JustComment:
                    this.Height = 75 + 48;
                    break;
                case CardState.JustOneExample:
                    this.Height = 75 + 63;
                    break;
                case CardState.JustTwoExamples:
                    this.Height = 75 + 63 + 52;
                    break;
                case CardState.CommentAndOneExample:
                    this.Height = 75 + 48 + 52;
                    break;
                case CardState.CommentAndTwoExamples:
                    this.Height = 75 + 48 + 52 + 52;
                    break;
            }
        }
        private void SetOrder() => this.OrderValue = (int)this.state;
        private void SetVerticalExpanderMargin(CommonRelation commonRelation)
        {
            bool shouldBeExpanded = CommentIsVisible || commonRelation.IsFirstExampleExist || commonRelation.IsSecondExampleExist;
            this.VerticalExpanderMargin = shouldBeExpanded ? new Thickness(0.3, 6, 0.3, 0) : new Thickness(0.3, 6, 0.3, 6);
            this.HorisontalSeporatorIsVisible = shouldBeExpanded;
        }
        #endregion

        #region Nested types
        private enum CardState
        {
            Without = 0,
            JustComment = 1,
            JustOneExample = 2,
            JustTwoExamples = 4,
            CommentAndOneExample = 3,
            CommentAndTwoExamples = 5,
        }
        #endregion
    }
}
