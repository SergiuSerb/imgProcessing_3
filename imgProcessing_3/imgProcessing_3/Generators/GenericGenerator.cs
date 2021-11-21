using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace imgProcessing_3.Generators
{
    public class GenericGenerator
    {
        private readonly List<bool> faces;
        private readonly bool generateCompositeImage;
        private readonly bool generateIndividualFaces;
        private readonly string path;
        private readonly int size;
        private readonly double strengthB;
        private readonly double strengthG;
        private readonly double strengthR;

        public GenericGenerator(bool generateIndividualFaces,
            bool generateCompositeImage,
            double strengthR,
            double strengthG,
            double strengthB,
            List<bool> faces,
            int size,
            string path)
        {
            this.generateIndividualFaces = generateIndividualFaces;
            this.generateCompositeImage = generateCompositeImage;
            this.strengthR = strengthR;
            this.strengthG = strengthG;
            this.strengthB = strengthB;
            this.faces = faces;
            this.size = size;
            this.path = path;
        }

        public void Generate()
        {
            if (generateIndividualFaces)
            {
                GenerateIndividualFaces();
            }
        }

        private void GenerateIndividualFaces()
        {
            if (faces[0])
            {
                GenerateFace0();
            }

            if (faces[1])
            {
                GenerateFace1();
            }
        }

        private void GenerateFace0()
        {
            Bitmap bitmap = new Bitmap(size, size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    bitmap.SetPixel(i, j, GetColourForCoord(i, size, j));
                }
            }

            bitmap.Save(path + "face0.bmp", ImageFormat.Bmp);
        }

        private void GenerateFace1()
        {
            Bitmap bitmap = new Bitmap(size, size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    bitmap.SetPixel(i, j, GetColourForCoord(size - i, 0, j));
                }
            }

            bitmap.Save(path + "face1.bmp", ImageFormat.Bmp);
        }

        private Color GetColourForCoord(int x, int y, int z)
        {
            int red = (int)(x * (255.0 / size));
            int green = (int)(y * (255.0 / size));
            int blue = (int)(z * (255.0 / size));

            return Color.FromArgb(red, green, blue);
        }
    }
}