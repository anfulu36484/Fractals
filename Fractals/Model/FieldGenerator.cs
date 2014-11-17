using System;
using System.Drawing;

namespace Fractals.Model
{
    class FieldGenerator
    {
        private Color[,] _field;
        private int _dimensionField;
        
        public Color[,] Field { get { return _field; } }
        public int DimensionField { get { return _dimensionField; } }

        private FractalPopulation _fractalPopulation;

        public FieldGenerator(FractalPopulation fractalPopulation, int dimensionField)
        {
            _fractalPopulation = fractalPopulation;
            _dimensionField = dimensionField; 
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

      
        public void Generate(GetResult getField)
        {
            _fractalPopulation.GenerateInitialFractals();
            GenerateVoidField();
            _fractalPopulation.GenerateInitialPoints();

            do
            {
                _fractalPopulation.GenerateNextPoints();
                _fractalPopulation.AddAndRemoveFractalsFromCollection();
                getField(_field);

            } while (_fractalPopulation.CheckStopCondition());
        }

    }


}
