using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.Data.Enums;
using EasyLearn.VM.Core;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;
using EasyLearn.VM.ViewModels.Pages;
using EasyLearn.VM.Windows;

namespace EasyLearn.VM.ViewModels.CustomControls
{
    public class ListVM : ViewModel
    {
        private int listId;
        private bool isExpanded;

        public string Name { get; set; }
        public string Description { get; set; }
        public ListType Type { get; set; }
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                if (isExpanded)
                {
                    SetCurrentList();
                }
            }
        }

        #region Commands

        public DelegateCommand OpenCurrentList { get; set; }

        protected override void InitCommands()
        {
            this.OpenCurrentList = new DelegateCommand(arg =>
            {
                App.ServiceProvider.GetService<AppWindowVM>().OpenEditListPage.Execute();
            });
        }
        #endregion

        public ListVM(string name, string description, int listId, ListType type)
        {
            this.Name = name;
            this.Type = type;
            this.listId = listId;
            this.Description = description;
        }

        private async void SetCurrentList()
        {
            EditListPageVM? editListPageVM = App.ServiceProvider.GetService<EditListPageVM>();
            if (editListPageVM is not null)
            {
                await editListPageVM.SetCurrentList(listId);
            }
        }

    }
}
