using System.Collections.Generic;
using System.Linq;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.DictationManagers;
using EasyLearn.Infrastructure.Enums;


namespace EasyLearn.VM.ViewModels.Pages
{
    /// <summary>
    /// Irregular verb dictation part
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
        #endregion

        private void IvShowSection()
        {
            this.CdSectionIsVisible = false;
            this.VpSectionIsVisible = false;
            this.IvSectionIsVisible = true;
        }
        private void IvSetDefaultFixedAnswerValues()
        {
            this.IvFirstFormFixedAnswerValue = "????";
            this.IvSecondFormFixedAnswerValue = "????";
            this.IvThirdFormFixedAnswerValue = "????";
        }
        private void IvSetDictationManager()
        {
            int countOfIrregularVerbs = this.DictationLengthSliderValue;
            List<IrregularVerb> irregularVerbs = DictationManagerHelper.Shuffle(this.irregularVerbRepository.GetAllIrregularVerbs()).Take(countOfIrregularVerbs).ToList();
            this.ivDictationManager = new IrregularVerbDictationManager(irregularVerbs);
        }
        private void IvSetIrregularVerb(IrregularVerb irregularVerb)
        {
            this.IvMainDisplayValue = irregularVerb.RussianUnit.Value.NormalizeRegister();
            this.IvCommentValue = irregularVerb.Comment.TryNormalizeRegister();
        }

#pragma warning disable CS8602
        private void IvStart()
        {
            SetDefaultPageState();
            this.dictationIsStarted = true;
            IvSetDictationManager();
            IrregularVerb firstIrregularVerb = ivDictationManager.Start();
            IvSetIrregularVerb(firstIrregularVerb);
            SwitchStartAndStopButtons();
            FocusAnswerTextBox();
            SetDictationProgressBar();
        }
#pragma warning restore CS8602

        private void IvCheck()
        {
            if (!dictationIsStarted || ivDictationManager is null)
                return;

            switch (currentIrregularVerbForm)
            {
                case IrregularVerbForm.FirstForm:
                    bool firstFormIsCorrect = this.ivDictationManager.IsFirstFormAnswerCorrect(this.AnswerValue);
                    if (firstFormIsCorrect)
                    {
                        IvShowFirstFormCorrectIcon();
                        IncreaseDictationProgressBarValue();
                        this.IvFirstFormFixedAnswerValue = StringHelper.NormalizeRegister(this.AnswerValue);
                        this.currentIrregularVerbForm = IrregularVerbForm.SecondForm;
                        SetDefaultAnswerValue();
                    }
                    else
                    {
                        IvShowFirstFormWrongIcon();
                    }
                    break;
                case IrregularVerbForm.SecondForm:
                    bool secondFormIsCorrect = ivDictationManager.IsSecondFormAnswerCorrect(this.AnswerValue);
                    if (secondFormIsCorrect)
                    {
                        IvShowSecondFormCorrectIcon();
                        IncreaseDictationProgressBarValue();
                        this.IvSecondFormFixedAnswerValue = StringHelper.NormalizeRegister(this.AnswerValue);
                        this.currentIrregularVerbForm = IrregularVerbForm.ThirdForm;
                        SetDefaultAnswerValue();
                    }
                    else
                    {
                        IvShowSecondFormWrongIcon();
                    }
                    break;
                case IrregularVerbForm.ThirdForm:
                    bool thirdFormIsCorrect = ivDictationManager.IsThirdFormAnswerCorrect(this.AnswerValue);
                    if (thirdFormIsCorrect)
                    {
                        IvShowThirdFormCorrectIcon();
                        IncreaseDictationProgressBarValue();
                        SwitchCheckAndNextButtons();
                        this.IvThirdFormFixedAnswerValue = StringHelper.NormalizeRegister(this.AnswerValue);
                        SetDefaultAnswerValue();
                        this.currentIrregularVerbForm = IrregularVerbForm.FirstForm;
                    }
                    else
                    {
                        IvShowThirdFormWrongIcon();
                    }
                    break;
            }
        }
        private void IvTryGoNext()
        {
            if (!this.dictationIsStarted || ivDictationManager is null)
                return;
            if (ivDictationManager.GoNext())
            {
                IvSetIrregularVerb(ivDictationManager.CurrentIrregularVerb);
                SetDefaultAnswerValue();
                IvShowGrayIcons();
                SwitchCheckAndNextButtons();
                IvSetDefaultFixedAnswerValues();
            }
            else
            {
                StopDictation();
            }
        }

        #region Icons
        private void IvShowFirstFormCorrectIcon()
        {
            this.IvFirstFormGrayIconIsVisible = false;
            this.IvFirstFormCorrectIconIsVisible = true;
            this.IvFirstFormWrongIconIsVisible = false;
        }
        private void IvShowFirstFormWrongIcon()
        {
            this.IvFirstFormGrayIconIsVisible = false;
            this.IvFirstFormCorrectIconIsVisible = false;
            this.IvFirstFormWrongIconIsVisible = true;
        }
        private void IvShowSecondFormCorrectIcon()
        {
            this.IvSecondFormGrayIconIsVisible = false;
            this.IvSecondFormCorrectIconIsVisible = true;
            this.IvSecondFormWrongIconIsVisible = false;
        }
        private void IvShowSecondFormWrongIcon()
        {
            this.IvSecondFormGrayIconIsVisible = false;
            this.IvSecondFormCorrectIconIsVisible = false;
            this.IvSecondFormWrongIconIsVisible = true;
        }
        private void IvShowThirdFormCorrectIcon()
        {
            this.IvThirdFormGrayIconIsVisible = false;
            this.IThirdFormCorrectIconIsVisible = true;
            this.IvThirdFormWrongIconIsVisible = false;
        }
        private void IvShowThirdFormWrongIcon()
        {
            this.IvThirdFormGrayIconIsVisible = false;
            this.IThirdFormCorrectIconIsVisible = false;
            this.IvThirdFormWrongIconIsVisible = true;
        }
        private void IvShowGrayIcons()
        {
            IvHideAllIcons();
            this.IvFirstFormGrayIconIsVisible = true;
            this.IvSecondFormGrayIconIsVisible = true;
            this.IvThirdFormGrayIconIsVisible = true;
        }
        private void IvHideAllIcons()
        {
            this.IvFirstFormGrayIconIsVisible = false;
            this.IvSecondFormGrayIconIsVisible = false;
            this.IvThirdFormGrayIconIsVisible = false;
            this.IvFirstFormCorrectIconIsVisible = false;
            this.IvSecondFormCorrectIconIsVisible = false;
            this.IThirdFormCorrectIconIsVisible = false;
            this.IvFirstFormWrongIconIsVisible = false;
            this.IvSecondFormWrongIconIsVisible = false;
            this.IvThirdFormWrongIconIsVisible = false;
        }
        #endregion
    }
}
