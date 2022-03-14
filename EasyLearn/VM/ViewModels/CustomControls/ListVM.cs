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

        public string Name { get; set; }

        public ListType Type { get; set; }

        public SolidColorBrush Color { get; set; }

        #region Commands

        public DelegateCommand OpenCurrentList { get; set; }

        protected override void InitCommands()
        {
            this.OpenCurrentList = new DelegateCommand(arg =>
            {
                App.ServiceProvider.GetService<EditListPageVM>().SetCurrentList(listId);
                App.ServiceProvider.GetService<AppWindowVM>().OpenEditListPage.Execute();
            });
        }
        #endregion

        public ListVM(string name, int listId, ListType type)
        {
            this.Name = name;
            this.Type = type;
            this.listId = listId;

            switch (type)
            {
                case ListType.IrregularVerbsList:
                    Color = new SolidColorBrush(Colors.Red);
                    break;
                case ListType.CommonWordsList:
                    Color = new SolidColorBrush(Colors.Blue);
                    break;
                case ListType.VerbPrepositionsList:
                    Color = new SolidColorBrush(Colors.Green);
                    break;
            }
        }

        //private void OpenCurrentList()
        //{
        //    App.ServiceProvider.GetService<EditListPageVM>().SetCurrentList(listId);
        //    App.ServiceProvider.GetService<AppWindowVM>().OpenEditListPage.Execute();
        //}
    }
}
