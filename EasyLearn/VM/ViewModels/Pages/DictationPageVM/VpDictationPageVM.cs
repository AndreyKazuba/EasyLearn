using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.DictationManagers;
using EasyLearn.Infrastructure.Exceptions;
using EasyLearn.Infrastructure.Helpers;

namespace EasyLearn.VM.ViewModels.Pages
{
    /// <summary>
    /// Verb preposition dictation section
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

        #region Private helpers
        private void VpSetDictationManager()
        {
            int countOfVerbPrepositions = DictationLengthSliderValue;
            List<VerbPreposition> verbPrepositions = UniversalHelper.Shuffle(vpLoadedDictionary.VerbPrepositions).Take(countOfVerbPrepositions).ToList();
            vpDictationManager = new VerbPrepositionDictationManager(verbPrepositions);
        }
        #endregion

        #region Private UI methods (verb preposition dictation section)
        private void VpShowSection()
        {
            CdSectionIsVisible = false;
            VpSectionIsVisible = true;
            IvSectionIsVisible = false;
        }
        private void VpSetVerbPreposition(VerbPreposition verbPreposition)
        {
            VpSetDefaultSecondValue();
            VpMainDisplayValue = verbPreposition.Verb.Value.NormalizeRegister();
            VpTranslationValue = verbPreposition.Translation.NormalizeRegister();
        }
        #endregion

        #region Private UI methods (dictation process)
        private void VpStart()
        {
            if (vpDictationManager is null)
                throw new Exception(ExceptionMessagesHelper.DictationManagerIsNull);
            SetDefaultPageState();
            dictationIsStarted = true;
            VpSetDictationManager();
            VerbPreposition firstVerbPreposition = vpDictationManager.Start();
            VpSetVerbPreposition(firstVerbPreposition);
            SwitchStartAndStopButtons();
            VpShowSecondDisplay();
            FocusAnswerTextBox();
            SetDictationProgressBar();
        }
        private void VpCheck()
        {
            if (!dictationIsStarted || vpDictationManager is null)
                return;
            bool answerIsCorrect = vpDictationManager.IsAnswerCorrect(AnswerValue);
            if (answerIsCorrect)
            {
                VpSetSecondValue(vpDictationManager.CurrentVerbPreposition.Preposition.Value);
                VpShowCorrectIcon();
                IncreaseDictationProgressBarValue();
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
            if (!dictationIsStarted || vpDictationManager is null)
                return;
            if (vpDictationManager.GoNext())
            {
                VpSetVerbPreposition(vpDictationManager.CurrentVerbPreposition);
                SetDefaultAnswerValue();
                VpHideIcons();
                VpHidePromt();
            }
            else
                StopDictation();
        }
        #endregion

        #region Private UI methods (second display)
        private void VpSetDefaultSecondValue()
        {
            VpSecondDisplayValue = "...";
            VpSecondDisplayColor = Brushes.Black;
        }
        private void VpSetSecondValue(string value)
        {
            VpSecondDisplayValue = value;
            VpSecondDisplayColor = Brushes.ForestGreen;
        }
        private void VpHideSecondDisplay() => this.VpSecondDisplayIsVisible = false;
        private void VpShowSecondDisplay() => this.VpSecondDisplayIsVisible = true;
        #endregion

        #region Private UI methods (wrong and correct icons)
        private void VpShowWrongIcon()
        {
            VpWrongIconIsVisible = true;
            VpCorrectIconIsVisible = false;
        }
        private void VpShowCorrectIcon()
        {
            VpCorrectIconIsVisible = true;
            VpWrongIconIsVisible = false;
        }
        private void VpHideIcons()
        {
            VpCorrectIconIsVisible = false;
            VpWrongIconIsVisible = false;
        }
        #endregion

        #region Privte UI methods (promt)
        private void VpShowPromt()
        {
            if (vpDictationManager is null)
                return;
            VpSetMysteriousPromtValue(vpDictationManager.CurrentPrepositionValue);
            VpPromtIsVisible = true;
        }
        private void VpHidePromt() => VpPromtIsVisible = false;
        private void VpSetMysteriousPromtValue(string value)
        {
            int symbolsCount = value.Length;
            string mysteriousString = new string('?', symbolsCount);
            VpPromtValue = $"({mysteriousString})";
        }
        private void VpSetPromtValue(string value) => VpPromtValue = $"({value})";
        #endregion

        #region Private UI methods (translation)
        private void VpSetDefaultTranslationValue() => VpTranslationValue = string.Empty;
        #endregion
    }
}
