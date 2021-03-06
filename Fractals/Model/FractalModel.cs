﻿using System;
using System.Drawing;
using System.Threading;
using Fractals.DataCollector;

namespace Fractals.Model
{
    public delegate void GetResult(Color[,] field);

    class FractalModel
    {
        
        private FieldGenerator _fieldGenerator;
        private FractalPopulation _fractalPopulation;
        private Thread _thread;
        private DataDistributor _dataVisualizer;
        private DataDistributor _statistics;
        private DataDistributor _dataBDSaver;
        private DataOpenTKVisualizer _dataOpenTkVisualizer;

        private Random _random;

        public Random Rand { get { return _random; } }

        public FractalPopulation FractalPopulation
        {
            get { return _fractalPopulation; }
        }

        public FieldGenerator FieldGenerator
        {
            get { return _fieldGenerator; }
        }


        public FractalModel(MainWindow mainWindow, DataOpenTKVisualizer dataOpenTkVisualizer)
        {
            _random = new Random();
            _fractalPopulation = new FractalPopulation(this);
            _fieldGenerator = new FieldGenerator(_fractalPopulation, Settings.SizeOfField);
            _statistics = new Statistics(_fractalPopulation, mainWindow);
            _dataVisualizer = new DataVisualizer(mainWindow);
            _dataBDSaver = new DataBDSaver();



            _dataOpenTkVisualizer = dataOpenTkVisualizer;
            
        }

    


        /// <summary>
        /// Выводить на экран картинку при кождой итерации алгоритма
        /// </summary>
        void Run()
        {
            /*GetResult getResult = _dataVisualizer.GetData;
            getResult += _statistics.GetData;
            getResult += _dataBDSaver.GetData;
            FieldGenerator.Generate(getResult);*/

            GetResult getResult = _statistics.GetData;
            getResult += _dataOpenTkVisualizer.GetData;
            //getResult += _dataBDSaver.GetData;
            FieldGenerator.Generate(getResult);
        }


        public void Start()
        {
            _thread = new Thread(Run);
            _thread.Start();
        }

        public void Stop()
        {
            _thread.Abort();
        }


    }
}
