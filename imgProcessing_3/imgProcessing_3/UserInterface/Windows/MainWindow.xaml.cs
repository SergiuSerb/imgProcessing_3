using System.Windows;

namespace imgProcessing_3
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeViewportControl();
        }

        private void InitializeViewportControl()
        {
            viewportControl.SubscribeToEvents(colourParameterControl, viewportParametersControl);
        }
    }
}