using System.Collections.Generic;
using System.Windows;
using imgProcessing_3.Generators;

namespace imgProcessing_3.UserInterface.Windows
{
    /// <summary>
    ///     Interaction logic for GenerateWindow.xaml
    /// </summary>
    public partial class GenerateWindow : Window
    {
        private readonly List<bool> faces;
        private readonly double strengthB;
        private readonly double strengthG;
        private readonly double strengthR;

        public GenerateWindow(double strengthR,
            double strengthG,
            double strengthB,
            List<bool> faces)
        {
            this.strengthR = strengthR;
            this.strengthG = strengthG;
            this.strengthB = strengthB;
            this.faces = faces;
            InitializeComponent();
        }

        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            string path = exportParametersControl.pathTextbox.Text;
            int size = exportParametersControl.CurrentSize;
            bool generateIndividualFaces = (bool)exportParametersControl.generateIndividualFaces.IsChecked;
            bool generateCompositeImage = (bool)exportParametersControl.generateCompositeImage.IsChecked;

            GenericGenerator generator = new GenericGenerator(generateIndividualFaces,
                generateCompositeImage,
                strengthR,
                strengthG,
                strengthB,
                faces,
                size,
                path);

            generator.Generate();
        }
    }
}