using System;
using System.Collections.Generic;
using System.Linq;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.DictationManagers;
using EasyLearn.Data.Enums;
using EasyLearn.Infrastructure.Helpers;
using EasyLearn.Infrastructure.Exceptions;

namespace EasyLearn.VM.ViewModels.Pages
{
    /// <summary>
    /// Irregular verb dictation section
    /// </summary>
    public partial class DictationPageVM
    {
        #region Private fields
        private IrregularVerbDictationManager? ivDictationManager;
        private IrregularVerbForm currentIrregularVerbForm;
        #endregion

        #region Binding props
        public string IvFirstFormFixedAnswerValue { get; set; }
        public string IvSecondFormFixedAnswerValue { get; set; }
        public string IvThirdFormFixedAnswerValue { get; set; }
        public string IvMainDisplayValue { get; set; }
        public string IvCommentValue { get; set; }
        public string IvPromtValue { get; set; }
        public bool IvSectionIsVisible { get; set; }
        public bool IvFirstFormGrayIconIsVisible { get; set; }
        public bool IvFirstFormCorrectIconIsVisible { get; set; }
        public bool IvFirstFormWrongIconIsVisible { get; set; }
        public bool IvSecondFormGrayIconIsVisible { get; set; }
        public bool IvSecondFormCorrectIconIsVisible { get; set; }
        public bool IvSecondFormWrongIconIsVisible { get; set; }
        public bool IvThirdFormGrayIconIsVisible { get; set; }
        public bool IThirdFormCorrectIconIsVisible { get; set; }
        public bool IvThirdFormWrongIconIsVisible { get; set; }
        public bool IvPromtIsVisible { get; set; }
        #endregion

        #region Private helpers
        private void IvSetDictationManager()
        {
            int countOfIrregularVerbs = this.DictationLengthSliderValue;
            List<IrregularVerb> irregularVerbs = UniversalHelper.Shuffle(irregularVerbRepository.GetAllIrregularVerbs()).Take(countOfIrregularVerbs).ToList();
            ivDictationManager = new IrregularVerbDictationManager(irregularVerbs);
        }
        #endregion

        #region Private UI methods (irregular verb dictation section)
        private void IvShowSection()
        {
            CdSectionIsVisible = false;
            VpSectionIsVisible = false;
            IvSectionIsVisible = true;
        }
        private void IvSetIrregularVerb(IrregularVerb irregularVerb)
        {
            IvMainDisplayValue = irregularVerb.RussianUnit.Value.NormalizeRegister();
            IvCommentValue = irregularVerb.Comment.TryNormalizeRegister();
        }
        #endregion

        #region Private UI methods (fixes answers)
        private void IvSetFixedAnswerValues(IrregularVerb irregularVerb)
        {
            IvFirstFormFixedAnswerValue = StringHelper.ReplaceString(irregularVerb.FirstForm.Value, '?');
            IvSecondFormFixedAnswerValue = StringHelper.ReplaceString(irregularVerb.SecondForm.Value, '?');
            IvThirdFormFixedAnswerValue = StringHelper.ReplaceString(irregularVerb.ThirdForm.Value, '?');
        }
        private void IvSetDefaultFixedAnswerValues()
        {
            string mysteriousString = "????";
            IvFirstFormFixedAnswerValue = mysteriousString;
            IvSecondFormFixedAnswerValue = mysteriousString;
            IvThirdFormFixedAnswerValue = mysteriousString;
        }
        #endregion

