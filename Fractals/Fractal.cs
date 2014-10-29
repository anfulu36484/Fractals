using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals
{
    class Fractal
    {
        private FieldGenerator _fieldGenerator;
        private Color _colorOfFractal;
        private Vector _lastPosition;
        private bool _dead;
        public bool Dead { get { return _dead; } }

        public Fractal(FieldGenerator fieldGenerator, Vector lastPosition, Color colorOfFractal)
        {
            _fieldGenerator = fieldGenerator;
            _lastPosition = lastPosition;
            _colorOfFractal = colorOfFractal;
            _dead = false;
        }

        public void GenerateInitialPoint()
        {
            _fieldGenerator.Field[_lastPosition.y, _lastPosition.x] = _colorOfFractal;
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
        List<Vector> FilterСoordinates(List<Vector> inputList)
        {
            List<Vector> list = new List<Vector>(inputList.Count);
            for (int i = 0; i < inputList.Count; i++)
            {
                //Координаты, лежащие за пределами поля отфильтровываются
                if (inputList[i].y < 0 || inputList[i].x < 0
                    || inputList[i].y > _fieldGenerator.DimensionField - 1 || inputList[i].x > _fieldGenerator.DimensionField - 1)
                    continue;

                if (_fieldGenerator.Field[inputList[i].y, inputList[i].x] == Color.White)
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

        /// <summary>
        /// Определить случайную точку роста
        /// </summary>
        Vector DetermineGrowthPoint(List<Vector> coordinates)
        {
            return coordinates[_fieldGenerator.Rand.Next(coordinates.Count - 1)];
        }

        public void GenerateNextPoint()
        {
            List<Vector> coordinatеs = GetCoordinatеsAllTheCells(_lastPosition);//Координаты всех ячеек в окрестности заданной точки
            List<Vector> filteredСoordinates = FilterСoordinates(coordinatеs);
            if (CheckingConditionStoppingGrowth(filteredСoordinates))
            {
                _dead = true;
                return;
            }

            Vector NextPoint = DetermineGrowthPoint(filteredСoordinates);
            _fieldGenerator.Field[NextPoint.y, NextPoint.x] = _colorOfFractal;
            _lastPosition = NextPoint;
        }

    }
}
