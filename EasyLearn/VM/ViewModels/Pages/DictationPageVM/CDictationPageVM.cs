using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.DictationManagers;
using EasyLearn.Infrastructure.Enums;
using EasyLearn.UI.CustomControls;
using EasyLearn.Infrastructure.Exceptions;
using EasyLearn.Infrastructure.Helpers;
using System.Windows;

namespace EasyLearn.VM.ViewModels.Pages
{
    /// <summary>
    /// Common dictation section
    /// </summary>
    public partial class DictationPageVM
    {
        #region Private fields
        private CommonDictationManager? commonDictationManager;
        private CommonDictionary cdLoadedDictionary;
        #endregion

        #region Binding props
        public Brush CdUnitTypeColor { get; set; }
        public ObservableCollection<SynonymView> CdAnotherAnswerViews { get; set; }
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
        public bool CdCommentIsVisile { get; set; }
        public string? CdFirstExampleValue { get; set; }
        public string? CdSecondExampleValue { get; set; }
        #endregion

        #region Private helpers
        private void CdSetDictationManager()
        {
            int countOfRelations = DictationLengthSliderValue.OneIfZero();
            List<CommonRelation> commonRelations = cdLoadedDictionary.Relations;
            commonDictationManager = CommonDictationManager.CreateManager(commonRelations, countOfRelations, SelectedDictationDirection);
        }
        #endregion

        #region Private UI methods (stop window)
        private void CdSetStopWindow()
        {
            if (commonDictationManager is null)
                throw new Exception(ExceptionMessagesHelper.DictationManagerIsNull);
            DictationWordsCount = commonDictationManager.TotalRelationsCount.ToString();
            DictationAnswersCount = commonDictationManager.AnswersCount.ToString();
            DictationWrongAnswersCount = commonDictationManager.WrongAnswersCount.ToString();
            Grade = commonDictationManager.AnswersCount != 0
                ? ((int)((commonDictationManager.AnswersCount - commonDictationManager.WrongAnswersCount) * (100d / commonDictationManager.AnswersCount))).ToString()
                : "0";
            GradeForeground = Convert.ToInt32(Grade).GetColorForGrade();
        }
        #endregion

        #region Private UI methods (common dictation section)
        private void CdSetRelation(CommonRelation relation)
        {
            if (SelectedDictationDirection == DictationDirection.Directly)
                CdSetRelationWithDirecltyDirection(relation);
            else
                CdSetRelationWithOpposteDirection(relation);
        }
        private void CdSetRelationWithDirecltyDirection(CommonRelation relation)
        {
            CdMainDisplayValue = relation.RussianUnit.Value.NormalizeRegister();
            CdUnitTypeValue = relation.RussianUnit.Type.GetRussianValue();
            CdUnitTypeColor = relation.RussianUnit.Type.GetColor();
            CdCommentValue = relation.Comment.TryNormalizeRegister();
            CdCommentIsVisile = !string.IsNullOrEmpty(relation.Comment);
            if (relation.IsFirstExampleExist)
            {
                CdFirstExampleValue = relation.FirstExampleRussianValue.TryNormalizeRegister();
                CdSecondExampleValue = relation.SecondExampleRussianValue.TryNormalizeRegister();
            }
            else
            {
                CdFirstExampleValue = relation.SecondExampleRussianValue.TryNormalizeRegister();
                CdSecondExampleValue = string.Empty;
            }
        }
        private void CdSetRelationWithOpposteDirection(CommonRelation relation)
        {
            CdMainDisplayValue = relation.EnglishUnit.Value.NormalizeRegister();
            CdUnitTypeValue = relation.EnglishUnit.Type.GetRussianValue();
            CdUnitTypeColor = relation.EnglishUnit.Type.GetColor();
            CdCommentIsVisile = false;
            if (relation.IsFirstExampleExist)
            {
                CdFirstExampleValue = relation.FirstExampleEnglishValue.TryNormalizeRegister();
                CdSecondExampleValue = relation.SecondExampleEnglishValue.TryNormalizeRegister();
            }
            else
            {
                CdFirstExampleValue = relation.SecondExampleEnglishValue.TryNormalizeRegister();
                CdSecondExampleValue = string.Empty;
            }
        }
        private void CdShowSection()
        {
            CdSectionIsVisible = true;
            VpSectionIsVisible = false;
            IvSectionIsVisible = false;
        }
        #endregion

