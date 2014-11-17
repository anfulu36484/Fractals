using System.Drawing;

namespace Fractals.Model.DrawFractal
{
    class Fractal
    {
        private FieldGenerator _fieldGenerator;
        private Color _colorOfFractal;
        private Vector _lastPosition;
        private StateOfFractal _stateOfFractal;
        private FractalModel _fractalModel;

        public StateOfFractal StateOfFractal
        {
            get { return _stateOfFractal; }
            set { _stateOfFractal = value; }
        }
        public Color ColorOfFractal { get { return _colorOfFractal; } }

        public Fractal(FractalModel fractalModel, FieldGenerator fieldGenerator, Vector lastPosition, Color colorOfFractal, FractalPopulation fractalPopulation)
        {
            _fractalModel = fractalModel;
            _fieldGenerator = fieldGenerator;
            _lastPosition = lastPosition;
            _stateOfFractal = StateOfFractal.Live;
            _fractalPopulation = fractalPopulation;
            _countOfMemberShip = 0;
            _colorOfFractal = colorOfFractal;
            _maxCountOfMemberShip = Settings.MaxCountOfMemberShip;
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
            Vector newInitialPoint = DeterminantOfGrowthPoints.DetermineGrowthPoint(_lastPosition, _fractalModel, this);
            Fractal fractal = new Fractal(_fractalModel, _fieldGenerator, newInitialPoint, _colorOfFractal, _fractalPopulation);
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
                Vector nextPoint = DeterminantOfGrowthPoints.DetermineGrowthPoint(_lastPosition, _fractalModel, this);
                if (nextPoint == null)
                    StateOfFractal = StateOfFractal.Dead;
                else
                {
                    Vector[,] neighborhoodOfPoint =
                        DeterminantOfCircularNeighborhoods.DetermineDensityForEachCell(nextPoint, this, Settings.R,
                            _fieldGenerator);
                    PainterOfPoints.Draw(nextPoint, neighborhoodOfPoint, _fieldGenerator, this);

                    _lastPosition = nextPoint;

                    GenNewFractalInTheEventOfConditions();

                }

            }
         }

    }

}
