using System.Collections.Generic;
using System.Drawing;

namespace Fractals.DrawFractal
{
    class PainterOfPoints
    {
        private FieldGenerator _fieldGenerator;
        private Fractal _fractal;
        
        public PainterOfPoints(Fractal fractal, FieldGenerator fieldGenerator)
        {
            _fieldGenerator = fieldGenerator;
            _fractal = fractal;
        }

        /// <summary>
        /// Нарисовать точку
        /// </summary>
        /// <param name="nextPoint"></param>
        void DrawPoint(Vector nextPoint)
        {
            _fieldGenerator.Field[nextPoint.y, nextPoint.x] = _fractal.ColorOfFractal;
        }

        #region Отрисовка окрестностей точки

        private readonly int _clarificationConst = 100;//Константа, количественно определяющая на сколько окрестности точки 
        //будут светлее точки


        /// <summary>
        /// Определение цвета окрестности точки
        /// </summary>
        /// <returns>Цвет окрестности точки</returns>
        Color DefinitionColorOfNeighborhoodPoint()
        {
            int R = _fractal.ColorOfFractal.R + _clarificationConst > 255
                ? 255
                : _fractal.ColorOfFractal.R + _clarificationConst;
            int G = _fractal.ColorOfFractal.G + _clarificationConst > 255
                ? 255
                : _fractal.ColorOfFractal.G + _clarificationConst;
            int B = _fractal.ColorOfFractal.B + _clarificationConst > 255
                ? 255
                : _fractal.ColorOfFractal.B + _clarificationConst;
            return Color.FromArgb(R, G, B);
        }

        Color MixingColor(Color color1, Color color2)
        {
            int R = (color1.R + color2.R)/2;
            int G = (color1.G + color2.G)/2;
            int B = (color1.B + color2.B)/2;
            return Color.FromArgb(R, G, B);
        }


        /// <summary>
        /// Нарисовать окрестности точки, т.е. заполнить прилегающие клетки цветом более светлым, чем цвет точки.
        /// </summary>
        void DrawNeighborhoodOfPoint(List<Vector> neighborhoodOfPoint)
        {
            Color ColorOfNeighborhood = DefinitionColorOfNeighborhoodPoint();

            foreach (var v in neighborhoodOfPoint)
            {
                _fieldGenerator.Field[v.y, v.x] = MixingColor(_fieldGenerator.Field[v.y, v.x], ColorOfNeighborhood);
            }
        }

        #endregion


        public void Draw(Vector point, List<Vector> neighborhoodOfPoint)
        {
            DrawPoint(point);
            DrawNeighborhoodOfPoint(neighborhoodOfPoint);
        }
    }
}
