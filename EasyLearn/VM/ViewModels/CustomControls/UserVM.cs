using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.VM.Core;
using EasyLearn.Data.Models;
using EasyLearn.VM.ViewModels.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class UserVM : ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCurrent { get; set; }
        public bool BackButtonIsEnabled { get; set; } = true;

        public UserVM(EasyLearnUser user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.IsCurrent = user.IsCurrent;
        }

        #region Commands

        public DelegateCommand SetUserAsCurrentCommand { get; private set; }
        public DelegateCommand RemoveUserCommand { get; private set; }

        protected override void InitCommands()
        {
            this.SetUserAsCurrentCommand = new DelegateCommand(userId => App.ServiceProvider.GetService<UsersPageVM>().SetUserAsCurrentCommand.Execute(userId));
            this.RemoveUserCommand = new DelegateCommand(userId => App.ServiceProvider.GetService<UsersPageVM>().RemoveUserCommand.Execute(userId));
        }

        #endregion
    }
}
