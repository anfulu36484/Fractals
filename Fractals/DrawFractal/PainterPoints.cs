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

        /// <summary>
        /// Константа, определяющая на сколько  должна возрасти плотность окрестной точки
        /// </summary>
        private static readonly float k = 1.1f;


        //Расчет плотности точки, лежащей в окрестностях новосозданной точки (px)
        static float CalcOfDensityOfNeighborhoodPoint(Vector neighborhoodOfPoint, FieldGenerator fieldGenerator)
        {
            return DeterminantOfGrowthPoints.DefineOfDensity(fieldGenerator.Field[neighborhoodOfPoint.y, neighborhoodOfPoint.x]);
        }

        /// <summary>
        /// Смешиваем цвета
        /// </summary>
        /// <returns>R[nx], G[nx], B[nx]</returns>
        static Color MixingColor(Color color1, Color color2)
        {
            int R = (color1.R + color2.R) / 2;
            int G = (color1.G + color2.G) / 2;
            int B = (color1.B + color2.B) / 2;
            return Color.FromArgb(R, G, B);
        }

        /// <summary>
        /// alpha := (1/3)*(1-p[x]*k*(R[nx]+G[nx]+B[nx]))/(p[x]*k)
        /// </summary>
        /// <returns></returns>
        static float CalcAlpha(Vector neighborhoodOfPoint, 
                               FieldGenerator fieldGenerator,
                               Color mixingColor)
        {
            float px = CalcOfDensityOfNeighborhoodPoint(neighborhoodOfPoint, fieldGenerator);
            return (1/3f)*(1 - px*k*(mixingColor.R + mixingColor.G + mixingColor.B))/(px*k);
        }


        static int filter(int value)
        {
            if (value > 255)
                return 255;
            if (value < 0)
                return 0;
            return value;
        }

        /// <summary>
        /// Определение цвета окрестности точки
        /// </summary>
        /// <returns>Цвет окрестности точки</returns>
        static Color DefinitionColorOfNeighborhoodPoint(Vector nextPoint, 
                                                        Vector neighborhoodOfPoint, 
                                                        FieldGenerator fieldGenerator)
        {
            Color mixingColor = MixingColor(fieldGenerator.Field[nextPoint.y, nextPoint.x],
                fieldGenerator.Field[neighborhoodOfPoint.y, neighborhoodOfPoint.x]);

            int alpha = (int)CalcAlpha(neighborhoodOfPoint, fieldGenerator, mixingColor);

            int R = filter(alpha + mixingColor.R);
            int G = filter(alpha + mixingColor.G);
            int B = filter(alpha + mixingColor.B);

            return Color.FromArgb(R, G, B);
        }

        


        /// <summary>
        /// Нарисовать окрестности точки
        /// </summary>
        static void DrawNeighborhoodOfPoint(Vector nextPoint, List<Vector> neighborhoodOfPoint, FieldGenerator fieldGenerator)
        {
            foreach (var v in neighborhoodOfPoint)
            {
                fieldGenerator.Field[v.y, v.x] = DefinitionColorOfNeighborhoodPoint(nextPoint,
                                                                                    v,
                                                                                    fieldGenerator);
            }
        }

        #endregion


        public static void Draw(Vector nextPoint, List<Vector> neighborhoodOfPoint, 
            FieldGenerator fieldGenerator, Fractal fractal)
        {
            DrawPoint(nextPoint, fieldGenerator, fractal);
            DrawNeighborhoodOfPoint(nextPoint, neighborhoodOfPoint, fieldGenerator);
        }
    }
}