        #region Private UI methods (dictation process)
        private void CdStart()
        {
            SetDefaultPageState();
            dictationIsStarted = true;
            CdSetDictationManager();
            CommonRelation firstCommonRelation = commonDictationManager?.Start() ?? throw new Exception(ExceptionMessagesHelper.DictationManagerIsNull);
            CdSetRelation(firstCommonRelation);
            CdShowUnitType();
            SwitchStartAndStopButtons();
            FocusAnswerTextBox();
            SetDictationProgressBar();
        }
        private void CdCheck()
        {
            if (!dictationIsStarted || commonDictationManager is null)
                return;
            bool answerIsCorrect = commonDictationManager.IsAnswerCorrect(AnswerValue);
            if (answerIsCorrect)
            {
                if (commonDictationManager.CurrentRelationHasSynonyms)
                    CdShowSynonyms(commonDictationManager.SynonymRelations, AnswerValue);
                SetAnswerTextBoxAsCorrect();
                IncreaseDictationProgressBarValue();
                CdHidePromt();
                wrongAnswers = 0;
                currentAnswerIsCorrect = true;
            }
            else
            {
                SetAnswerTextBoxAsWrong();
                currentAnswerIsCorrect = false;
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
                CdHideSynonyms();
                currentAnswerIsCorrect = false;
                SetAnswerTextBoxAsDefault();
                CdHidePromt();
            }
            else
                StopDictationButtonSoftClick();
        }
        #endregion

        #region Private UI methods (unit type label)
        private void CdShowUnitType() => CdUnitTypeIsVisible = true;
        private void CdHideUnitType() => CdUnitTypeIsVisible = false;
        #endregion

        #region Private UI methods (synonyms)
        private void CdShowSynonyms(IEnumerable<CommonRelation> commonRelations, string asnwerValue)
        {
            CdAnotherAnswersIsVisible = true;
            IEnumerable<SynonymView> synonymsViews = GetSynonymViews(commonRelations, asnwerValue);
            CdAnotherAnswerViews = new ObservableCollection<SynonymView>(synonymsViews);
        }
        private IEnumerable<SynonymView> GetSynonymViews(IEnumerable<CommonRelation> commonRelations, string asnwerValue)
        {
            if (SelectedDictationDirection == DictationDirection.Directly)
            {
                return commonRelations
                .Where(relation => !StringHelper.Equals(relation.EnglishUnit.Value, asnwerValue))
                .Select(relation => SynonymView.Create(relation, SelectedDictationDirection));
            }
            else
            {
                return commonRelations
                .Where(relation => !StringHelper.Equals(relation.RussianUnit.Value, asnwerValue))
                .Select(relation => SynonymView.Create(relation, SelectedDictationDirection));
            }
        }
        private void CdHideSynonyms()
        {
            CdAnotherAnswersIsVisible = false;
            if (CdAnotherAnswerViews is not null)
                CdAnotherAnswerViews.Clear();
        }
        #endregion

        #region Private UI methods (promt)
        private void CdShowPromt()
        {
            if (commonDictationManager is null)
                return;
            CdSetMysteriousPromtValue(this.commonDictationManager.CurrentAnswerValue);
            CdPromtIsVisible = true;
        }
        private void CdHidePromt() => CdPromtIsVisible = false;
        private void CdSetMysteriousPromtValue(string value)
        {
            int symbolsCount = value.Length;
            string mysteriousString = new string('?', symbolsCount);
            CdPromtValue = $"({mysteriousString})";
        }
        private void CdSetPromtValue(string value) => CdPromtValue = $"({value})";
        #endregion

        #region Private UI methods (examples)
        private void CdHideExamples()
        {
            CdFirstExampleValue = string.Empty;
            CdSecondExampleValue = string.Empty;
        }
        #endregion
    }
}
