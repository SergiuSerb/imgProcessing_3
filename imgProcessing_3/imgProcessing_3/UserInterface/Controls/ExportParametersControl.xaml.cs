using System;
using System.Windows;
using System.Windows.Controls;

namespace imgProcessing_3.UserInterface.Controls
{
    /// <summary>
    ///     Interaction logic for ExportParametersControl.xaml
    /// </summary>
    public partial class ExportParametersControl : UserControl
    {
        public int CurrentSize { get; set; }

        public ExportParametersControl()
        {
            CurrentSize = 2;
            InitializeComponent();
        }


        private void sizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CurrentSize = (int)Math.Pow(2, sizeSlider.Value);
            UpdateSizeTextBox();
        }

        private void UpdateSizeTextBox()
        {
            if (sizeValue != null)
            {
                sizeValue.Text = $"{CurrentSize} x {CurrentSize}";
            }
        }

        private void pathTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}