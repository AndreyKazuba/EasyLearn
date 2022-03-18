using EasyLearn.VM.Core;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class IrregularVerbDictionaryVM : ViewModel
    {
        public string Name { get; set; }

        public IrregularVerbDictionaryVM()
        {
            this.Name = "Неправильные глаголы";
        }
    }
}
