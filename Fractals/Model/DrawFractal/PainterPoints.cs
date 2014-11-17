using System.Drawing;

namespace Fractals.Model.DrawFractal
{
    class PainterOfPoints
    {

        /// <summary>
        /// Нарисовать точку
        /// </summary>
        /// <param name="nextPoint"></param>
        static void DrawPoint(Vector nextPoint, FieldGenerator fieldGenerator, Fractal fractal)
        {
            fieldGenerator.Field[nextPoint.x, nextPoint.y] = fractal.ColorOfFractal;
        }

        #region Отрисовка окрестностей точки


        //Расчет плотности точки, лежащей в окрестностях новосозданной точки (px)
        static float CalcOfDensityOfNeighborhoodPoint(Vector neighborhoodOfPoint, FieldGenerator fieldGenerator)
        {
            return DeterminantOfGrowthPoints.DefineOfDensity(fieldGenerator.Field[neighborhoodOfPoint.x, neighborhoodOfPoint.y]);
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
            return (1 / 3f) * (1 - px * neighborhoodOfPoint.k * (mixingColor.R + mixingColor.G + mixingColor.B)) / (px * neighborhoodOfPoint.k);
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
            Color mixingColor = MixingColor(fieldGenerator.Field[nextPoint.x, nextPoint.y],
                fieldGenerator.Field[neighborhoodOfPoint.x, neighborhoodOfPoint.y]);

            int alpha = (int)CalcAlpha(neighborhoodOfPoint, fieldGenerator, mixingColor);

            int R = filter(alpha + mixingColor.R);
            int G = filter(alpha + mixingColor.G);
            int B = filter(alpha + mixingColor.B);

            return Color.FromArgb(R, G, B);
        }

        


        /// <summary>
        /// Нарисовать окрестности точки
        /// </summary>
        static void DrawNeighborhoodOfPoint(Vector nextPoint, Vector[,] neighborhoodOfPoint, FieldGenerator fieldGenerator)
        {
            for (int i = 0; i < neighborhoodOfPoint.GetLength(0); i++)
            {
                for (int j = 0; j < neighborhoodOfPoint.GetLength(1); j++)
                {
                    if (neighborhoodOfPoint[i,j]!=null)
                        fieldGenerator.Field[neighborhoodOfPoint[i,j].x, neighborhoodOfPoint[i,j].y] = DefinitionColorOfNeighborhoodPoint(nextPoint,
                                                                                    neighborhoodOfPoint[i,j],
                                                                                    fieldGenerator);
                }
            }
        }

        #endregion


        public static void Draw(Vector nextPoint, Vector[,] neighborhoodOfPoint, 
            FieldGenerator fieldGenerator, Fractal fractal)
        {
            DrawPoint(nextPoint, fieldGenerator, fractal);
            DrawNeighborhoodOfPoint(nextPoint, neighborhoodOfPoint, fieldGenerator);
        }
    }
}
