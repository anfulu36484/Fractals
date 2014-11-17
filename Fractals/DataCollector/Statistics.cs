using System.Drawing;
using System.Linq;
using System.Windows.Threading;
using Fractals.Model;
using Fractals.Model.DrawFractal;

namespace Fractals.DataCollector
{
    class Statistics : DataDistributor
    {
        private FractalPopulation _fractalsPopulation;
        private MainWindow _mainWindow;

        public Statistics(FractalPopulation fractalsPopulation, MainWindow mainWindow)
        {
            _fractalsPopulation = fractalsPopulation;
            _mainWindow = mainWindow;
        }

        void GetCountLiveAndDeadFractals(ref int liveFractals, ref int deadFractals)
        {
            deadFractals = _fractalsPopulation.Fractals.Count(n => n.StateOfFractal == StateOfFractal.Dead);
            liveFractals = _fractalsPopulation.Fractals.Count - deadFractals;
        }

        public void ShowStatistics()
        {
            _mainWindow.Dispatcher.Invoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(delegate
                {
                    int liveFractals=0, deadFractals=0;
                    GetCountLiveAndDeadFractals(ref liveFractals, ref deadFractals);
                    _mainWindow.LiveFractalLabel.Content = string.Format("Число живых цепей : {0}", liveFractals);
                    _mainWindow.DeadFractalLabel.Content = string.Format("Число мертвых цепей : {0}", deadFractals);
                    return null;
                }), null);

        }

        public override void GetData(Color[,] data)
        {
            ShowStatistics();
        }
    }
}
