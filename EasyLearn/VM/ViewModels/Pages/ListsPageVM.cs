using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.VM.Core;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;
using System.Collections.ObjectModel;
using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;
using EasyLearn.UI.CustomControls;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class ListsPageVM : ViewModel
    {
        private readonly IEasyLearnUsersRerository usersRerository;
        private readonly ICommonDictionaryRepository commonWordListsRepository;
        private readonly IVerbPrepositionDictionaryRepository verbPrepositionListsRepository;

        private int currentUserId;

        private DictionaryType currentNewListType;
        private bool newListIsCommon;
        private bool newListIsPrepositionsList;

        #region Props for binding

        public ObservableCollection<UserControl> Lists { get; set; }
        public string NewListName { get; set; }
        public string NewListDescription { get; set; }
        public bool CommonWordListTypeComboBoxIsCollapsed { get; set; }

        public bool NewListIsCommon
        {
            get { return this.newListIsCommon; }
            set
            {
                this.newListIsCommon = value;
                this.currentNewListType = DictionaryType.CommonDictionary;
                this.CommonWordListTypeComboBoxIsCollapsed = false;
            }
        }
        public bool NewListIsPrepositionsList
        {
            get { return this.newListIsPrepositionsList; }
            set
            {
                this.newListIsPrepositionsList = value;
                this.currentNewListType = DictionaryType.VerbPrepositionDictionary;
                this.CommonWordListTypeComboBoxIsCollapsed = true;
            }
        }




        #endregion

        #region Commands

        public DelegateCommand ClearNewListWindow { get; set; }
        public DelegateCommand CreateNewList { get; set; }

        protected override void InitCommands()
        {
            this.ClearNewListWindow = new DelegateCommand(arg => ResetNewListWindow());
            this.CreateNewList = new DelegateCommand(async arg => await AddNewList());
        }

        #endregion

        public ListsPageVM
            (IEasyLearnUsersRerository usersRerository,
            ICommonDictionaryRepository commonWordListsRepository,
            IVerbPrepositionDictionaryRepository verbPrepositionListsRepository)
        {
            this.usersRerository = usersRerository;
            this.commonWordListsRepository = commonWordListsRepository;
            this.verbPrepositionListsRepository = verbPrepositionListsRepository;
            ResetNewListWindow();
            UpdateCurrentUserId();
            RefreshLists();
        }

        public void UpdateView()
        {
            UpdateCurrentUserId();
            RefreshLists();
        }

        private async Task AddNewList()
        {
            switch (this.currentNewListType)
            {
                case DictionaryType.CommonDictionary:
                    await commonWordListsRepository.CreateCommonDictionary(this.NewListName, this.NewListDescription, this.currentUserId);
                    break;
                case DictionaryType.VerbPrepositionDictionary:
                    await verbPrepositionListsRepository.CreateVerbPrepositionDictionary(this.NewListName, this.NewListDescription, this.currentUserId);
                    break;
            }

            RefreshLists();
        }

        private void ResetNewListWindow()
        {
            this.NewListName = string.Empty;
            this.NewListDescription = string.Empty;
            this.NewListIsCommon = true;
        }

        private void UpdateCurrentUserId()
        {
            this.currentUserId = usersRerository.GetCurrentUser().Id;
        }

        private void RefreshLists()
        {
            UserControl irregularVerbsList = new IrregularVerbsListView(new IrregularVerbsListVM());

            IEnumerable<UserControl> commonLists = commonWordListsRepository
                .GetUsersCommonDictionaries(this.currentUserId)
                .Select(list => new CommonWordListView(new CommonWordListVM(list.Name, list.Description, list.Id)));

            IEnumerable<UserControl> prepositionsLists = verbPrepositionListsRepository
                .GetUsersVerbPreposotionDictionaries(this.currentUserId)
                .Select(list => new VerbPrepositionListView(new VerbPrepositionListVM(list.Name, list.Description, list.Id)));

            List<UserControl> allCurrentUserLists = commonLists.Union(prepositionsLists).ToList();
            allCurrentUserLists.Add(irregularVerbsList);

            this.Lists = new ObservableCollection<UserControl>(allCurrentUserLists);
        }
    }
}
