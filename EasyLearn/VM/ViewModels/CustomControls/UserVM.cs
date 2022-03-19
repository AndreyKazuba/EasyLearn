using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.VM.Core;
using EasyLearn.Data.Models;
using EasyLearn.VM.ViewModels.Pages;
using Microsoft.Extensions.DependencyInjection;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Helpers;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class UserVM : ViewModel
    {
        private readonly IEasyLearnUserRepository userRerository;

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCurrent { get; set; }
        public string EditNameFieldValue { get; set; }
        public bool IsCardFlipped { get; set; }
        public bool BackButtonIsEnabled { get; set; } = true;

        public UserVM(EasyLearnUser user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.IsCurrent = user.IsCurrent;

            IEasyLearnUserRepository? usersRerository = App.ServiceProvider.GetService<IEasyLearnUserRepository>();
            if (usersRerository is not null)
                this.userRerository = usersRerository;
            else
                throw new Exception("Something went wrong");
        }


        #region Commands

        public DelegateCommand SetUserAsCurrentCommand { get; private set; }
        public DelegateCommand RemoveUserCommand { get; private set; }
        public DelegateCommand EditUserCommand { get; private set; }
        public DelegateCommand SetEditNameFieldValueCommand { get; private set; }
        public DelegateCommand FlipBackAllAnotherCardsCommand { get; private set; }

        protected override void InitCommands()
        {
            this.SetUserAsCurrentCommand = new DelegateCommand(arg => SetUserAsCurrent());
            this.RemoveUserCommand = new DelegateCommand(arg => RemoveUser());
            this.EditUserCommand = new DelegateCommand(async arg => await EditUser());
            this.SetEditNameFieldValueCommand = new DelegateCommand(arg => SetEditNameFieldValue());
            this.FlipBackAllAnotherCardsCommand = new DelegateCommand(arg => FlipBackAllAnotherCards());
        }
        private void SetUserAsCurrent() => GetUsersPageVM().SetUserAsCurrentCommand.Execute(this.Id);
        private void RemoveUser() => GetUsersPageVM().RemoveUserCommand.Execute(this.Id);
        private void FlipBackAllAnotherCards() => GetUsersPageVM().FlipBackAllCardsCommand.Execute();
        private void SetEditNameFieldValue() => this.EditNameFieldValue = this.Name;
        private async Task EditUser()
        {
            string newUserName = this.EditNameFieldValue;
            if (String.IsNullOrWhiteSpace(newUserName))
                throw new Exception("Something went wrong");
            if (StringHelper.Equals(this.Name, newUserName))
                return;
            this.Name = newUserName;
            await userRerository.EditUser(this.Id, newUserName);
        }
        private UsersPageVM GetUsersPageVM()
        {
            UsersPageVM? usersPageVM = App.ServiceProvider.GetService<UsersPageVM>();
            if (usersPageVM is not null)
                return usersPageVM;
            else
                throw new Exception("Something went wrong");
        }
        #endregion
    }
}
