// Released to the public domain. Use, modify and relicense at will.

using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace Fractals.DataCollector
{
    class DataOpenTKVisualizer : GameWindow
    {

        private Model.FractalModel _fractalModel;

        /// <summary>Creates a 800x600 window with the specified title.</summary>
        public DataOpenTKVisualizer(MainWindow mainWindow, Model.FractalModel fractalModel)
            : base(Settings.SizeOfField, Settings.SizeOfField, GraphicsMode.Default, "OpenTK Quick Start Sample")
        {
            VSync = VSyncMode.On;

            _fractalModel = fractalModel;
            _fractalModel = new Model.FractalModel(mainWindow,this);
        }

        /// <summary>Load resources here.</summary>
        /// <param name="e">Not used.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.White);
            //GL.Enable(EnableCap.DepthTest);

            int w = Settings.SizeOfField;
            int h = Settings.SizeOfField;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, w, 0, h, -1, 1); // Верхний левый угол имеет кооординаты(0, 0)
            GL.Viewport(0, 0, w, h); // Использовать всю поверхность GLControl под рисование
            _fractalModel.Start();
        }

 
        /// <summary>
        /// Called when it is time to setup the next frame. Add you game logic here.
        /// </summary>
        /// <param name="e">Contains timing information for framerate independent logic.</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[Key.Escape])
                Exit();
        }

        private Color[,] _field;

        public void GetData(Color[,] field)
        {
            _field = field;
        }


        /// <summary>
        /// Called when it is time to render the next frame. Add your rendering code here.
        /// </summary>
        /// <param name="e">Contains timing information.</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();


            GL.PointSize(1);
            


            GL.Begin(BeginMode.Points);

            if (_field != null)
            {
                for (int i = 0; i < Settings.SizeOfField; i += 1)
                {
                    for (int j = 0; j < Settings.SizeOfField; j += 1)
                    {
                        GL.Color3(_field[i, j]);
                        GL.Vertex2(i, j);
                    }
                }
            }

            GL.End();
            


            SwapBuffers();
        }
    }
}