using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.DictationManagers;
using EasyLearn.Infrastructure.Enums;
using EasyLearn.UI.CustomControls;

namespace EasyLearn.VM.ViewModels.Pages
{
    /// <summary>
    /// Common dictation part
    /// </summary>
    public partial class DictationPageVM
    {
        #region Private fields
        private CommonDictationManager? commonDictationManager;
        private CommonDictionary cdLoadedDictionary;
        #endregion

        #region Binding props
        public Brush CdUnitTypeColor { get; set; }
        public ObservableCollection<AvailableRelationView> CdAnotherAnswerViews { get; set; }
        public DictationDirection SelectedDictationDirection { get; set; }
        public string CdMainDisplayValue { get; set; }
        public string CdUnitTypeValue { get; set; }
        public string CdCommentValue { get; set; }
        public string CdPromtValue { get; set; }
        public bool CdSectionIsVisible { get; set; }
        public bool CdUnitTypeIsVisible { get; set; }
        public bool CdPromtIsVisible { get; set; }
        public bool CdCorrectIconIsVisible { get; set; }
        public bool CdWrongIconIsVisible { get; set; }
        public bool CdAnotherAnswersIsVisible { get; set; }
        public string? CdFirstExampleValue { get; set; }
        public string? CdSecondExampleValue { get; set; }

        #endregion

        private void CdSetRelation(CommonRelation relation)
        {
            bool firstExampleExist = relation.IsFirstExampleExist;
            if (SelectedDictationDirection == DictationDirection.Directly)
            {
                this.CdMainDisplayValue = relation.RussianUnit.Value.NormalizeRegister();
                this.CdUnitTypeValue = relation.RussianUnit.Type.GetRussianValue();
                this.CdUnitTypeColor = relation.RussianUnit.Type.GetColor();
                if (firstExampleExist)
                {
                    this.CdFirstExampleValue = relation.FirstExampleRussianValue.TryNormalizeRegister();
                    this.CdSecondExampleValue = relation.SecondExampleRussianValue.TryNormalizeRegister();
                }
                else
                {
                    this.CdFirstExampleValue = relation.SecondExampleRussianValue.TryNormalizeRegister();
                }
            }
            else
            {
                this.CdMainDisplayValue = relation.EnglishUnit.Value.NormalizeRegister();;
                this.CdUnitTypeValue = relation.EnglishUnit.Type.GetRussianValue();
                this.CdUnitTypeColor = relation.EnglishUnit.Type.GetColor();
                if (firstExampleExist)
                {
                    this.CdFirstExampleValue = relation.FirstExampleEnglishValue.TryNormalizeRegister();
                    this.CdSecondExampleValue = relation.SecondExampleEnglishValue.TryNormalizeRegister();
                }
                else
                {
                    this.CdFirstExampleValue = relation.SecondExampleEnglishValue.TryNormalizeRegister();
                }
                
            }
            this.CdCommentValue = relation.Comment.TryNormalizeRegister();
        }
        private void CdSetDictationManager()
        {
            int countOfRelations = this.DictationLengthSliderValue;
            List<CommonRelation> commonRelations = this.cdLoadedDictionary.Relations;
            this.commonDictationManager = CommonDictationManager.CreateManager(commonRelations, countOfRelations, SelectedDictationDirection);
        }
        private void CdShowSection()
        {
            this.CdSectionIsVisible = true;
            this.VpSectionIsVisible = false;
            this.IvSectionIsVisible = false;
        }

