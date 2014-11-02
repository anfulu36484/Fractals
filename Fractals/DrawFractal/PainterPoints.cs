using System.Collections.Generic;
using System.Drawing;

namespace Fractals.DrawFractal
{
    class PainterOfPoints
    {

        /// <summary>
        /// Нарисовать точку
        /// </summary>
        /// <param name="nextPoint"></param>
        static void DrawPoint(Vector nextPoint, FieldGenerator fieldGenerator, Fractal fractal)
        {
            fieldGenerator.Field[nextPoint.y, nextPoint.x] = fractal.ColorOfFractal;
        }

        #region Отрисовка окрестностей точки

        private static readonly int _clarificationConst = 100;//Константа, количественно определяющая на сколько окрестности точки 
        //будут светлее точки


        /// <summary>
        /// Определение цвета окрестности точки
        /// </summary>
        /// <returns>Цвет окрестности точки</returns>
        static Color DefinitionColorOfNeighborhoodPoint(Fractal fractal)
        {
            int R = fractal.ColorOfFractal.R + _clarificationConst > 255
                ? 255
                : fractal.ColorOfFractal.R + _clarificationConst;
            int G = fractal.ColorOfFractal.G + _clarificationConst > 255
                ? 255
                : fractal.ColorOfFractal.G + _clarificationConst;
            int B = fractal.ColorOfFractal.B + _clarificationConst > 255
                ? 255
                : fractal.ColorOfFractal.B + _clarificationConst;
            return Color.FromArgb(R, G, B);
        }

        static Color MixingColor(Color color1, Color color2)
        {
            int R = (color1.R + color2.R)/2;
            int G = (color1.G + color2.G)/2;
            int B = (color1.B + color2.B)/2;
            return Color.FromArgb(R, G, B);
        }


        /// <summary>
        /// Нарисовать окрестности точки, т.е. заполнить прилегающие клетки цветом более светлым, чем цвет точки.
        /// </summary>
        static void DrawNeighborhoodOfPoint(List<Vector> neighborhoodOfPoint, FieldGenerator fieldGenerator, Fractal fractal)
        {
            Color ColorOfNeighborhood = DefinitionColorOfNeighborhoodPoint(fractal);

            foreach (var v in neighborhoodOfPoint)
            {
                fieldGenerator.Field[v.y, v.x] = MixingColor(fieldGenerator.Field[v.y, v.x], ColorOfNeighborhood);
            }
        }

        #endregion


        public static void Draw(Vector point, List<Vector> neighborhoodOfPoint, 
            FieldGenerator fieldGenerator, Fractal fractal)
        {
            DrawPoint(point,fieldGenerator,fractal);
            DrawNeighborhoodOfPoint(neighborhoodOfPoint, fieldGenerator, fractal);
        }
    }
}
