using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.VM.Core;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using EasyLearn.UI.CustomControls;
using System.Collections.ObjectModel;
using EasyLearn.VM.ViewModels.CustomControls;

//#pragma warning disable 

namespace EasyLearn.VM.ViewModels.Pages
{
    public class UsersPageVM : ViewModel
    {
        private readonly IEasyLearnUsersRerository usersRerository;
        public UsersPageVM(IEasyLearnUsersRerository usersRerository)
        {
            this.usersRerository = usersRerository;

            RefreshUsers();
        }

        #region Props for binding

        public ObservableCollection<User> Users { get; set; }
        public string NewUserNickName { get; set; }
        public bool ConfirmNewUserButtonIsEnabled { get; set; } = true;

        #endregion

        #region Commands

        public DelegateCommand CreateUser { get; private set; }
        public DelegateCommand DeleteUser { get; private set; }

        public DelegateCommand ClearNewUserNickName { get; private set; }

        protected override void InitCommands()
        {
            this.CreateUser = new DelegateCommand(async arg =>
            {
                await usersRerository.AddUser(NewUserNickName);
                RefreshUsers();
            });
            this.ClearNewUserNickName = new DelegateCommand(arg => this.NewUserNickName = string.Empty);
        }

        #endregion

        public async Task SetCurrentUser(int userId)
        {
            if (this.Users.Any())
            {
                User? lastCurrentUser = this.Users.FirstOrDefault(user => user.ViewModel.IsCurrent);
                if (lastCurrentUser is not null)
                {
                    lastCurrentUser.ViewModel.IsCurrent = false;
                }
                
                this.Users.First(user => user.ViewModel.Id == userId).ViewModel.IsCurrent = true;
            }

            await usersRerository.SetCurrentUser(userId);
        }

        public async Task RemoveUser(int userId)
        {
            User user = this.Users.First(user => user.ViewModel.Id == userId);
            bool isCurrent = user.ViewModel.IsCurrent;

            this.Users.Remove(user);

            if (isCurrent && this.Users.Any())
            {
                await SetCurrentUser(this.Users[0].ViewModel.Id);
            }

            await usersRerository.RemoveUser(userId);
        }

        private void RefreshUsers()
        {
            IEnumerable<EasyLearnUser> easyLearnUsers = usersRerository.GetAllUsers();
            this.Users = new ObservableCollection<User>(easyLearnUsers.Select(easyLearnUser => new User(new UserVM(easyLearnUser))));
        }
    }
}
