using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Controls;
using EasyLearn.VM.Core;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;
using EasyLearn.UI.CustomControls;
using EasyLearn.VM.ViewModels.ExpandedElements;
using EasyLearn.Data.Constants;
using EasyLearn.VM.Windows;
using EasyLearn.Data.Helpers;
using EasyLearn.Infrastructure.Validation;
using EasyLearn.Infrastructure.Exceptions;
using EasyLearn.UI.Pages;
using EasyLearn.Infrastructure.Helpers;

namespace EasyLearn.VM.ViewModels.Pages
{
    public class DictionariesPageVM : ViewModel
    {
        #region Repositories
        private readonly IEasyLearnUserRepository userRepository;
        private readonly ICommonDictionaryRepository commonDictionaryRepository;
        private readonly IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository;
        #endregion

        #region Private fields
        private int currentUserId;
        #endregion

        #region Binding props
        public ObservableCollection<UserControl> DictionaryViews { get; set; }
        public string AddingWindowDictionaryNameValue { get; set; }
        public ObservableCollection<DictionaryTypeComboBoxItem> AddingWindowDictionaryTypes { get; set; }
        public DictionaryTypeComboBoxItem AddingWindowSelectedDictionaryType { get; set; }
        public bool IsConfirmDictionaryAddingButtonEnabled { get; set; }
        public bool DictionaryNameTextBoxHasError { get; set; }
        #endregion

#pragma warning disable CS8618
        public DictionariesPageVM(
            IEasyLearnUserRepository userRepository,
            ICommonDictionaryRepository commonDictionaryRepository,
            IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository)
        {
            this.userRepository = userRepository;
            this.commonDictionaryRepository = commonDictionaryRepository;
            this.verbPrepositionDictionaryRepository = verbPrepositionDictionaryRepository;
            UpdatePageForNewUser();
            SetAddingWindowDictionaryTypes();
            ClearAddingWindow();
        }
#pragma warning restore CS8618

        #region Commands
        public Command ClearAddingWindowCommand { get; private set; }
        public Command CreateDictionaryCommand { get; private set; }
        public Command<int> DeleteCommonDictionaryCommand { get; private set; }
        public Command<int> DeleteVerbPrepositionDictionaryCommand { get; private set; }
        public Command FlipBackAllCardsCommand { get; private set; }
        public Command UpdatePageForNewUserCommand { get; private set; }
        public Command UpdateConfirmDictionaryAddingButtonAvailabilityCommand { get; private set; }
        public Command OpenAddingDictionaryWindowCommand { get; private set; }
        protected override void InitCommands()
        {
            ClearAddingWindowCommand = new Command(ClearAddingWindow);
            CreateDictionaryCommand = new Command(async () => await CreateDictionary());
            DeleteCommonDictionaryCommand = new Command<int>(async dictionaryId => await DeleteCommonDictionary(dictionaryId));
            DeleteVerbPrepositionDictionaryCommand = new Command<int>(async dictionaryId => await DeleteVerbPrepositionDictionary(dictionaryId));
            FlipBackAllCardsCommand = new Command(FlipBackAllCards);
            UpdatePageForNewUserCommand = new Command(UpdatePageForNewUser);
            UpdateConfirmDictionaryAddingButtonAvailabilityCommand = new Command(UpdateConfirmDictionaryAddingButtonAvailability);
            OpenAddingDictionaryWindowCommand = new Command(OpenAddingDictionaryWindow);
        }
        private void ClearAddingWindow()
        {
            AddingWindowDictionaryNameValue = string.Empty;
            AddingWindowSelectedDictionaryType = AddingWindowDictionaryTypes[0];
            IsConfirmDictionaryAddingButtonEnabled = false;
        }
        private async Task CreateDictionary()
        {
            DictionaryType selectedDictionaryType = AddingWindowSelectedDictionaryType.DictionaryType;
            switch (selectedDictionaryType)
            {
                case DictionaryType.CommonDictionary:
                    await CreateCommonDictionary();
                    break;
                case DictionaryType.VerbPrepositionDictionary:
                    await CreateVerbPrepositionDictionary();
                    break;
            }
        }
        private async Task DeleteCommonDictionary(int commonDictionaryId)
        {
            CommonDictionaryView commonDictionaryView = FindCommonDictionaryView(commonDictionaryId);
            DictionaryViews.Remove(commonDictionaryView);
            await commonDictionaryRepository.DeleteCommonDictionary(commonDictionaryView.Id);
        }
        private async Task DeleteVerbPrepositionDictionary(int verbPrepositionDictionaryId)
        {
            VerbPrepositionDictionaryView verbPrepositionDictionaryView = FindVerbPrepositionDictionaryView(verbPrepositionDictionaryId);
            DictionaryViews.Remove(verbPrepositionDictionaryView);
            await verbPrepositionDictionaryRepository.DeleteVerbPrepositionDictionary(verbPrepositionDictionaryView.Id);
        }
        private void FlipBackAllCards()
        {
            foreach (UserControl dictionaryView in this.DictionaryViews)
            {
                if (dictionaryView is VerbPrepositionDictionaryView)
                    ((VerbPrepositionDictionaryView)dictionaryView).IsCardFlipped = false;
                if (dictionaryView is CommonDictionaryView)
                    ((CommonDictionaryView)dictionaryView).IsCardFlipped = false;
            }
        }
        private void UpdatePageForNewUser()
        {
            SetCurrentUserId();
            LoadDictionaries();
        }
        private void UpdateConfirmDictionaryAddingButtonAvailability() => this.IsConfirmDictionaryAddingButtonEnabled = ValidationPool.IsValid(ValidationRulesGroup.AddNewDictionary);
        private void OpenAddingDictionaryWindow() => AddNewDictionaryButtonSoftClick();
        #endregion

