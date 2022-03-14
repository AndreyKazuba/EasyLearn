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

namespace EasyLearn.VM.ViewModels.Pages
{
    public class ListsPageVM : ViewModel
    {
        private readonly IEasyLearnUsersRerository usersRerository;
        private readonly ICommonWordListsRepository commonWordListsRepository;
        private readonly IVerbPrepositionListsRepository verbPrepositionListsRepository;

        private int currentUserId;

        private ListType currentNewListType;
        private bool newListIsCommon;
        private bool newListIsPrepositionsList;

        private CommonWordListType currentCommonWordListType;
        private bool newListHasNoType;
        private bool newListWithNouns;
        private bool newListWithVerbs;
        private bool newListWithAdjectives;
        private bool newListWithSentences;
        private bool newListWithConbinationsOfWords;
        private bool newListWithPronouns;
        private bool newListWithNumerals;
        private bool newListWithAdverbs;

        #region Props for binding

        public ObservableCollection<UI.CustomControls.List> Lists { get; set; }
        public string NewListName { get; set; }
        public string NewListDescription { get; set; }
        public bool CommonWordListTypeComboBoxIsCollapsed { get; set; }

        public bool NewListIsCommon
        {
            get { return this.newListIsCommon; }
            set
            {
                this.newListIsCommon = value;
                this.currentNewListType = ListType.CommonWordsList;
                this.CommonWordListTypeComboBoxIsCollapsed = false;
            }
        }
        public bool NewListIsPrepositionsList
        {
            get { return this.newListIsPrepositionsList; }
            set
            {
                this.newListIsPrepositionsList = value;
                this.currentNewListType = ListType.VerbPrepositionsList;
                this.CommonWordListTypeComboBoxIsCollapsed = true;
            }
        }

        public bool NewListHasNoType
        {
            get { return this.newListHasNoType; }
            set
            {
                this.newListHasNoType = value;
                this.currentCommonWordListType = CommonWordListType.No;
            }
        }

        public bool NewListWithNouns
        {
            get { return this.newListWithNouns; }
            set
            {
                this.newListWithNouns = value;
                this.currentCommonWordListType = CommonWordListType.Nouns;
            }
        }

        public bool NewListWithVerbs
        {
            get { return this.newListWithVerbs; }
            set
            {
                this.newListWithVerbs = value;
                this.currentCommonWordListType = CommonWordListType.Verbs;
            }
        }

        public bool NewListWithAdjectives
        {
            get { return this.newListWithAdjectives; }
            set
            {
                this.newListWithAdjectives = value;
                this.currentCommonWordListType = CommonWordListType.Adjectives;
            }
        }

        public bool NewListWithSentences
        {
            get { return this.newListWithSentences; }
            set
            {
                this.newListWithSentences = value;
                this.currentCommonWordListType = CommonWordListType.Sentences;
            }
        }

        public bool NewListWithConbinationsOfWords
        {
            get { return this.newListWithConbinationsOfWords; }
            set
            {
                this.newListWithConbinationsOfWords = value;
                this.currentCommonWordListType = CommonWordListType.ConbinationsOfWords;
            }
        }

        public bool NewListWithPronouns
        {
            get { return this.newListWithPronouns; }
            set
            {
                this.newListWithPronouns = value;
                this.currentCommonWordListType = CommonWordListType.Pronouns;
            }
        }

        public bool NewListWithNumerals
        {
            get { return this.newListWithNumerals; }
            set
            {
                this.newListWithNumerals = value;
                this.currentCommonWordListType = CommonWordListType.Numerals;
            }
        }

        public bool NewListWithAdverbs
        {
            get { return this.newListWithAdverbs; }
            set
            {
                this.newListWithAdverbs = value;
                this.currentCommonWordListType = CommonWordListType.Adverbs;
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
            ICommonWordListsRepository commonWordListsRepository,
            IVerbPrepositionListsRepository verbPrepositionListsRepository)
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
                case ListType.CommonWordsList:
                    await commonWordListsRepository.AddList(this.NewListName, this.NewListDescription, this.currentUserId, this.currentCommonWordListType);
                    break;
                case ListType.VerbPrepositionsList:
                    await verbPrepositionListsRepository.AddList(this.NewListName, this.NewListDescription, this.currentUserId);
                    break;
            }

            RefreshLists();
        }

        private void ResetNewListWindow()
        {
            this.NewListName = string.Empty;
            this.NewListDescription = string.Empty;
            this.NewListIsCommon = true;
            this.NewListHasNoType = true;
        }

        private void UpdateCurrentUserId()
        {
            this.currentUserId = usersRerository.GetCurrentUser().Id;
        }

        private void RefreshLists()
        {
            UI.CustomControls.List irregularVerbsList = new UI.CustomControls.List(new ListVM("Irregular verbs", 0, ListType.IrregularVerbsList));

            IEnumerable<UI.CustomControls.List> commonLists = commonWordListsRepository
                .GetUsersCommonLists(this.currentUserId)
                .Select(list => new UI.CustomControls.List(new ListVM(list.Name, list.Id, ListType.CommonWordsList)));

            IEnumerable<UI.CustomControls.List> prepositionsLists = verbPrepositionListsRepository
                .GetUsersVerbPreposotionLists(this.currentUserId)
                .Select(list => new UI.CustomControls.List(new ListVM(list.Name, list.Id, ListType.VerbPrepositionsList)));

            List<UI.CustomControls.List> allCurrentUserLists = commonLists.Union(prepositionsLists).ToList();
            allCurrentUserLists.Add(irregularVerbsList);

            this.Lists = new ObservableCollection<UI.CustomControls.List>(allCurrentUserLists);
        }
    }
}
