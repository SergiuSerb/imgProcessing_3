using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace imgProcessing_3.UserInterface.Controls
{
    public partial class ColourParametersControl : UserControl
    {
        public delegate void ColourParametersChangedHandler(object sender, double R, double G, double B);

        private static readonly IList<string> ColourPresets = new List<string> { "Red-Green", "Red-Blue", "Green-Blue", "Custom" };

        private bool isPresetSelected;
        private ComboBox presetDropDownControl;
        private IDictionary<string, Slider> strengthSliderControls;

        private IDictionary<string, TextBlock> textBlockControls;

        public double StrengthR { get; set; }

        public double StrengthG { get; set; }

        public double StrengthB { get; set; }

        public string SelectedPreset { get; set; }

        public ColourParametersControl()
        {
            InitializeComponent();

            isPresetSelected = false;

            StrengthR = 1;
            StrengthG = 1;
            StrengthB = 1;

            LoadTextBlocks();
            UpdateTextBlocks();

            LoadSliders();
            UpdateSliders();

            LoadPresetDropDown();
            UpdatePresetDropDown();

            SetDropdownValues();

            SubscribeToEvents();
        }

        private void UpdatePresetDropDown()
        {
            presetDropdown.SelectedIndex = 3;
        }

        private void LoadTextBlocks()
        {
            textBlockControls = new Dictionary<string, TextBlock>
            {
                { "strengthRValue", strengthRValue },
                { "strengthGValue", strengthGValue },
                { "strengthBValue", strengthBValue }
            };
        }

        private void LoadSliders()
        {
            strengthSliderControls = new Dictionary<string, Slider>
            {
                { "strengthRSlider", strengthRSlider },
                { "strengthGSlider", strengthGSlider },
                { "strengthBSlider", strengthBSlider }
            };
        }

        private void LoadPresetDropDown()
        {
            presetDropDownControl = presetDropdown;
        }

        private void SetDropdownValues()
        {
            foreach (string colourPreset in ColourPresets)
            {
                presetDropdown.Items.Add(colourPreset);
            }
        }

        private event SliderValueChangedHandler SliderValueChanged;

        private event PresetValueChangedHandler PresetValueChanged;

        public event ColourParametersChangedHandler ColourParametersChanged;

        private void SubscribeToEvents()
        {
            SliderValueChanged += OnSliderValueChanged;
            PresetValueChanged += OnPresetValueChanged;
        }

        private void OnPresetValueChanged(object sender)
        {
            UpdateTextBlocks();
            UpdateSliders();
            ColourParametersChanged?.Invoke(sender, StrengthR, StrengthG, StrengthB);
        }

        private void OnSliderValueChanged(object sender)
        {
            UpdateTextBlocks();
            ColourParametersChanged?.Invoke(sender, StrengthR, StrengthG, StrengthB);
        }

        private void UpdateSliders()
        {
            strengthSliderControls["strengthRSlider"].Value = StrengthR;
            strengthSliderControls["strengthRSlider"].IsEnabled = !isPresetSelected;

            strengthSliderControls["strengthGSlider"].Value = StrengthG;
            strengthSliderControls["strengthGSlider"].IsEnabled = !isPresetSelected;

            strengthSliderControls["strengthBSlider"].Value = StrengthB;
            strengthSliderControls["strengthBSlider"].IsEnabled = !isPresetSelected;
        }

        private void UpdateTextBlocks()
        {
            textBlockControls["strengthRValue"].Text = GetStringifiedValue(StrengthR);
            textBlockControls["strengthGValue"].Text = GetStringifiedValue(StrengthG);
            textBlockControls["strengthBValue"].Text = GetStringifiedValue(StrengthB);
        }

        private string GetStringifiedValue(double valueToStringify)
        {
            string value = Math.Round(valueToStringify, 2).ToString(CultureInfo.InvariantCulture);

            switch (value)
            {
                case "0": return "0.00";
                case "1": return "1.00";
                default: return value;
            }
        }


        private void strengthRSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            StrengthR = e.NewValue;
            SliderValueChanged?.Invoke(this);
        }

        private void strengthGSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            StrengthG = e.NewValue;
            SliderValueChanged?.Invoke(this);
        }

        private void strengthBSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            StrengthB = e.NewValue;
            SliderValueChanged?.Invoke(this);
        }

        private void presetDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? selection = e.AddedItems[0].ToString();

            switch (selection)
            {
                case "Red-Green":
                    StrengthR = 1;
                    StrengthG = 1;
                    StrengthB = 0;
                    isPresetSelected = true;
                    break;

                case "Red-Blue":
                    StrengthR = 1;
                    StrengthG = 0;
                    StrengthB = 1;
                    isPresetSelected = true;
                    break;

                case "Green-Blue":
                    StrengthR = 0;
                    StrengthG = 1;
                    StrengthB = 1;
                    isPresetSelected = true;
                    break;

                case "Custom":
                    StrengthR = 1;
                    StrengthG = 1;
                    StrengthB = 1;
                    isPresetSelected = false;
                    break;
            }

            PresetValueChanged?.Invoke(this);
        }

        private delegate void SliderValueChangedHandler(object sender);

        private delegate void PresetValueChangedHandler(object sender);
    }
}