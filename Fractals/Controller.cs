using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fractals.Model;

namespace Fractals
{
    class Controller
    {
        public FractalModel FractalModel;

        public Controller(MainWindow mainWindow)
        {
            //FractalModel = new FractalModel(mainWindow);
        }

        public void Start()
        {
            FractalModel.Start();
        }

        public void Stop()
        {
            FractalModel.Stop();
        }

    }
}
