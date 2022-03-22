using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EasyLearn.VM.Core
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ViewModel()
        {
            InitCommands();
            InitEvents();
        }
        protected void OnPropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        protected virtual void InitCommands() { }
        protected virtual void InitEvents() { }
    }
}
