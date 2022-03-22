using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class CommonRelationVM : ViewModel
    {
        public int Id { get; set; }
        public string RussianValue { get; set; }
        public string EnglishValue { get; set; }
        public UnitType RussianUnitType { get; set; }
        public UnitType EnglishUnitType { get; set; }
        public string? Comment { get; set; }
        public bool IsCommentVisible { get; set; }
        public int CardHeight { get; set; }
        public Thickness VerticalExpanderMargin { get; set; }
        public CommonRelationVM(CommonRelation commonRelation)
        {
            this.Id = commonRelation.Id;
            this.RussianValue = StringHelper.NormalizeRegister(commonRelation.RussianUnit.Value);
            this.EnglishValue = StringHelper.NormalizeRegister(commonRelation.EnglishUnit.Value);
            this.RussianUnitType = commonRelation.RussianUnit.Type;
            this.EnglishUnitType = commonRelation.EnglishUnit.Type;
            this.Comment = StringHelper.TryNormalizeRegister(commonRelation.Comment);
            this.IsCommentVisible = !string.IsNullOrEmpty(this.Comment);
            this.CardHeight = string.IsNullOrEmpty(this.Comment) ? 75 : 125;
            this.VerticalExpanderMargin = string.IsNullOrEmpty(this.Comment) ? new Thickness(0.3, 6, 0.3, 6) : new Thickness(0.3, 6, 0.3, 0);
        }

        #region Commands
        public DelegateCommand DeleteRelationCommand { get; private set; }
        protected override void InitCommands()
        {
            this.DeleteRelationCommand = new DelegateCommand(arg => DeleteRelation());
        }
        #endregion

        private void DeleteRelation()
        {
            App.GetService<EditCommonDictionaryPageVM>().DeleteRelationCommand.Execute(Id);
        }
    }
}
