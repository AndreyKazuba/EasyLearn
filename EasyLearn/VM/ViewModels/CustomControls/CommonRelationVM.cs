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
        public int Id { get; private set; }
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
        public string FirstExampleRussianValue { get; set; }
        public string FirstExampleEnglishValue { get; set; }
        public string SecondExampleRussianValue { get; set; }
        public string SecondExampleEnglishValue { get; set; }
        public bool IsSeporatorVisible { get; set; }
        public CommonRelationVM(CommonRelation commonRelation)
        {
            this.Id = commonRelation.Id;
            this.RussianValue = StringHelper.NormalizeRegister(commonRelation.RussianUnit.Value);
            this.EnglishValue = StringHelper.NormalizeRegister(commonRelation.EnglishUnit.Value);
            this.RussianUnitType = commonRelation.RussianUnit.Type.GetRussianValue();
            this.EnglishUnitType = commonRelation.EnglishUnit.Type.GetRussianValue();
            this.RussianUnitTypeColor = commonRelation.RussianUnit.Type.GetColor();
            this.EnglishUnitTypeColor = commonRelation.EnglishUnit.Type.GetColor();
            this.Comment = StringHelper.TryNormalizeRegister(commonRelation.Comment);
            this.IsCommentVisible = !string.IsNullOrEmpty(this.Comment);
            this.CardHeight = 75;
            if (this.IsCommentVisible)
                this.CardHeight += 48;
            bool firstExampleExist = commonRelation.Examples.Count > 0;
            bool secondExampleExist = commonRelation.Examples.Count > 1;
            if (firstExampleExist)
                this.CardHeight += 63;
            if (secondExampleExist)
                this.CardHeight += 50;
            this.VerticalExpanderMargin = IsCommentVisible || firstExampleExist ? new Thickness(0.3, 6, 0.3, 0) : new Thickness(0.3, 6, 0.3, 6);
            this.IsSeporatorVisible = IsCommentVisible || firstExampleExist;
            this.IsFirstExampleVisible = firstExampleExist;
            this.IsSecondExampleVisible = secondExampleExist;
            if (firstExampleExist)
            {
                this.FirstExampleRussianValue = commonRelation.Examples.ToArray()[0].RussianValue;
                this.FirstExampleEnglishValue = commonRelation.Examples.ToArray()[0].EnglishValue;
            }
            if (secondExampleExist)
            {
                this.FirstExampleRussianValue = commonRelation.Examples.ToArray()[1].RussianValue;
                this.FirstExampleEnglishValue = commonRelation.Examples.ToArray()[1].EnglishValue;
            }
        }

        #region Commands
        public Command DeleteRelationCommand { get; private set; }
        protected override void InitCommands()
        {
            this.DeleteRelationCommand = new Command(DeleteRelation);
        }
        #endregion

        private void DeleteRelation()
        {
            App.GetService<EditCommonDictionaryPageVM>().DeleteCommonRelationCommand.Execute(Id);
        }
    }
}
