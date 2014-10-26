using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Fractals
{
    class FieldGenerator
    {
        private bool[,] _field;
        private int _dimensionField;
        private Random _random;

        public FieldGenerator(int dimensionField)
        {
            _dimensionField = dimensionField; 
            _random = new Random();
        }

        void GenerateVoidField()
        {
            _field = new bool[_dimensionField,_dimensionField];
            for (int i = 0; i < _dimensionField; i++)
            {
                for (int j = 0; j < _dimensionField; j++)
                {
                    _field[i, j] = false;
                }
            }
        }

        void GenerateInitialPoint(int x, int y)
        {
            _field[y, x] = true;
        }


        /// <summary>
        /// Получить координаты всех ячеек в диапазоне от n-1 до n+1 кроме точки {n, n}
        /// Коордианаты x y задают точку {n, n}
        /// </summary>
        int[,] GetCoordinatеsAllTheCells(int x, int y)
        {
            return new int[,]
            {
                {y - 1, x - 1},
                {y-1, x},
                {y-1, x+1},
                {y, x+1},
                {y+1,x+1},
                {y+1, x},
                {y+1,x-1},
                {y,x-1}
            };
        }

        /// <summary>
        /// Получить только те координаты в окрестности точки {n, n}, которые не соответсвуют другим точкам
        /// со значением true
        /// </summary>
        List<int[]> FilterСoordinates(int[,] coordinates)
        {
            List<int[]> list = new List<int[]>(coordinates.GetLength(0));
            for (int i = 0; i < coordinates.GetLength(0); i++)
            {
                //Координаты, лежащие за пределами поля отфильтровываются
                if (coordinates[i, 0] < 0 || coordinates[i, 1]<0
                    || coordinates[i, 0] > _dimensionField - 1 || coordinates[i, 1] > _dimensionField - 1)
                    continue;

                if (!_field[coordinates[i,0],coordinates[i,1]])
                    list.Add(new int[] { coordinates[i, 0], coordinates[i, 1] });
            }
            return list;
        }

        /// <summary>
        /// Проверка условия остановки роста
        /// Единственным условием останвки роста является отсутсвие свободных ячеек для роста
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        bool CheckingConditionStoppingGrowth(List<int[]> coordinates)
        {
            return coordinates.Count == 0;
        }

        /// <summary>
        /// Определить случайную точку роста
        /// </summary>
        int[] DetermineGrowthPoint(List<int[]> coordinates)
        {
            return coordinates[_random.Next(coordinates.Count-1)];
        }


        int[] GenerateNextPoint(int x, int y)
        {
            int[,] coordinatеs = GetCoordinatеsAllTheCells(x, y);//Координаты всех ячеек в окрестности заданной точки
            List<int[]> filteredСoordinates = FilterСoordinates(coordinatеs);
            if (CheckingConditionStoppingGrowth(filteredСoordinates))
                return null;
            int[] NextPoint = DetermineGrowthPoint(filteredСoordinates);
            _field[NextPoint[0], NextPoint[1]] = true;
            return NextPoint;
        }

        public bool[,] GenerateFractals()
        {
            //Координаты начальной точки
            int [] Point = {_dimensionField/2, _dimensionField/2};

            GenerateVoidField();
            GenerateInitialPoint(Point[0], Point[1]);
            do
            {
                Point = GenerateNextPoint(Point[0], Point[1]);

            } while (Point!=null);

            return _field;

        }

    }
}
