﻿using System;
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
        private float StrengthB { get; set; }

        private float StrengthG { get; set; }

        private float StrengthR { get; set; }

        public ViewportControl()
        {
            InitializeComponent();
            StrengthR = 1;
            StrengthG = 1;
            StrengthB = 1;
        }

        private void OpenGLControl_OpenGLDraw(object sender, OpenGLRoutedEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

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

            List<Tuple<byte, byte, byte, byte>> faces2 = new List<Tuple<byte, byte, byte, byte>>
            {
                new Tuple<byte, byte, byte, byte>(4, 3, 2, 5),
                new Tuple<byte, byte, byte, byte>(7, 1, 0, 6),
                new Tuple<byte, byte, byte, byte>(6, 0, 3, 4),
                new Tuple<byte, byte, byte, byte>(5, 2, 1, 7),
                new Tuple<byte, byte, byte, byte>(2, 3, 0, 1),
                new Tuple<byte, byte, byte, byte>(4, 5, 7, 6)
            };

            gl.LoadIdentity();
            gl.Translate(0, 0, -4);
            gl.Rotate(25, -45, 0);

            gl.Begin(OpenGL.GL_QUADS);
            foreach (Tuple<byte, byte, byte, byte> face in faces2)
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

            gl.Flush();
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

        public void SubscribeToEvents(ColourParametersControl colourParameterControl)
        {
            colourParameterControl.ColourParametersChanged += ColourParameterControlOnColourParametersChanged;
        }

        private void ColourParameterControlOnColourParametersChanged(object sender, double r, double g, double b)
        {
            StrengthR = (float)r;
            StrengthG = (float)g;
            StrengthB = (float)b;
        }
    }
}