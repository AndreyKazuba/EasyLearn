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
using EasyLearn.VM.ViewModels.ExpandedElements;
using EasyLearn.Infrastructure.Constants;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class DictionariesPageVM : ViewModel
    {
        private readonly IEasyLearnUsersRerository usersRerository;
        private readonly ICommonDictionaryRepository commonDictionaryRepository;
        private readonly IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository;

        private int currentUserId;

        #region Props for binding

        public ObservableCollection<UserControl> Dictionaries { get; set; }
        public string NewDictionaryName { get; set; }
        public string NewDictionaryDescription { get; set; }
        public ObservableCollection<DictionaryTypeComboBoxItem> DictionaryTypes { get; set; }
        public DictionaryTypeComboBoxItem SelectedDictionaryType { get; set; }

        #endregion

        #region Commands

        public DelegateCommand CleanDictionaryAddingWindowCommand { get; private set; }
        public DelegateCommand CreateNewList { get; private set; }
        public DelegateCommand RemoveCommonDictionaryCommand { get; private set; }
        public DelegateCommand RemoveVerbPrepositionDictionaryCommand { get; private set; }

        protected override void InitCommands()
        {
            this.CleanDictionaryAddingWindowCommand = new DelegateCommand(arg => CleanDictionaryAddingWindow());
            this.CreateNewList = new DelegateCommand(async arg => await AddNewDictionary());
            this.RemoveCommonDictionaryCommand = new DelegateCommand(async commonDictionaryId => await RemoveCommonDictionary((int)commonDictionaryId));
            this.RemoveVerbPrepositionDictionaryCommand = new DelegateCommand(async dictionaryId => await RemoveVerbPrepositionDictionary((int)dictionaryId));
        }

        #endregion

        public DictionariesPageVM
            (IEasyLearnUsersRerository usersRerository,
            ICommonDictionaryRepository commonWordListsRepository,
            IVerbPrepositionDictionaryRepository verbPrepositionListsRepository)
        {
            this.usersRerository = usersRerository;
            this.commonDictionaryRepository = commonWordListsRepository;
            this.verbPrepositionDictionaryRepository = verbPrepositionListsRepository;
            CleanDictionaryAddingWindow();
            UpdateCurrentUserId();
            RefreshDictionaries();
            SetDictionaryTypes();
        }

        public void UpdateView()
        {
            UpdateCurrentUserId();
            RefreshDictionaries();
        }

        private async Task RemoveCommonDictionary(int commonDictionaryId)
        {
            CommonDictionaryView? commonDictionaryView = FindCommonDictionary(commonDictionaryId);

            if (commonDictionaryView is null)
            {
                throw new Exception("Something went wrong");
            }

            await commonDictionaryRepository.DeleteCommonDictionary(commonDictionaryView.ViewModel.Id);
            this.Dictionaries.Remove(commonDictionaryView);
        }

        private CommonDictionaryView? FindCommonDictionary(int commonDictionaryId)
        {
            foreach (UserControl dictionary in this.Dictionaries)
            {
                if (dictionary is CommonDictionaryView)
                {
                    CommonDictionaryView dictionaryView = (CommonDictionaryView)dictionary;
                    if (dictionaryView.ViewModel.Id == commonDictionaryId)
                        return dictionaryView;
                }
            }

            return null;
        }

        private async Task RemoveVerbPrepositionDictionary(int verbPrepositionDictionaryId)
        {
            VerbPrepositionDictionaryView? verbPrepositionDictionaryView = FindVerbPrepositionDictionary(verbPrepositionDictionaryId);

            if (verbPrepositionDictionaryView is null)
            {
                throw new Exception("Something went wrong");
            }

            await verbPrepositionDictionaryRepository.DeleteVerbPrepositionDictionary(verbPrepositionDictionaryView.ViewModel.Id);
            this.Dictionaries.Remove(verbPrepositionDictionaryView);
        }

        private VerbPrepositionDictionaryView? FindVerbPrepositionDictionary(int verbPrepositionDictionaryId)
        {
            foreach (UserControl dictionary in this.Dictionaries)
            {
                if (dictionary is VerbPrepositionDictionaryView)
                {
                    VerbPrepositionDictionaryView dictionaryView = (VerbPrepositionDictionaryView)dictionary;
                    if (dictionaryView.ViewModel.Id == verbPrepositionDictionaryId)
                        return dictionaryView;
                }
            }

            return null;
        }

        private async Task AddNewDictionary()
        {
            switch (this.SelectedDictionaryType.DictionaryType)
            {
                case DictionaryType.CommonDictionary:
                    await commonDictionaryRepository.CreateCommonDictionary(this.NewDictionaryName, this.NewDictionaryDescription, this.currentUserId);
                    break;
                case DictionaryType.VerbPrepositionDictionary:
                    await verbPrepositionDictionaryRepository.CreateVerbPrepositionDictionary(this.NewDictionaryName, this.NewDictionaryDescription, this.currentUserId);
                    break;
            }

            RefreshDictionaries();
        }

        private void SetDictionaryTypes()
        {
            ObservableCollection<DictionaryTypeComboBoxItem> dictionryTypes = new ObservableCollection<DictionaryTypeComboBoxItem>
            {
                new DictionaryTypeComboBoxItem(DictionaryTypeRussianNames.CommonDictionary, DictionaryType.CommonDictionary),
                new DictionaryTypeComboBoxItem(DictionaryTypeRussianNames.VerbPrepositionDictionary, DictionaryType.VerbPrepositionDictionary),
            };

            DictionaryTypeComboBoxItem selectedItem = dictionryTypes[0];

            this.DictionaryTypes = dictionryTypes;
            this.SelectedDictionaryType = selectedItem;
        }

        private void CleanDictionaryAddingWindow()
        {
            this.NewDictionaryName = string.Empty;
            this.NewDictionaryDescription = string.Empty;
        }

        private void UpdateCurrentUserId()
        {
            this.currentUserId = usersRerository.GetCurrentUser().Id;
        }

        private void RefreshDictionaries()
        {
            UserControl irregularVerbsDictionary = new IrregularVerbDictionaryView(new IrregularVerbDictionaryVM());

            IEnumerable<UserControl> commonDictionaries = commonDictionaryRepository
                .GetUsersCommonDictionaries(this.currentUserId)
                .Select(commonDictionary => new CommonDictionaryView(new CommonDictionaryVM(commonDictionary)));

            IEnumerable<UserControl> verbPrepositionDictionaties = verbPrepositionDictionaryRepository
                .GetUsersVerbPreposotionDictionaries(this.currentUserId)
                .Select(verbPrepositionDictionary => new VerbPrepositionDictionaryView(new VerbPrepositionDictionaryVM(verbPrepositionDictionary)));

            List<UserControl> allCurrentUserDictionaries = commonDictionaries.Union(verbPrepositionDictionaties).ToList();
            allCurrentUserDictionaries.Add(irregularVerbsDictionary);

            this.Dictionaries = new ObservableCollection<UserControl>(allCurrentUserDictionaries);
        }
    }
}
