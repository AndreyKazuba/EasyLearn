using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class ShadowUserVM : ViewModel
    {
        public Command AddUserCommand { get; set; }
        protected override void InitCommands()
        {
            this.AddUserCommand = new Command(AddUser);
        }
        private void AddUser() => App.GetService<UsersPageVM>().OpenAddingUserWindowCommand.Execute();
        
    }
}
