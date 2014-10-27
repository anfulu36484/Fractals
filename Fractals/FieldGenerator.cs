using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Fractals
{
    class FieldGenerator
    {
        private Color[,] _field;
        private int _dimensionField;
        private Random _random;

        public FieldGenerator(int dimensionField)
        {
            _dimensionField = dimensionField; 
            _random = new Random();
        }

        void GenerateVoidField()
        {
            _field = new Color[_dimensionField,_dimensionField];
            for (int i = 0; i < _dimensionField; i++)
            {
                for (int j = 0; j < _dimensionField; j++)
                {
                    _field[i, j] = Color.White;
                }
            }
        }

        void GenerateInitialPoint(Vector p)
        {
            _field[p.y, p.x] = Color.FromArgb(1,1,1);
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
        /// Получить только те координаты в окрестности точки {n, n}, которые не соответсвуют другим точкам
        /// со значением true
        /// </summary>
        List<Vector> FilterСoordinates(List<Vector> inputList )
        {
            List<Vector>  list = new List<Vector>(inputList.Count);
            for (int i = 0; i < inputList.Count; i++)
            {
                //Координаты, лежащие за пределами поля отфильтровываются
                if (inputList[i].y < 0 || inputList[i].x <0
                    || inputList[i].y > _dimensionField - 1 || inputList[i].x > _dimensionField - 1)
                    continue;

                if (_field[inputList[i].y,inputList[i].x]==Color.White)
                    list.Add(inputList[i]);
            }
            return list;
        }

        /// <summary>
        /// Проверка условия остановки роста
        /// Единственным условием останвки роста является отсутсвие свободных ячеек для роста
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        bool CheckingConditionStoppingGrowth(List<Vector> coordinates)
        {
            return coordinates.Count == 0;
        }


        class Vector
        {
            public int x, y;
            public Vector(int y, int x)
            {
                this.x = x;
                this.y = y;
            }

        }

        /// <summary>
        /// Определить случайную точку роста
        /// </summary>
        Vector DetermineGrowthPoint(List<Vector> coordinates)
        {
            return coordinates[_random.Next(coordinates.Count - 1)];
        }


        Vector GenerateNextPoint(Vector p)
        {
            List<Vector> coordinatеs = GetCoordinatеsAllTheCells(p);//Координаты всех ячеек в окрестности заданной точки
            List<Vector> filteredСoordinates = FilterСoordinates(coordinatеs);
            if (CheckingConditionStoppingGrowth(filteredСoordinates))
                return null;
            Vector NextPoint = DetermineGrowthPoint(filteredСoordinates);
            _field[NextPoint.y, NextPoint.x] = Color.FromArgb(1, 1, 1); 
            return NextPoint;
        }

        public Color[,] GenerateFractals()
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
