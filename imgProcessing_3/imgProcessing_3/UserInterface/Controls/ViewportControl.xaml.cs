using System;
using System.Collections.Generic;
using System.Windows.Controls;
using SharpGL;
using SharpGL.WPF;

namespace imgProcessing_3.UserInterface.Controls
{
    /// <summary>
    ///     Interaction logic for ViewportControl.xaml
    /// </summary>
    public partial class ViewportControl : UserControl
    {
        private string renderMode;
        private List<bool> selectedFaces;

        private float StrengthB { get; set; }

        private float StrengthG { get; set; }

        private float StrengthR { get; set; }

        public float CurrentRotation { get; set; }

        public ViewportControl()
        {
            InitializeComponent();
            StrengthR = 1;
            StrengthG = 1;
            StrengthB = 1;
            selectedFaces = new List<bool>
            {
                true, true, true, true, true, true
            };
        }

        private void OpenGLControl_OpenGLDraw(object sender, OpenGLRoutedEventArgs args)
        {
            OpenGL gl = args.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            if (renderMode != "3D")
            {
                gl.MatrixMode(OpenGL.GL_PROJECTION);
                gl.LoadIdentity();

                gl.Ortho(-1.5, 1.5, -1.5, 1.5, 2, 30);
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();

                gl.LookAt(3, 3, 3,
                    0, 0.0, 0.0,
                    0.0, 1, 0.0);

                gl.Rotate(CurrentRotation, 0, 1, 0);

                gl.PushMatrix();
                {
                    gl.Translate(-0.5, -0.5, -0.5);
                    RenderCube(gl);
                }
                gl.PopMatrix();
            }
            else
            {
                gl.MatrixMode(OpenGL.GL_PROJECTION);
                gl.LoadIdentity();

                gl.Ortho(-2.25, 3.75, -3, 3, -5, 30);
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();

                gl.LookAt(0, 3, 0.001,
                    0, 0.0, 0.0,
                    0.0, 1, 0.0);

                if (selectedFaces[0])
                {
                    gl.PushMatrix();
                    {
                        gl.Translate(-0.5, -0.5, -0.5);
                        RenderWithoutFaceCheckCube(gl);
                    }
                    gl.PopMatrix();
                }

                if (selectedFaces[2])
                {
                    gl.PushMatrix();
                    {
                        gl.Rotate(90, 1, 0, 0);
                        gl.Translate(-0.5, -1.5, -0.5);
                        RenderWithoutFaceCheckCube(gl);
                    }
                    gl.PopMatrix();
                }

                if (selectedFaces[5])
                {
                    gl.PushMatrix();
                    {
                        gl.Rotate(90, 0, 0, 1);
                        gl.Translate(1.5, -1.5, -0.5);
                        RenderWithoutFaceCheckCube(gl);
                    }
                    gl.PopMatrix();
                }

                if (selectedFaces[3])
                {
                    gl.PushMatrix();
                    {
                        gl.Rotate(-90, 1, 0, 0);
                        gl.Translate(-0.5, -1.5, -0.5);
                        RenderWithoutFaceCheckCube(gl);
                    }
                    gl.PopMatrix();
                }

                if (selectedFaces[4])
                {
                    gl.PushMatrix();
                    {
                        gl.Rotate(-90, 0, 0, 1);
                        gl.Translate(1.5, -1.5, -0.5);
                        RenderWithoutFaceCheckCube(gl);
                    }
                    gl.PopMatrix();
                }

                if (selectedFaces[1])
                {
                    gl.PushMatrix();
                    {
                        gl.Rotate(-180, 0, 0, 1);
                        gl.Translate(-2.5, -1.5, -0.5);
                        RenderWithoutFaceCheckCube(gl);
                    }
                    gl.PopMatrix();
                }
            }

            gl.Flush();
        }