        #region Dictation
#pragma warning disable CS8602
        private void CdStart()
        {
            SetDefaultPageState();
            this.dictationIsStarted = true;
            CdSetDictationManager();
            CommonRelation firstCommonRelation = commonDictationManager.Start();
            CdSetRelation(firstCommonRelation);
            CdShowUnitType();
            SwitchStartAndStopButtons();
            FocusAnswerTextBox();
            SetDictationProgressBar();
        }
#pragma warning restore CS8602
        private void CdCheck()
        {
            if (!dictationIsStarted || commonDictationManager is null)
                return;
            bool answerIsCorrect = commonDictationManager.IsAnswerCorrect(this.AnswerValue);
            if (answerIsCorrect)
            {
                if (commonDictationManager.CurrentRelationHasSynonyms)
                    CdShowAnotherAnswers(commonDictationManager.AvailableRelations, this.AnswerValue);
                //CdShowCorrectIcon();
                SetAnswerTextBoxAsCorrect();
                IncreaseDictationProgressBarValue();
                SwitchCheckAndNextButtons();
                CdHidePromt();
                wrongAnswers = 0;
            }
            else
            {
                //CdShowWrongIcon();
                SetAnswerTextBoxAsWrong();
                if (++wrongAnswers > 2)
                    CdShowPromt();
            }
        }
        private void CdTryGoNext()
        {
            if (!dictationIsStarted || commonDictationManager is null)
                return;
            if (commonDictationManager.GoNext())
            {
                CdSetRelation(commonDictationManager.CurrentRelation);
                SetDefaultAnswerValue();
                CdHideAnotherAnswers();
                //CdHideIcons();
                SetAnswerTextBoxAsDefault();
                CdHidePromt();
                SwitchCheckAndNextButtons();
            }
            else
            {
                StopDictation();
            }
        }
        #endregion

        #region Unit type
        private void CdShowUnitType()
        {
            this.CdUnitTypeIsVisible = true;
        }
        private void CdHideUnitType()
        {
            this.CdUnitTypeIsVisible = false;
        }
        #endregion

        #region Another asnwers
        private void CdShowAnotherAnswers(IEnumerable<CommonRelation> commonRelations, string asnwerValue)
        {
            this.CdAnotherAnswersIsVisible = true;
            IEnumerable<AvailableRelationView> anotherAnswerViews = GetAnotherAnswerViews(commonRelations, asnwerValue);
            this.CdAnotherAnswerViews = new ObservableCollection<AvailableRelationView>(anotherAnswerViews);
        }
        private IEnumerable<AvailableRelationView> GetAnotherAnswerViews(IEnumerable<CommonRelation> commonRelations, string asnwerValue)
        {
            if (SelectedDictationDirection == DictationDirection.Directly)
            {
                return commonRelations
                .Where(relation => !StringHelper.Equals(relation.EnglishUnit.Value, asnwerValue))
                .Select(relation => AvailableRelationView.Create(relation, SelectedDictationDirection));
            }
            else
            {
                return commonRelations
                .Where(relation => !StringHelper.Equals(relation.RussianUnit.Value, asnwerValue))
                .Select(relation => AvailableRelationView.Create(relation, SelectedDictationDirection));
            }
        }
        private void CdHideAnotherAnswers()
        {
            this.CdAnotherAnswersIsVisible = false;
            if (this.CdAnotherAnswerViews is not null)
                this.CdAnotherAnswerViews.Clear();
        }
        #endregion

        #region Icons
        private void CdShowWrongIcon()
        {
            this.CdWrongIconIsVisible = true;
            this.CdCorrectIconIsVisible = false;
        }
        private void CdShowCorrectIcon()
        {
            this.CdCorrectIconIsVisible = true;
            this.CdWrongIconIsVisible = false;
        }
        private void CdHideIcons()
        {
            this.CdCorrectIconIsVisible = false;
            this.CdWrongIconIsVisible = false;
        }
        #endregion

        #region Promt
        private void CdShowPromt()
        {
            if (this.commonDictationManager is null)
                return;
            CdSetMysteriousPromtValue(this.commonDictationManager.CurrentAnswerValue);
            this.CdPromtIsVisible = true;
        }
        private void CdHidePromt() => this.CdPromtIsVisible = false;
        private void CdSetMysteriousPromtValue(string value)
        {
            int symbolsCount = value.Length;
            string mysteriousString = new string('?', symbolsCount);
            this.CdPromtValue = $"({mysteriousString})";
        }
        private void CdSetPromtValue(string value)
        {
            this.CdPromtValue = $"({value})";
        }
        #endregion

        private void CdHideExamples()
        {
            this.CdFirstExampleValue = string.Empty;
            this.CdSecondExampleValue = string.Empty;
        }
    }
}
