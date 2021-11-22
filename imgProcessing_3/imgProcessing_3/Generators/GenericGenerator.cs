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

            if (faces[2])
            {
                GenerateFace2();
            }

            if (faces[3])
            {
                GenerateFace3();
            }

            if (faces[4])
            {
                GenerateFace4();
            }

            if (faces[5])
            {
                GenerateFace5();
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

            bitmap.Save(path + "face0.jpeg", ImageFormat.Jpeg);
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

            bitmap.Save(path + "face1.jpeg", ImageFormat.Jpeg);
        }

        private void GenerateFace2()
        {
            Bitmap bitmap = new Bitmap(size, size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    bitmap.SetPixel(i, j, GetColourForCoord(i, j, 0));
                }
            }

            bitmap.Save(path + "face2.jpeg", ImageFormat.Jpeg);
        }

        private void GenerateFace3()
        {
            Bitmap bitmap = new Bitmap(size, size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    bitmap.SetPixel(i, j, GetColourForCoord(i, size - j, size));
                }
            }

            bitmap.Save(path + "face3.jpeg", ImageFormat.Jpeg);
        }

        private void GenerateFace4()
        {
            Bitmap bitmap = new Bitmap(size, size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    bitmap.SetPixel(i, j, GetColourForCoord(0, i, j));
                }
            }

            bitmap.Save(path + "face4.jpeg", ImageFormat.Jpeg);
        }

        private void GenerateFace5()
        {
            Bitmap bitmap = new Bitmap(size, size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    bitmap.SetPixel(i, j, GetColourForCoord(size, size - i, j));
                }
            }

            bitmap.Save(path + "face5.jpeg", ImageFormat.Jpeg);
        }

        private Color GetColourForCoord(int x, int y, int z)
        {
            int red = (int)(x * (255.0 / size) * strengthR);
            int green = (int)(y * (255.0 / size) * strengthG);
            int blue = (int)(z * (255.0 / size) * strengthB);

            return Color.FromArgb(red, green, blue);
        }
    }
}