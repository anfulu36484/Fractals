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
            FractalPopulation fractalPopulation = new FractalPopulation(this);
            fractalPopulation.GenerateInitialFractals();
            GenerateVoidField();
            fractalPopulation.GenerateInitialPoints();

            do
            {
                fractalPopulation.GenerateNextPoints();

            } while (fractalPopulation.WhetherThereAreLivingFractals());

            return _field;

        }

        public void Generate(GetResult getField)
        {
            FractalPopulation fractalPopulation = new FractalPopulation(this);
            fractalPopulation.GenerateInitialFractals();
            GenerateVoidField();
            fractalPopulation.GenerateInitialPoints();

            do
            {
                fractalPopulation.GenerateNextPoints();
                getField(_field);

            } while (fractalPopulation.WhetherThereAreLivingFractals());
        }

    }


}
