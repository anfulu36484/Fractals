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

        void GenerateInitialPoint(Vector p)
        {
            _field[p.y, p.x] = true;
        }


        /// <summary>
        /// Получить координаты всех ячеек в диапазоне от n-1 до n+1 кроме точки {n, n}
        /// Коордианаты x y задают точку {n, n}
        /// </summary>
        int[,] GetCoordinatеsAllTheCells(Vector p)
        {
            return new int[,]
            {
                {p.y - 1, p.x - 1},
                {p.y-1, p.x},
                {p.y-1, p.x+1},
                {p.y, p.x+1},
                {p.y+1,p.x+1},
                {p.y+1, p.x},
                {p.y+1,p.x-1},
                {p.y,p.x-1}
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


        class Vector
        {
            public int x, y;
            public Vector(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public Vector(int[] coordinates)
            {
                x = coordinates[1];
                y = coordinates[0];
            }
        }

        /// <summary>
        /// Определить случайную точку роста
        /// </summary>
        Vector DetermineGrowthPoint(List<int[]> coordinates)
        {
            return new Vector(coordinates[_random.Next(coordinates.Count - 1)]);
        }


        Vector GenerateNextPoint(Vector p)
        {
            int[,] coordinatеs = GetCoordinatеsAllTheCells(p);//Координаты всех ячеек в окрестности заданной точки
            List<int[]> filteredСoordinates = FilterСoordinates(coordinatеs);
            if (CheckingConditionStoppingGrowth(filteredСoordinates))
                return null;
            Vector NextPoint = DetermineGrowthPoint(filteredСoordinates);
            _field[NextPoint.y, NextPoint.x] = true;
            return NextPoint;
        }

        public bool[,] GenerateFractals()
        {
            //Координаты начальной точки
            
            Vector Point = new Vector(_dimensionField/2, _dimensionField/2);

            GenerateVoidField();
            GenerateInitialPoint(Point);
            do
            {
                Point = GenerateNextPoint(Point);

            } while (Point!=null);

            return _field;

        }

        public void GenerateFractals(GetResult getField)
        {
            //Координаты начальной точки
            Vector Point = new Vector(_dimensionField/2, _dimensionField/2);

            GenerateVoidField();
            GenerateInitialPoint(Point);
            do
            {
                Point = GenerateNextPoint(Point);
                getField(_field);

            } while (Point != null);
        }

    }


}
