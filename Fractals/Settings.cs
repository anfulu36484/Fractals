using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fractals.Model;
using Fractals.Model.DrawFractal;

namespace Fractals
{
    public static class Settings
    {
        /// <summary>
        /// Размер поля. Ширина = Высота = SizeOfField
        /// </summary>
        public static int SizeOfField = 900;

        /// <summary>
        /// Начальное количество фракталов
        /// </summary>
        public static int InitialCountOfFractals = 100;

        /// <summary>
        /// Максимальная длина фрактала, после которой происходит раздвоение фрактала
        /// </summary>
        public static int MaxCountOfMemberShip = 10;

        /// <summary>
        /// Параметр уравнения Гаусса, определяющий ширину распределения Гаусса
        /// </summary>
        public static float Sigma = 0.90f;

        /// <summary>
        /// Радиус окрестностей точки
        /// </summary>
        public static int R=1;

        /// <summary>
        /// Уравнение для расчета z окрестностей точки
        /// </summary>
        public static Func<int, int, float> CalcZ = DeterminantOfCircularNeighborhoods.CalcZ_EquestionOfSauss;

        /// <summary>
        /// Генератор случайного цвета, позволяющий получить начальный цвет фрактала
        /// </summary>
        public static GeneratorOfRandomColor RandomColor = new GeneratorOfRandomColor
        {
            MinRed = 0,
            MaxRed = 0,
            MinGreen = 10,
            MaxGreen = 220,
            MinBlue = 10,
            MaxBlue = 220
        };

        /// <summary>
        /// Имя файла, в котором будут храниться результаты вычислений
        /// </summary>
        public static string NameOfBDFile = @"D:\С_2013\Fractals\Data\picBD.db3";


    }
}
