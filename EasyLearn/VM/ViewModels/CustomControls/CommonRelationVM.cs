using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.VM.Core;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class CommonRelationVM : ViewModel
    {
        public string RussianValue { get; set; }
        public string EnglishValue { get; set; }
        public UnitType RussianUnitType { get; set; }
        public UnitType EnglishUnitType { get; set; }
        public string? Comment { get; set; }
        public CommonRelationVM(CommonRelation commonRelation)
        {
            this.RussianValue = StringHelper.NormalizeRegister(commonRelation.RussianUnit.Value);
            this.EnglishValue = StringHelper.NormalizeRegister(commonRelation.EnglishUnit.Value);
            this.RussianUnitType = commonRelation.RussianUnit.Type;
            this.EnglishUnitType = commonRelation.EnglishUnit.Type;
            this.Comment = StringHelper.TryNormalizeRegister(commonRelation.Comment);
        }
    }
}
