using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

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
            Parallel.Invoke(() =>
                {
                    if (generateIndividualFaces)
                    {
                        GenerateIndividualFaces();
                    }
                },
                () =>
                {
                    if (generateCompositeImage)
                    {
                        GenerateCompositeImage();
                    }
                }
            );
        }

        private void GenerateCompositeImage()
        {
            int scalingFactor = 1;
            if (size > 1024)
            {
                scalingFactor = size / 1024;
            }

            Bitmap face0 = null, face1 = null, face2 = null, face3 = null, face4 = null, face5 = null;

            Parallel.Invoke(
                () =>
                {
                    face0 = faces[0]
                        ? GenerateFace0()
                        : new Bitmap(size / scalingFactor, size / scalingFactor);
                },
                () =>
                {
                    face1 = faces[1]
                        ? GenerateFace1()
                        : new Bitmap(size / scalingFactor, size / scalingFactor);
                },
                () =>
                {
                    face2 = faces[2]
                        ? GenerateFace2()
                        : new Bitmap(size / scalingFactor, size / scalingFactor);
                },
                () =>
                {
                    face3 = faces[3]
                        ? GenerateFace3()
                        : new Bitmap(size / scalingFactor, size / scalingFactor);
                },
                () =>
                {
                    face4 = faces[4]
                        ? GenerateFace4()
                        : new Bitmap(size / scalingFactor, size / scalingFactor);
                },
                () =>
                {
                    face5 = faces[5]
                        ? GenerateFace5()
                        : new Bitmap(size / scalingFactor, size / scalingFactor);
                }
            );

            Bitmap compositeImage = new Bitmap((size / scalingFactor) * 4, (size / scalingFactor) * 3);

            PaintFace(ref compositeImage, 1, 1, scalingFactor, face0);
            PaintFace(ref compositeImage, 3, 1, scalingFactor, face1);
            PaintFace(ref compositeImage, 1, 0, scalingFactor, face2);
            PaintFace(ref compositeImage, 1, 2, scalingFactor, face3);
            PaintFace(ref compositeImage, 0, 1, scalingFactor, face4);
            PaintFace(ref compositeImage, 2, 1, scalingFactor, face5);

            compositeImage.Save(path + "compositeImage.jpeg", ImageFormat.Jpeg);
        }

        private void PaintFace(ref Bitmap compositeImage, int offsetFaceX, int offsetFaceY, int scalingFactor, Bitmap faceBitmap)
        {
            int offsetX = offsetFaceX * size / scalingFactor;
            int offsetY = offsetFaceY * size / scalingFactor;

            for (int i = 0; i < size; i += scalingFactor)
            {
                for (int j = 0; j < size; j += scalingFactor)
                {
                    compositeImage.SetPixel(i / scalingFactor + offsetX, j / scalingFactor + offsetY, faceBitmap.GetPixel(i, j));
                }
            }
        }

        private void GenerateIndividualFaces()
        {
            Parallel.Invoke(
                () =>
                {
                    if (faces[1])
                    {
                        GenerateFace0().Save(path + "face0.jpeg", ImageFormat.Jpeg);
                    }
                },
                () =>
                {
                    if (faces[1])
                    {
                        GenerateFace1().Save(path + "face1.jpeg", ImageFormat.Jpeg);
                    }
                },
                () =>
                {
                    if (faces[2])
                    {
                        GenerateFace2().Save(path + "face2.jpeg", ImageFormat.Jpeg);
                    }
                },
                () =>
                {
                    if (faces[3])
                    {
                        GenerateFace3().Save(path + "face3.jpeg", ImageFormat.Jpeg);
                    }
                },
                () =>
                {
                    if (faces[4])
                    {
                        GenerateFace4().Save(path + "face4.jpeg", ImageFormat.Jpeg);
                    }
                },
                () =>
                {
                    if (faces[5])
                    {
                        GenerateFace5().Save(path + "face5.jpeg", ImageFormat.Jpeg);
                    }
                });
        }

        private Bitmap GenerateFace0()
        {
            Bitmap bitmap = new Bitmap(size, size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    bitmap.SetPixel(i, j, GetColourForCoord(i, size, j));
                }
            }

            return bitmap;
        }

        private Bitmap GenerateFace1()
        {
            Bitmap bitmap = new Bitmap(size, size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    bitmap.SetPixel(i, j, GetColourForCoord(size - i, 0, j));
                }
            }

            return bitmap;
        }

        private Bitmap GenerateFace2()
        {
            Bitmap bitmap = new Bitmap(size, size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    bitmap.SetPixel(i, j, GetColourForCoord(i, j, 0));
                }
            }

            return bitmap;
        }

        private Bitmap GenerateFace3()
        {
            Bitmap bitmap = new Bitmap(size, size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    bitmap.SetPixel(i, j, GetColourForCoord(i, size - j, size));
                }
            }

            return bitmap;
        }

        private Bitmap GenerateFace4()
        {
            Bitmap bitmap = new Bitmap(size, size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    bitmap.SetPixel(i, j, GetColourForCoord(0, i, j));
                }
            }

            return bitmap;
        }

        private Bitmap GenerateFace5()
        {
            Bitmap bitmap = new Bitmap(size, size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    bitmap.SetPixel(i, j, GetColourForCoord(size, size - i, j));
                }
            }

            return bitmap;
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