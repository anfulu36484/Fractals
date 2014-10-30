using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Fractals
{
    class Statistics
    {
        private FieldGenerator _fieldGenerator;
        private MainWindow _mainWindow;

        public Statistics(FieldGenerator fieldGenerator, MainWindow mainWindow)
        {
            _fieldGenerator = fieldGenerator;
            _mainWindow = mainWindow;
        }

        void GetCountLiveAndDeadFractals(ref int liveFractals, ref int deadFractals)
        {
            deadFractals = _fieldGenerator.FractalPopulation.Fractals.Count(n => n.Dead);
            liveFractals = _fieldGenerator.FractalPopulation.Fractals.Count - deadFractals;
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
    }
}
