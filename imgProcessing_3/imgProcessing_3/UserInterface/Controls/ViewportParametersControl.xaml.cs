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
        public delegate void ViewportParametersChangedHandler(object sender, double rotation, string renderMode, List<bool> selectedFaces);

        private static readonly IList<string> RenderPresets = new List<string> { "3D", "2D" };

        private string currentlySelectedRenderMode;
        private double currentRotation;
        private List<bool> selectedFaces;

        public ViewportParametersControl()
        {
            InitializeComponent();
            InitializePresetDropdown();
            InitializeFacesCheckboxes();
            selectedFaces = new List<bool>
            {
                true, true, true, true, true, true
            };
        }

        private void InitializeFacesCheckboxes()
        {
            checkbox1.IsChecked = true;
            checkbox2.IsChecked = true;
            checkbox3.IsChecked = true;
            checkbox4.IsChecked = true;
            checkbox5.IsChecked = true;
            checkbox6.IsChecked = true;
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
            ViewportParametersChanged?.Invoke(this, currentRotation, currentlySelectedRenderMode, selectedFaces);
        }

        private void rotationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            currentRotation = rotationSlider.Value;
            ViewportParametersChanged?.Invoke(this, currentRotation, currentlySelectedRenderMode, selectedFaces);
        }

        private void faceCheckbox_Click(object sender, RoutedEventArgs e)
        {
            selectedFaces = new List<bool>
            {
                checkbox1.IsChecked.GetValueOrDefault(),
                checkbox2.IsChecked.GetValueOrDefault(),
                checkbox3.IsChecked.GetValueOrDefault(),
                checkbox4.IsChecked.GetValueOrDefault(),
                checkbox5.IsChecked.GetValueOrDefault(),
                checkbox6.IsChecked.GetValueOrDefault()
            };
            ViewportParametersChanged?.Invoke(this, currentRotation, currentlySelectedRenderMode, selectedFaces);
        }
    }
}