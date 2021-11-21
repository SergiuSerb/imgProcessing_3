using System.Windows;
using imgProcessing_3.UserInterface.Windows;

namespace imgProcessing_3
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GenerateWindow generateWindow;

        public MainWindow()
        {
            InitializeComponent();
            InitializeViewportControl();
        }

        private void InitializeViewportControl()
        {
            viewportControl.SubscribeToEvents(colourParameterControl, viewportParametersControl);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            generateWindow = new GenerateWindow(colourParameterControl.StrengthR,
                colourParameterControl.StrengthG,
                colourParameterControl.StrengthB,
                viewportParametersControl.selectedFaces);

            generateWindow.ShowDialog();
        }
    }
}