        #region Private UI methods (dictation process)
        private void IvStart()
        {
            if (ivDictationManager is null)
                throw new Exception(ExceptionMessagesHelper.DictationManagerIsNull);
            SetDefaultPageState();
            dictationIsStarted = true;
            IvSetDictationManager();
            IrregularVerb firstIrregularVerb = ivDictationManager.Start();
            IvSetIrregularVerb(firstIrregularVerb);
            SwitchStartAndStopButtons();
            FocusAnswerTextBox();
            SetDictationProgressBar();
        }
        private void IvCheck()
        {
            if (!dictationIsStarted || ivDictationManager is null)
                return;
            ExecuteForCurrentIrregularVerbForm(IvCheckForFirstForm, IvCheckForSecondForm, IvCheckForThirdForm);
        }
        private void IvCheckForFirstForm()
        {
            if (ivDictationManager is null)
                throw new Exception(ExceptionMessagesHelper.DictationManagerIsNull);
            bool firstFormIsCorrect = ivDictationManager.IsV1AnswerCorrect(AnswerValue);
            if (firstFormIsCorrect)
            {
                IvShowFirstFormCorrectIcon();
                IncreaseDictationProgressBarValue();
                IvHidePromt();
                IvFirstFormFixedAnswerValue = StringHelper.NormalizeRegister(AnswerValue);
                currentIrregularVerbForm = IrregularVerbForm.SecondForm;
                SetDefaultAnswerValue();
                wrongAnswers = 0;
            }
            else
            {
                IvShowFirstFormWrongIcon();
                if (++wrongAnswers > 2)
                    IvShowPromt();
            }
        }
        private void IvCheckForSecondForm()
        {
            if (ivDictationManager is null)
                throw new Exception(ExceptionMessagesHelper.DictationManagerIsNull);
            bool secondFormIsCorrect = ivDictationManager.IsV2AnswerCorrect(AnswerValue);
            if (secondFormIsCorrect)
            {
                IvShowSecondFormCorrectIcon();
                IncreaseDictationProgressBarValue();
                IvHidePromt();
                IvSecondFormFixedAnswerValue = StringHelper.NormalizeRegister(AnswerValue);
                currentIrregularVerbForm = IrregularVerbForm.ThirdForm;
                SetDefaultAnswerValue();
                wrongAnswers = 0;
            }
            else
            {
                IvShowSecondFormWrongIcon();
                if (++wrongAnswers > 2)
                    IvShowPromt();
            }
        }
        private void IvCheckForThirdForm()
        {
            if (ivDictationManager is null)
                throw new Exception(ExceptionMessagesHelper.DictationManagerIsNull);
            bool thirdFormIsCorrect = ivDictationManager.IsV3AnswerCorrect(AnswerValue);
            if (thirdFormIsCorrect)
            {
                IvShowThirdFormCorrectIcon();
                IncreaseDictationProgressBarValue();
                IvHidePromt();
                IvThirdFormFixedAnswerValue = StringHelper.NormalizeRegister(AnswerValue);
                SetDefaultAnswerValue();
                currentIrregularVerbForm = IrregularVerbForm.FirstForm;
                wrongAnswers = 0;
            }
            else
            {
                IvShowThirdFormWrongIcon();
                if (++wrongAnswers > 2)
                    IvShowPromt();
            }
        }
        private void IvTryGoNext()
        {
            if (!dictationIsStarted || ivDictationManager is null)
                return;
            if (ivDictationManager.GoNext())
            {
                IvSetIrregularVerb(ivDictationManager.CurrentIrregularVerb);
                SetDefaultAnswerValue();
                IvShowGrayIcons();
                IvHidePromt();
                IvSetFixedAnswerValues(ivDictationManager.CurrentIrregularVerb);
            }
            else
                StopDictation();
        }
        #endregion

        #region Private UI methods (wrong and correct icons)
        private void IvShowFirstFormCorrectIcon()
        {
            IvFirstFormGrayIconIsVisible = false;
            IvFirstFormCorrectIconIsVisible = true;
            IvFirstFormWrongIconIsVisible = false;
        }
        private void IvShowFirstFormWrongIcon()
        {
            IvFirstFormGrayIconIsVisible = false;
            IvFirstFormCorrectIconIsVisible = false;
            IvFirstFormWrongIconIsVisible = true;
        }
        private void IvShowSecondFormCorrectIcon()
        {
            IvSecondFormGrayIconIsVisible = false;
            IvSecondFormCorrectIconIsVisible = true;
            IvSecondFormWrongIconIsVisible = false;
        }
        private void IvShowSecondFormWrongIcon()
        {
            IvSecondFormGrayIconIsVisible = false;
            IvSecondFormCorrectIconIsVisible = false;
            IvSecondFormWrongIconIsVisible = true;
        }
        private void IvShowThirdFormCorrectIcon()
        {
            IvThirdFormGrayIconIsVisible = false;
            IThirdFormCorrectIconIsVisible = true;
            IvThirdFormWrongIconIsVisible = false;
        }
        private void IvShowThirdFormWrongIcon()
        {
            IvThirdFormGrayIconIsVisible = false;
            IThirdFormCorrectIconIsVisible = false;
            IvThirdFormWrongIconIsVisible = true;
        }
        private void IvShowGrayIcons()
        {
            IvHideAllIcons();
            IvFirstFormGrayIconIsVisible = true;
            IvSecondFormGrayIconIsVisible = true;
            IvThirdFormGrayIconIsVisible = true;
        }
        private void IvHideAllIcons()
        {
            IvFirstFormGrayIconIsVisible = false;
            IvSecondFormGrayIconIsVisible = false;
            IvThirdFormGrayIconIsVisible = false;
            IvFirstFormCorrectIconIsVisible = false;
            IvSecondFormCorrectIconIsVisible = false;
            IThirdFormCorrectIconIsVisible = false;
            IvFirstFormWrongIconIsVisible = false;
            IvSecondFormWrongIconIsVisible = false;
            IvThirdFormWrongIconIsVisible = false;
        }
        #endregion

        #region Private UI methods (promt)
        private void IvShowPromt()
        {
            if (ivDictationManager is null)
                return;
            ExecuteForCurrentIrregularVerbForm(
                () => IvSetMysteriousPromtValue(ivDictationManager.CurrentV1Value),
                () => IvSetMysteriousPromtValue(ivDictationManager.CurrentV2Value),
                () => IvSetMysteriousPromtValue(ivDictationManager.CurrentV3Value));
            IvPromtIsVisible = true;
        }
        private void IvHidePromt() => IvPromtIsVisible = false;
        private void IvSetMysteriousPromtValue(string value)
        {
            int symbolsCount = value.Length;
            string mysteriousString = new string('?', symbolsCount);
            IvPromtValue = $"({mysteriousString})";
        }
        private void IvSetPromtValue(string value)
        {
            IvPromtValue = $"({value})";
        }
        #endregion
    }
}
