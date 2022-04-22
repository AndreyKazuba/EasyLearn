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

#pragma warning disable CS8618
        public DictionariesPageVM
            (IEasyLearnUserRepository userRerository,
            ICommonDictionaryRepository commonDictionaryRepository,
            IVerbPrepositionDictionaryRepository erbPrepositionDictionaryRepository)
        {
            this.userRepository = userRerository;
            this.commonDictionaryRepository = commonDictionaryRepository;
            this.verbPrepositionDictionaryRepository = erbPrepositionDictionaryRepository;
            SubscribeToEvents();
            UpdatePageForNewUser();
            SetAddingWindowDictionaryTypes();
            ClearAddingWindow();
        }
#pragma warning restore CS8618


        #region Props for binding
        public ObservableCollection<UserControl> DictionaryViews { get; set; }
        public string AddingWindowDictionaryNameValue { get; set; }
        public string AddingWindowDictionaryDescriptionValue { get; set; }
        public ObservableCollection<DictionaryTypeComboBoxItem> AddingWindowDictionaryTypes { get; set; }
        public DictionaryTypeComboBoxItem AddingWindowSelectedDictionaryType { get; set; }
        public bool IsConfirmDictionaryAddingButtonEnabled { get; set; }

        #endregion

        #region Commands
        public Command ClearAddingWindowCommand { get; private set; }
        public Command CreateDictionaryCommand { get; private set; }
        public Command<int> DeleteCommonDictionaryCommand { get; private set; }
        public Command<int> DeleteVerbPrepositionDictionaryCommand { get; private set; }
        public Command FlipBackAllCardsCommand { get; private set; }
        public Command UpdatePageForNewUserCommand { get; private set; }
        public Command UpdateConfirmDictionaryAddingButtonAvailabilityCommand { get; private set; }
        protected override void InitCommands()
        {
            this.ClearAddingWindowCommand = new Command(ClearAddingWindow);
            this.CreateDictionaryCommand = new Command(async () => await CreateDictionary());
            this.DeleteCommonDictionaryCommand = new Command<int>(async dictionaryId => await DeleteCommonDictionary(dictionaryId));
            this.DeleteVerbPrepositionDictionaryCommand = new Command<int>(async dictionaryId => await DeleteVerbPrepositionDictionary(dictionaryId));
            this.FlipBackAllCardsCommand = new Command(FlipBackAllCards);
            this.UpdatePageForNewUserCommand = new Command(UpdatePageForNewUser);
            this.UpdateConfirmDictionaryAddingButtonAvailabilityCommand = new Command(UpdateConfirmDictionaryAddingButtonAvailability);
        }
        #endregion

        #region Command logic methods
        private void ClearAddingWindow()
        {
            this.AddingWindowDictionaryNameValue = String.Empty;
            this.AddingWindowDictionaryDescriptionValue = String.Empty;
            this.AddingWindowSelectedDictionaryType = this.AddingWindowDictionaryTypes[0];
            this.IsConfirmDictionaryAddingButtonEnabled = false;
        }
        private async Task CreateDictionary()
        {
            DictionaryType selectedDictionaryType = this.AddingWindowSelectedDictionaryType.DictionaryType;
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
            this.DictionaryViews.Remove(commonDictionaryView);
            await commonDictionaryRepository.DeleteCommonDictionary(commonDictionaryView.Id);
        }
        private async Task DeleteVerbPrepositionDictionary(int verbPrepositionDictionaryId)
        {
            VerbPrepositionDictionaryView verbPrepositionDictionaryView = FindVerbPrepositionDictionaryView(verbPrepositionDictionaryId);
            this.DictionaryViews.Remove(verbPrepositionDictionaryView);
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
        #endregion

        #region Other private methods
        private async Task CreateCommonDictionary()
        {
            string name = this.AddingWindowDictionaryNameValue;
            string? description = StringHelper.NullIfEmptyOrWhiteSpace(this.AddingWindowDictionaryDescriptionValue);
            int userId = this.currentUserId;
            CommonDictionary newCommonDictionary = await commonDictionaryRepository.CreateCommonDictionary(name, description, userId);
            AddCommonDictionaryToUI(newCommonDictionary);
        }
        private async Task CreateVerbPrepositionDictionary()
        {
            string name = this.AddingWindowDictionaryNameValue;
            string? description = StringHelper.NullIfEmptyOrWhiteSpace(this.AddingWindowDictionaryDescriptionValue);
            int userId = this.currentUserId;
            VerbPrepositionDictionnary newVerbPrepositionDictionary = await verbPrepositionDictionaryRepository.CreateVerbPrepositionDictionary(name, description, userId);
            AddVerbPrepositionDictionaryToUI(newVerbPrepositionDictionary);
        }
        private void AddCommonDictionaryToUI(CommonDictionary dictionary) => this.DictionaryViews.Add(CommonDictionaryView.Create(dictionary));
        private void AddVerbPrepositionDictionaryToUI(VerbPrepositionDictionnary dictionnary) => this.DictionaryViews.Add(VerbPrepositionDictionaryView.Create(dictionnary));
        private CommonDictionaryView FindCommonDictionaryView(int commonDictionaryId)
        {
            foreach (UserControl dictionary in this.DictionaryViews)
                if (dictionary is CommonDictionaryView)
                {
                    CommonDictionaryView dictionaryView = (CommonDictionaryView)dictionary;
                    if (dictionaryView.Id == commonDictionaryId)
                        return dictionaryView;
                }
            throw new Exception($"На UI нет {nameof(CommonDictionary)} с Id = {commonDictionaryId}");
        }
        private VerbPrepositionDictionaryView FindVerbPrepositionDictionaryView(int verbPrepositionDictionaryId)
        {
            foreach (UserControl dictionary in this.DictionaryViews)
                if (dictionary is VerbPrepositionDictionaryView)
                {
                    VerbPrepositionDictionaryView dictionaryView = (VerbPrepositionDictionaryView)dictionary;
                    if (dictionaryView.Id == verbPrepositionDictionaryId)
                        return dictionaryView;
                }
            throw new Exception($"На UI нет {nameof(VerbPrepositionDictionnary)} с Id = {verbPrepositionDictionaryId}");
        }
        private void SetAddingWindowDictionaryTypes()
        {
            ObservableCollection<DictionaryTypeComboBoxItem> dictionryTypes = new ObservableCollection<DictionaryTypeComboBoxItem>
            {
                new DictionaryTypeComboBoxItem(DictionaryTypeRussianNames.CommonDictionary, DictionaryType.CommonDictionary),
                new DictionaryTypeComboBoxItem(DictionaryTypeRussianNames.VerbPrepositionDictionary, DictionaryType.VerbPrepositionDictionary),
            };
            DictionaryTypeComboBoxItem selectedDictionaryType = dictionryTypes[0];
            this.AddingWindowDictionaryTypes = dictionryTypes;
            this.AddingWindowSelectedDictionaryType = selectedDictionaryType;
        }
        private void SetCurrentUserId()
        {
            int? currentUserId = userRepository.TryGetCurrentUser()?.Id;
            if (!currentUserId.HasValue)
                throw new Exception("Не удалось получить из базы текущего пользователя");
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
        }
        private void SubscribeToEvents() => App.GetService<AppWindowVM>().CurrentPageChanged += () => FlipBackAllCards();
        #endregion
    }
}
