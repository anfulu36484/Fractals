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
        private PainterOfPoints _painterOfPoints;
        private DeterminantOfGrowthPoints _determinantOfGrowthPoints;

        public StateOfFractal StateOfFractal
        {
            get { return _stateOfFractal; }
            set { _stateOfFractal = value; }
        }
        public Color ColorOfFractal { get { return _colorOfFractal; } }

        public Fractal(FieldGenerator fieldGenerator, Vector lastPosition, Color colorOfFractal)
        {
            _fieldGenerator = fieldGenerator;
            _lastPosition = lastPosition;
            _colorOfFractal = colorOfFractal;
            _stateOfFractal = StateOfFractal.Live;
            _painterOfPoints = new PainterOfPoints(this, fieldGenerator);
            _determinantOfGrowthPoints = new DeterminantOfGrowthPoints(this, fieldGenerator);
        }

        public void GenerateInitialPoint()
        {
            _fieldGenerator.Field[_lastPosition.y, _lastPosition.x] = _colorOfFractal;
        }


        public void GenerateNextPoint()
        {
            Vector nextPoint = _determinantOfGrowthPoints.DetermineGrowthPoint(_lastPosition);
            if (nextPoint == null)
                StateOfFractal = StateOfFractal.Dead;
            else
            {
                _painterOfPoints.Draw(nextPoint,);
                _lastPosition = nextPoint;
            }

        }

    }

}
