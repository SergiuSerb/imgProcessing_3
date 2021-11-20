using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace imgProcessing_3.UserInterface.Controls
{
    /// <summary>
    ///     Interaction logic for ViewportParametersControl.xaml
    /// </summary>
    public partial class ViewportParametersControl : UserControl
    {
        public delegate void ViewportParametersChangedHandler(object sender, double rotation, string renderMode);

        private static readonly IList<string> RenderPresets = new List<string> { "3D", "2D" };

        private string currentlySelectedRenderMode;
        private double currentRotation;

        public ViewportParametersControl()
        {
            InitializeComponent();
            InitializePresetDropdown();
        }

        private void InitializePresetDropdown()
        {
            foreach (string item in RenderPresets)
            {
                presetDropdown.Items.Add(item);
            }
        }

        public event ViewportParametersChangedHandler ViewportParametersChanged;

        private void presetDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentlySelectedRenderMode = presetDropdown.Text;
            ViewportParametersChanged?.Invoke(this, currentRotation, currentlySelectedRenderMode);
        }

        private void rotationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            currentRotation = rotationSlider.Value;
            ViewportParametersChanged?.Invoke(this, currentRotation, currentlySelectedRenderMode);
        }
    }
}