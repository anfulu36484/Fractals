using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Fractals.DrawFractal
{
    class DeterminantOfGrowthPoints
    {

        private Fractal _fractal;
        private FieldGenerator _fieldGenerator;

        public DeterminantOfGrowthPoints(Fractal fractal, FieldGenerator fieldGenerator)
        {
            _fractal = fractal;
            _fieldGenerator = fieldGenerator;
        }

        /// <summary>
        /// Получить координаты всех ячеек в диапазоне от n-1 до n+1 кроме точки {n, n}
        /// Коордианаты x y задают точку {n, n}
        /// </summary>
        List<Vector> GetCoordinatеsAllTheCells(Vector p)
        {
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
        List<Vector> RemoveTheCoordinatesLieOutsideOfField(List<Vector> input)
        {
            return input.Where(n => !(n.y < 0
                                      || n.x < 0
                                      || n.y > _fieldGenerator.DimensionField - 1
                                      || n.x > _fieldGenerator.DimensionField - 1))
                        .ToList();
        }


        float DefineOfDensity(Color color)
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
        StateOfFractal CheckingConditionStoppingGrowth(List<Vector> coordinates)
        {
            var densityForNeigborhood = coordinates.Select(n => new
            {
                vector = n,
                density = DefineOfDensity(_fieldGenerator.Field[n.y, n.x])
            });

            double densityOfFractal = DefineOfDensity(_fractal.ColorOfFractal);

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
        List<Vector> FindTheCoordinatesWithLowesDensity(List<Vector> input)
        {
            var densityForNeigborhood = input.Select(n => new
            {
                vector = n,
                density = DefineOfDensity(_fieldGenerator.Field[n.y, n.x])
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
        Vector SelectionOfOlyOnePointOfCollection(List<Vector> input )
        {
            return input[_fieldGenerator.Rand.Next(input.Count)];
        }

        

        /// <summary>
        /// Определение точки роста
        /// </summary>
        /// <returns></returns>
        public Vector DetermineGrowthPoint(Vector initialPoint)
        {
            //Получаем координаты всех ячеек в диапазоне от n-1 до n+1 кроме точки {n, n}
            List<Vector> coordinatеsAllTheCells = GetCoordinatеsAllTheCells(initialPoint);
            //Отфильтровываем коордианты, лежашие за предеолами поля
            List<Vector> coordinatesLieOutsideOfField = RemoveTheCoordinatesLieOutsideOfField(coordinatеsAllTheCells);

            if (CheckingConditionStoppingGrowth(coordinatesLieOutsideOfField) == StateOfFractal.Live)
            {
                List<Vector> coordinatesWithLowesDensity = FindTheCoordinatesWithLowesDensity(coordinatesLieOutsideOfField);
                if (coordinatesWithLowesDensity.Count == 1)
                    return coordinatesWithLowesDensity[0];
                else
                    return SelectionOfOlyOnePointOfCollection(coordinatesWithLowesDensity);
            }
            else
            {
                return null;
            }

        }

    }
}
