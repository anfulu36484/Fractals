using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Fractals.DrawFractal
{
    static class DeterminantOfGrowthPoints
    {


        /// <summary>
        /// Получить координаты всех ячеек в диапазоне от n-1 до n+1 кроме точки {n, n}
        /// Коордианаты x y задают точку {n, n}
        /// </summary>
        public static List<Vector> GetCoordinatеsAllTheCells(Vector p, Fractal fractal)
        {
            Fractal f = fractal;
            return new List<Vector>
            {
                new Vector(p.y - 1, p.x - 1),
                new Vector(p.y - 1, p.x),
                new Vector(p.y - 1, p.x + 1),
                new Vector(p.y, p.x + 1),
                new Vector(p.y + 1, p.x + 1),
                new Vector(p.y + 1, p.x),
                new Vector(p.y + 1, p.x - 1),
                new Vector(p.y, p.x - 1)
            };
        }


        /// <summary>
        /// Отфильтровать коордианты, лежашие за предеолами поля
        /// </summary>
        /// <returns></returns>
        public static List<Vector> RemoveTheCoordinatesLieOutsideOfField(List<Vector> input, FieldGenerator fieldGenerator)
        {
            return input.Where(n => !(n.y < 0
                                      || n.x < 0
                                      || n.y > fieldGenerator.DimensionField - 1
                                      || n.x > fieldGenerator.DimensionField - 1))
                        .ToList();
        }


        public static float DefineOfDensity(Color color)
        {
            return 1/(float)(color.R + color.G + color.B);
        }


        /// <summary>
        /// Проверка условия остановки роста
        /// Единственным условием остановки роста является присутствие ячеек 
        /// в окрестностях начальной точки с более высокой плотностью чем сама точка.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        static StateOfFractal CheckingConditionStoppingGrowth(List<Vector> coordinates, 
            FieldGenerator fieldGenerator, Fractal fractal )
        {
            var densityForNeigborhood = coordinates.Select(n => new
            {
                vector = n,
                density = DefineOfDensity(fieldGenerator.Field[n.y, n.x])
            });

            double densityOfFractal = DefineOfDensity(fractal.ColorOfFractal);

            foreach (var n in densityForNeigborhood)
            {
                if (n.density < densityOfFractal)
                    return StateOfFractal.Live;
            }
            return StateOfFractal.Dead;
        }


        /// <summary>
        /// Найти координаты с наименьшей плотностью
        /// </summary>
        /// <returns></returns>
        static List<Vector> FindTheCoordinatesWithLowesDensity(List<Vector> input, FieldGenerator fieldGenerator)
        {
            var densityForNeigborhood = input.Select(n => new
            {
                vector = n,
                density = DefineOfDensity(fieldGenerator.Field[n.y, n.x])
            });

            float minValue = densityForNeigborhood.Min(n => n.density); //Находим значение минимальной плотности ячейки из коллекции input

            List<Vector> output = new List<Vector>();
            foreach (var n in densityForNeigborhood)
            {
                if(Math.Abs(n.density - minValue) < 0.0001)
                    output.Add(n.vector);
            }
            return output;
        }


        /// <summary>
        /// Если точек с минимальной плотностью несколько, то случайным образом выберим одну.
        /// </summary>
        /// <returns></returns>
        static Vector SelectionOfOlyOnePointOfCollection(List<Vector> input, FieldGenerator fieldGenerator)
        {
            return input[fieldGenerator.Rand.Next(input.Count)];
        }

        

        /// <summary>
        /// Определение точки роста
        /// </summary>
        /// <returns></returns>
        public static Vector DetermineGrowthPoint(Vector initialPoint, FieldGenerator fieldGenerator, Fractal fractal)
        {
            //Получаем координаты всех ячеек в диапазоне от n-1 до n+1 кроме точки {n, n}
            List<Vector> coordinatеsAllTheCells = GetCoordinatеsAllTheCells(initialPoint, fractal);
            //Отфильтровываем коордианты, лежашие за предеолами поля
            List<Vector> coordinatesLieOutsideOfField = RemoveTheCoordinatesLieOutsideOfField(coordinatеsAllTheCells,fieldGenerator);

            if (CheckingConditionStoppingGrowth(coordinatesLieOutsideOfField,fieldGenerator,fractal) == StateOfFractal.Live)
            {
                List<Vector> coordinatesWithLowesDensity = FindTheCoordinatesWithLowesDensity(coordinatesLieOutsideOfField,fieldGenerator);
                if (coordinatesWithLowesDensity.Count == 1)
                    return coordinatesWithLowesDensity[0];
                else
                    return SelectionOfOlyOnePointOfCollection(coordinatesWithLowesDensity,fieldGenerator);
            }
            else
            {
                return null;
            }

        }

    }
}