        #region Event handling
        protected override void InitEvents()
        {
            App.GetService<AppWindowVM>().CurrentPageChanged += FlipBackAllCards;
        }
        #endregion

        #region Private helpers
        private async Task CreateCommonDictionary()
        {
            string name = AddingWindowDictionaryNameValue;
            CommonDictionary newCommonDictionary = await commonDictionaryRepository.CreateCommonDictionary(name, currentUserId);
            AddCommonDictionaryViewToUI(newCommonDictionary);
        }
        private async Task CreateVerbPrepositionDictionary()
        {
            string name = AddingWindowDictionaryNameValue;
            VerbPrepositionDictionnary newVerbPrepositionDictionary = await verbPrepositionDictionaryRepository.CreateVerbPrepositionDictionary(name, currentUserId);
            AddVerbPrepositionDictionaryViewToUI(newVerbPrepositionDictionary);
        }
        private void SetAddingWindowDictionaryTypes()
        {
            ObservableCollection<DictionaryTypeComboBoxItem> dictionryTypes = new ObservableCollection<DictionaryTypeComboBoxItem>
            {
                new DictionaryTypeComboBoxItem(DictionaryTypeRussianNames.CommonDictionary, DictionaryType.CommonDictionary),
                new DictionaryTypeComboBoxItem(DictionaryTypeRussianNames.VerbPrepositionDictionary, DictionaryType.VerbPrepositionDictionary),
            };
            DictionaryTypeComboBoxItem selectedDictionaryType = dictionryTypes[0];
            AddingWindowDictionaryTypes = dictionryTypes;
            AddingWindowSelectedDictionaryType = selectedDictionaryType;
        }
        private void SetCurrentUserId()
        {
            int? currentUserId = userRepository.TryGetCurrentUser()?.Id;
            if (!currentUserId.HasValue)
                throw new Exception(ExceptionMessagesHelper.FailedToGetCurrentUserId);
            this.currentUserId = currentUserId.Value;
        }
        private void LoadDictionaries()
        {
            UserControl irregularVerbDictionaryView = IrregularVerbDictionaryView.Create();
            IEnumerable<UserControl> commonDictionaryViews = commonDictionaryRepository
                .GetUsersCommonDictionaries(this.currentUserId)
                .Select(commonDictionary => CommonDictionaryView.Create(commonDictionary));
            IEnumerable<UserControl> verbPrepositionDictionaryViews = verbPrepositionDictionaryRepository
                .GetUsersVerbPreposotionDictionaries(this.currentUserId)
                .Select(verbPrepositionDictionary => VerbPrepositionDictionaryView.Create(verbPrepositionDictionary));
            List<UserControl> allCurrentUserDictionariesViews = commonDictionaryViews.Union(verbPrepositionDictionaryViews).ToList();
            allCurrentUserDictionariesViews.Add(irregularVerbDictionaryView);
            this.DictionaryViews = new ObservableCollection<UserControl>(allCurrentUserDictionariesViews);
            AddShadowDictionaryViewToUI();
        }
        #endregion

        #region Private UI methods
        private void AddCommonDictionaryViewToUI(CommonDictionary dictionary)
        {
            RemoveShadowDictionaryViewFromUI();
            DictionaryViews.Add(CommonDictionaryView.Create(dictionary));
            AddShadowDictionaryViewToUI();
        }
        private void AddVerbPrepositionDictionaryViewToUI(VerbPrepositionDictionnary dictionnary) => DictionaryViews.Add(VerbPrepositionDictionaryView.Create(dictionnary));
        private CommonDictionaryView FindCommonDictionaryView(int commonDictionaryId)
        {
            foreach (UserControl dictionary in DictionaryViews)
                if (dictionary is CommonDictionaryView)
                {
                    CommonDictionaryView dictionaryView = (CommonDictionaryView)dictionary;
                    if (dictionaryView.Id == commonDictionaryId)
                        return dictionaryView;
                }
            throw new Exception(ExceptionMessagesHelper.NoSuchDictionaryOnUI(nameof(CommonDictionary), commonDictionaryId));
        }
        private VerbPrepositionDictionaryView FindVerbPrepositionDictionaryView(int verbPrepositionDictionaryId)
        {
            foreach (UserControl dictionary in DictionaryViews)
                if (dictionary is VerbPrepositionDictionaryView)
                {
                    VerbPrepositionDictionaryView dictionaryView = (VerbPrepositionDictionaryView)dictionary;
                    if (dictionaryView.Id == verbPrepositionDictionaryId)
                        return dictionaryView;
                }
            throw new Exception(ExceptionMessagesHelper.NoSuchDictionaryOnUI(nameof(VerbPrepositionDictionnary), verbPrepositionDictionaryId));
        }
        private void AddShadowDictionaryViewToUI() => DictionaryViews.Add(ShadowDictionaryView.Create());
        private void RemoveShadowDictionaryViewFromUI() => DictionaryViews.RemoveAt(DictionaryViews.Count - 1);
        private void AddNewDictionaryButtonSoftClick() => App.GetService<DictionariesPage>().addNewDictionaryButton.SoftClick();
        #endregion
    }
}
