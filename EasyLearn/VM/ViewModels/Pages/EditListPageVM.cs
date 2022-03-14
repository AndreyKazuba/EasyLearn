using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.VM.Core;
using EasyLearn.VM.Windows;
using Microsoft.Extensions.DependencyInjection;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Models;
using EasyLearn.Data.Enums;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class EditListPageVM : ViewModel
    {
        private readonly ICommonWordListsRepository commonWordListsRepository;

        private int currentCommonListId;

        public string Name { get; set; }
        public string Description { get; set; }
        public CommonWordListType Type { get; set; }

        #region Commands

        public DelegateCommand GoBack { get; set; }

        protected override void InitCommands()
        {
            this.GoBack = new DelegateCommand(arg =>
            {
                App.ServiceProvider.GetService<AppWindowVM>().OpenListsPage.Execute(arg);
            });
        }

        #endregion

        public EditListPageVM(ICommonWordListsRepository commonWordListsRepository)
        {
            this.commonWordListsRepository = commonWordListsRepository;
        }

        public void SetCurrentList(int listId)
        {
            this.currentCommonListId = listId;
            CommonWordList currentCommonList = commonWordListsRepository.GetCommonWordList(currentCommonListId);
            this.Name = currentCommonList.Name;
            this.Description = currentCommonList.Description;
            this.Type = currentCommonList.Type;
        }
    }
}
