using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.VM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class VerbPrepositionVM : ViewModel
    {
        public string PrepositionValue { get; set; }
        public string VerbValue { get; set; }
        public string Comment { get; set; }
        public VerbPrepositionVM(VerbPreposition verbPreposition)
        {
            this.PrepositionValue = verbPreposition.Preposition.Value.ToLower();
            this.VerbValue = StringHelper.NormalizeRegister(verbPreposition.Verb.Value);
            this.Comment = StringHelper.TryNormalizeRegister(verbPreposition.Comment);
        }
    }
}
