//На основе:
//http://stackoverflow.com/questions/625029/how-do-i-store-and-retrieve-a-blob-from-sqlite
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Fractals
{
    class DataManager
    {
        private BMPVisualizer _bmpVisualizer;
        private Statistics _statistics;
        private FieldGenerator _fieldGenerator;
        private Thread _thread;
        private MainWindow _mainWindow;

        public DataManager(MainWindow mainWindow)
        {
            _bmpVisualizer = new BMPVisualizer(mainWindow);
            _fieldGenerator = new FieldGenerator(Settings.SizeOfField);
            _statistics = new Statistics(_fieldGenerator, mainWindow);
        }

        /// <summary>
        /// Вывести на экран конечный вариант картинки
        /// </summary>
        void Run1()
        {
            Color[,] field = _fieldGenerator.Generate();
            _bmpVisualizer.DisplayImage(field);
        }


        void GetResultHandler(Color[,] field)
        {
            _bmpVisualizer.DisplayImage(field);
            _statistics.ShowStatistics();
        }

        /// <summary>
        /// Выводить на экран картинку при кождой итерации алгоритма
        /// </summary>
        void Run2()
        {
            _fieldGenerator.Generate(GetResultHandler);
        }


        public void Start()
        {
            _thread = new Thread(Run2);
            _thread.Start();
        }

        public void Stop()
        {
            _thread.Abort();
        }


    }
}