        private void RenderCube(OpenGL gl)
        {
            List<Tuple<byte, byte, byte>> vertices = new List<Tuple<byte, byte, byte>>
            {
                new Tuple<byte, byte, byte>(0, 0, 0), // 0
                new Tuple<byte, byte, byte>(0, 0, 1), // 1
                new Tuple<byte, byte, byte>(0, 1, 1), // 2
                new Tuple<byte, byte, byte>(0, 1, 0), // 3
                new Tuple<byte, byte, byte>(1, 1, 0), // 4
                new Tuple<byte, byte, byte>(1, 1, 1), // 5
                new Tuple<byte, byte, byte>(1, 0, 0), // 6
                new Tuple<byte, byte, byte>(1, 0, 1)
            };

            List<Tuple<byte, byte, byte, byte>> faces = new List<Tuple<byte, byte, byte, byte>>
            {
                new Tuple<byte, byte, byte, byte>(4, 3, 2, 5),
                new Tuple<byte, byte, byte, byte>(7, 1, 0, 6),
                new Tuple<byte, byte, byte, byte>(6, 0, 3, 4),
                new Tuple<byte, byte, byte, byte>(5, 2, 1, 7),
                new Tuple<byte, byte, byte, byte>(2, 3, 0, 1),
                new Tuple<byte, byte, byte, byte>(4, 5, 7, 6)
            };

            gl.Begin(OpenGL.GL_QUADS);
            int currentFace = 0;
            foreach (Tuple<byte, byte, byte, byte> face in faces)
            {
                if (selectedFaces[currentFace])
                {
                    List<Tuple<byte, byte, byte>> faceVertices = new List<Tuple<byte, byte, byte>>
                    {
                        vertices[face.Item1],
                        vertices[face.Item2],
                        vertices[face.Item3],
                        vertices[face.Item4]
                    };

                    foreach (Tuple<byte, byte, byte> vertex in faceVertices)
                    {
                        (float colourR, float colourG, float colourB) = GetColourForVertex(vertex);
                        gl.Color(colourR, colourG, colourB);

                        gl.Vertex(vertex.Item1, vertex.Item2, vertex.Item3);
                    }
                }

                currentFace++;
            }

            gl.End();
        }

        private void RenderWithoutFaceCheckCube(OpenGL gl)
        {
            List<Tuple<byte, byte, byte>> vertices = new List<Tuple<byte, byte, byte>>
            {
                new Tuple<byte, byte, byte>(0, 0, 0), // 0
                new Tuple<byte, byte, byte>(0, 0, 1), // 1
                new Tuple<byte, byte, byte>(0, 1, 1), // 2
                new Tuple<byte, byte, byte>(0, 1, 0), // 3
                new Tuple<byte, byte, byte>(1, 1, 0), // 4
                new Tuple<byte, byte, byte>(1, 1, 1), // 5
                new Tuple<byte, byte, byte>(1, 0, 0), // 6
                new Tuple<byte, byte, byte>(1, 0, 1)
            };

            List<Tuple<byte, byte, byte, byte>> faces = new List<Tuple<byte, byte, byte, byte>>
            {
                new Tuple<byte, byte, byte, byte>(4, 3, 2, 5),
                new Tuple<byte, byte, byte, byte>(7, 1, 0, 6),
                new Tuple<byte, byte, byte, byte>(6, 0, 3, 4),
                new Tuple<byte, byte, byte, byte>(5, 2, 1, 7),
                new Tuple<byte, byte, byte, byte>(2, 3, 0, 1),
                new Tuple<byte, byte, byte, byte>(4, 5, 7, 6)
            };

            gl.Begin(OpenGL.GL_QUADS);
            foreach (Tuple<byte, byte, byte, byte> face in faces)
            {
                List<Tuple<byte, byte, byte>> faceVertices = new List<Tuple<byte, byte, byte>>
                {
                    vertices[face.Item1],
                    vertices[face.Item2],
                    vertices[face.Item3],
                    vertices[face.Item4]
                };

                foreach (Tuple<byte, byte, byte> vertex in faceVertices)
                {
                    (float colourR, float colourG, float colourB) = GetColourForVertex(vertex);
                    gl.Color(colourR, colourG, colourB);

                    gl.Vertex(vertex.Item1, vertex.Item2, vertex.Item3);
                }
            }

            gl.End();
        }

        private Tuple<float, float, float> GetColourForVertex(Tuple<byte, byte, byte> vertex)
        {
            return new Tuple<float, float, float>(
                vertex.Item1 * StrengthR,
                vertex.Item2 * StrengthG,
                vertex.Item3 * StrengthB);
        }

        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLRoutedEventArgs args)
        {
            args.OpenGL.Enable(OpenGL.GL_DEPTH_TEST);
        }

        public void SubscribeToEvents(ColourParametersControl colourParameterControl, ViewportParametersControl viewportParametersControl)
        {
            colourParameterControl.ColourParametersChanged += ColourParameterControlOnColourParametersChanged;
            viewportParametersControl.ViewportParametersChanged += ViewportParametersControlOnViewportParametersChanged;
        }

        private void ViewportParametersControlOnViewportParametersChanged(object sender, double rotation, string renderMode, List<bool> selectedFaces)
        {
            CurrentRotation = (float)rotation;
            this.selectedFaces = selectedFaces;
            this.renderMode = renderMode;
        }

        private void ColourParameterControlOnColourParametersChanged(object sender, double r, double g, double b)
        {
            StrengthR = (float)r;
            StrengthG = (float)g;
            StrengthB = (float)b;
        }
    }
}