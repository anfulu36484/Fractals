using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Fractals
{
    class FieldGenerator
    {
        private Color[,] _field;
        private int _dimensionField;
        private Random _random;
        private FractalPopulation _fractalPopulation;

        public FractalPopulation FractalPopulation { get { return _fractalPopulation; } }

        public Color[,] Field { get { return _field; } }
        public int DimensionField { get { return _dimensionField; } }
        public Random Rand { get { return _random; } }

        public FieldGenerator(int dimensionField)
        {
            _dimensionField = dimensionField; 
            _random = new Random();
        }

        void GenerateVoidField()
        {
            _field = new Color[_dimensionField,_dimensionField];
            for (int i = 0; i < _dimensionField; i++)
            {
                for (int j = 0; j < _dimensionField; j++)
                {
                    _field[i, j] = Color.White;
                }
            }
        }

        

        public Color[,] Generate()
        {
            _fractalPopulation = new FractalPopulation(this);
            _fractalPopulation.GenerateInitialFractals();
            GenerateVoidField();
            _fractalPopulation.GenerateInitialPoints();

            do
            {
                _fractalPopulation.GenerateNextPoints();

            } while (_fractalPopulation.WhetherThereAreLivingFractals());

            return _field;

        }

        public void Generate(GetResult getField)
        {
            _fractalPopulation = new FractalPopulation(this);
            _fractalPopulation.GenerateInitialFractals();
            GenerateVoidField();
            _fractalPopulation.GenerateInitialPoints();

            do
            {
                _fractalPopulation.GenerateNextPoints();
                getField(_field);

            } while (_fractalPopulation.WhetherThereAreLivingFractals());
        }

    }


}
