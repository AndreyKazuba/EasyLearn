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
        public string NickName { get; set; }
        public bool IsCurrent { get; set; }
        public bool BackButtonIsEnabled { get; set; } = true;

        public UserVM(EasyLearnUser user)
        {
            this.Id = user.Id;
            this.NickName = user.Name;
            this.IsCurrent = user.IsCurrent;
        }

        #region Commands

        public DelegateCommand SetCurrentUser { get; private set; }
        public DelegateCommand RemoveUser { get; private set; }

        protected override void InitCommands()
        {
            this.SetCurrentUser = new DelegateCommand(async userId => await App.ServiceProvider.GetService<UsersPageVM>().SetCurrentUser((int)userId));
            this.RemoveUser = new DelegateCommand(async userId => await App.ServiceProvider.GetService<UsersPageVM>().RemoveUser((int)userId));
        }

        #endregion
    }
}
