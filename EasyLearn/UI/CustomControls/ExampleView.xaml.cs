﻿using EasyLearn.VM.ViewModels.CustomControls;
using System.Windows.Controls;

namespace EasyLearn.UI.CustomControls
{
    public partial class ExampleView : UserControl
    {
        #region Private fields
        private readonly ExampleVM viewModel;
        #endregion

        #region Public props
        public int Id => viewModel.Id;
        public string RussianValue => viewModel.RussianValue;
        public string EnglishValue => viewModel.EnglishValue;
        #endregion
        public ExampleView(ExampleVM viewModel)
        {
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }

        #region Static methods
        public static ExampleView Create(
            string russianTranslation,
            string englishTranslation,
            int exampleId) => new ExampleView(new ExampleVM(russianTranslation, englishTranslation, exampleId));
        #endregion
    }
}
