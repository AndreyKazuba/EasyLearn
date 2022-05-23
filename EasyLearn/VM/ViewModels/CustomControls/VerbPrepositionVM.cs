using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.VM.Core;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class VerbPrepositionVM : ViewModel
    {
        public string VerbValue { get; set; }
        public string PrepositionValue { get; set; }
        public string TranslationValue { get; set; }
        public VerbPrepositionVM(VerbPreposition verbPreposition)
        {
            this.PrepositionValue = verbPreposition.Preposition.Value;
            this.VerbValue = StringHelper.NormalizeRegister(verbPreposition.Verb.Value);
            this.TranslationValue = StringHelper.NormalizeRegister(verbPreposition.Translation);
            //this.CommentValue = StringHelper.EmptyIfNull(verbPreposition.Comment);
        }
    }
}
