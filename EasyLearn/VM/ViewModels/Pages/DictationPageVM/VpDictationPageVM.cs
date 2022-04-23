using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.DictationManagers;
using EasyLearn.Infrastructure.Helpers;

namespace EasyLearn.VM.ViewModels.Pages
{
    /// <summary>
    /// Verb preposition dictation part
    /// </summary>
    public partial class DictationPageVM
    {
        #region Private fields
        private VerbPrepositionDictationManager? vpDictationManager;
        private VerbPrepositionDictionnary vpLoadedDictionary;
        #endregion

        #region Binding props
        public Brush VpSecondDisplayColor { get; set; }
        public string VpMainDisplayValue { get; set; }
        public string VpSecondDisplayValue { get; set; }
        public string VpTranslationValue { get; set; }
        public string VpCommentValue { get; set; }
        public string VpPromtValue { get; set; }
        public bool VpCorrectIconIsVisible { get; set; }
        public bool VpWrongIconIsVisible { get; set; }
        public bool VpSectionIsVisible { get; set; }
        public bool VpPromtIsVisible { get; set; }
        public bool VpSecondDisplayIsVisible { get; set; }
        #endregion

        private void VpShowSection()
        {
            this.CdSectionIsVisible = false;
            this.VpSectionIsVisible = true;
            this.IvSectionIsVisible = false;
        }
        private void VpSetDefaultTranslationValue() => this.VpTranslationValue = String.Empty;
        private void VpSetVerbPreposition(VerbPreposition verbPreposition)
        {
            VpSetDefaultSecondValue();
            this.VpMainDisplayValue = verbPreposition.Verb.Value.NormalizeRegister();
            this.VpTranslationValue = verbPreposition.Translation.NormalizeRegister();
            this.VpCommentValue = StringHelper.EmptyIfNull(verbPreposition.Comment);
        }
        private void VpSetDictationManager()
        {
            int countOfVerbPrepositions = this.DictationLengthSliderValue;
            List<VerbPreposition> verbPrepositions = UniversalHelper.Shuffle(this.vpLoadedDictionary.VerbPrepositions).Take(countOfVerbPrepositions).ToList();
            this.vpDictationManager = new VerbPrepositionDictationManager(verbPrepositions);
        }

        #region Dictation
#pragma warning disable CS8602
        private void VpStart()
        {
            SetDefaultPageState();
            this.dictationIsStarted = true;
            VpSetDictationManager();
            VerbPreposition firstVerbPreposition = vpDictationManager.Start();
            VpSetVerbPreposition(firstVerbPreposition);
            SwitchStartAndStopButtons();
            VpShowSecondDisplay();
            FocusAnswerTextBox();
            SetDictationProgressBar();
        }
#pragma warning restore CS8602
        private void VpCheck()
        {
            if (!dictationIsStarted || vpDictationManager is null)
                return;
            bool answerIsCorrect = vpDictationManager.IsAnswerCorrect(this.AnswerValue);
            if (answerIsCorrect)
            {
                VpSetSecondValue(vpDictationManager.CurrentVerbPreposition.Preposition.Value);
                VpShowCorrectIcon();
                IncreaseDictationProgressBarValue();
                SwitchCheckAndNextButtons();
                SetDefaultAnswerValue();
                VpHidePromt();
                wrongAnswers = 0;
            }
            else
            {
                VpShowWrongIcon();
                if (++wrongAnswers > 2)
                    VpShowPromt();
            }
        }
        private void VpTryGoNext()
        {
            if (!this.dictationIsStarted || this.vpDictationManager is null)
                return;
            if (this.vpDictationManager.GoNext())
            {
                VpSetVerbPreposition(vpDictationManager.CurrentVerbPreposition);
                SetDefaultAnswerValue();
                VpHideIcons();
                VpHidePromt();
                SwitchCheckAndNextButtons();
            }
            else
            {
                StopDictation();
            }
        }
        #endregion

        #region Second display
        private void VpSetDefaultSecondValue()
        {
            this.VpSecondDisplayValue = "...";
            this.VpSecondDisplayColor = Brushes.Black;
        }
        private void VpSetSecondValue(string value)
        {
            this.VpSecondDisplayValue = value;
            this.VpSecondDisplayColor = Brushes.ForestGreen;
        }
        private void VpHideSecondDisplay() => this.VpSecondDisplayIsVisible = false;
        private void VpShowSecondDisplay() => this.VpSecondDisplayIsVisible = true;
        #endregion

        #region Icons
        private void VpShowWrongIcon()
        {
            this.VpWrongIconIsVisible = true;
            this.VpCorrectIconIsVisible = false;
        }
        private void VpShowCorrectIcon()
        {
            this.VpCorrectIconIsVisible = true;
            this.VpWrongIconIsVisible = false;
        }
        private void VpHideIcons()
        {
            this.VpCorrectIconIsVisible = false;
            this.VpWrongIconIsVisible = false;
        }
        #endregion

        #region Promt
        private void VpShowPromt()
        {
            if (vpDictationManager is null)
                return;
            VpSetMysteriousPromtValue(vpDictationManager.CurrentPrepositionValue);
            this.VpPromtIsVisible = true;
        }
        private void VpHidePromt() => this.VpPromtIsVisible = false;
        private void VpSetMysteriousPromtValue(string value)
        {
            int symbolsCount = value.Length;
            string mysteriousString = new string('?', symbolsCount);
            this.VpPromtValue = $"({mysteriousString})";
        }
        private void VpSetPromtValue(string value)
        {
            this.VpPromtValue = $"({value})";
        }
        #endregion
    }
}
