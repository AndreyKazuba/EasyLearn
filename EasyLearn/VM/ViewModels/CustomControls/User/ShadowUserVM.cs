using EasyLearn.VM.Core;
using EasyLearn.VM.ViewModels.Pages;

#pragma warning disable CS8618
namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class ShadowUserVM : ViewModel
    {
        #region Commands
        public Command AddUserCommand { get; set; }
        protected override void InitCommands()
        {
            AddUserCommand = new Command(AddUser);
        }
        private void AddUser() => App.GetService<UsersPageVM>().OpenAddingUserWindowCommand.Execute();
        #endregion
    }
}
