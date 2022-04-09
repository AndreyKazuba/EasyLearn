﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.DictationManagers;
using EasyLearn.Infrastructure.Helpers;
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
        #endregion

        private void CdSetRelation(CommonRelation relation)
        {
            this.CdMainDisplayValue = relation.RussianUnit.Value.NormalizeRegister();
            this.CdCommentValue = relation.Comment.TryNormalizeRegister();
            this.CdUnitTypeValue = relation.RussianUnit.Type.GetRussianValue();
            this.CdUnitTypeColor = relation.RussianUnit.Type.GetColor();
        }
        private void CdSetDictationManager()
        {
            int countOfRelations = this.DictationLengthSliderValue;
            List<CommonRelation> commonRelations = this.cdLoadedDictionary.Relations;
            this.commonDictationManager = new CommonDictationManager(commonRelations, countOfRelations);
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
                CdShowCorrectIcon();
                IncreaseDictationProgressBarValue();
                SwitchCheckAndNextButtons();
                CdHidePromt();
                wrongAnswers = 0;
            }
            else
            {
                CdShowWrongIcon();
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
                CdHideIcons();
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
            IEnumerable<AvailableRelationView> anotherAnswerViews = commonRelations
                .Where(relation => !StringHelper.Equals(relation.EnglishUnit.Value, asnwerValue))
                .Select(relation => AvailableRelationView.Create(relation));
            this.CdAnotherAnswerViews = new ObservableCollection<AvailableRelationView>(anotherAnswerViews);
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
            CdSetMysteriousPromtValue(this.commonDictationManager.CurrentEnglishValue);
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
    }
}
