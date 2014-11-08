using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace Fractals.DrawFractal
{
    class Fractal
    {
        private FieldGenerator _fieldGenerator;
        private Color _colorOfFractal;
        private Vector _lastPosition;
        private StateOfFractal _stateOfFractal;

        public StateOfFractal StateOfFractal
        {
            get { return _stateOfFractal; }
            set { _stateOfFractal = value; }
        }
        public Color ColorOfFractal { get { return _colorOfFractal; } }

        public Fractal(FieldGenerator fieldGenerator, Vector lastPosition, Color colorOfFractal, FractalPopulation fractalPopulation)
        {
            _fieldGenerator = fieldGenerator;
            _lastPosition = lastPosition;
            _colorOfFractal = colorOfFractal;
            _stateOfFractal = StateOfFractal.Live;
            _fractalPopulation = fractalPopulation;
            _countOfMemberShip = 0;
            _maxCountOfMemberShip = 10;
        }

        public void GenerateInitialPoint()
        {
            _fieldGenerator.Field[_lastPosition.x, _lastPosition.y] = _colorOfFractal;
        }

        #region Создание нового фрактала как ответвления от существующего

        private int _countOfMemberShip;

        /// <summary>
        /// Максимальное количество членов в цепочке после которого зарождается еще одна цепочка
        /// </summary>
        private int _maxCountOfMemberShip;

        private FractalPopulation _fractalPopulation;

        void GenerateNewFractal()
        {
            Vector newInitialPoint = DeterminantOfGrowthPoints.DetermineGrowthPoint(_lastPosition, _fieldGenerator, this);
            Fractal fractal = new Fractal(_fieldGenerator,newInitialPoint,_colorOfFractal,_fractalPopulation);
            _fractalPopulation.AddFractal(fractal);
        }

        void GenNewFractalInTheEventOfConditions()
        {
            if (_countOfMemberShip > _maxCountOfMemberShip)
            {
                GenerateNewFractal();
                _countOfMemberShip = 0;
            }
            else
            {
                _countOfMemberShip++;
            }
        }

        #endregion

        public void GenerateNextPoint()
        {
            if(_lastPosition == null)
                StateOfFractal = StateOfFractal.Dead;
            else
            {
                Vector nextPoint = DeterminantOfGrowthPoints.DetermineGrowthPoint(_lastPosition, _fieldGenerator, this);
                if (nextPoint == null)
                    StateOfFractal = StateOfFractal.Dead;
                else
                {
                    Vector[,] neighborhoodOfPoint =
                        DeterminantOfCircularNeighborhoods.DetermineDensityForEachCell(nextPoint, this, 3,
                            _fieldGenerator);
                    PainterOfPoints.Draw(nextPoint, neighborhoodOfPoint, _fieldGenerator, this);

                    _lastPosition = nextPoint;

                    GenNewFractalInTheEventOfConditions();

                }

            }
         }

    }

}
