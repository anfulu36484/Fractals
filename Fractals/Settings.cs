using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals
{
    static class Settings
    {
        /// <summary>
        /// Размер поля. Ширина = Высота = SizeOfField
        /// </summary>
        public static int SizeOfField = 10;

        /// <summary>
        /// Начальное количество фракталов
        /// </summary>
        public static int InitialCountOfFractals = 1;

        /// <summary>
        /// Максимальная длина фрактала, после которой происходит раздвоение фрактала
        /// </summary>
        public static int MaxCountOfMemberShip = 30;

        /// <summary>
        /// Параметр уравнения Гаусса, определяющий ширину распределения Гаусса
        /// </summary>
        public static float Sigma = 2f;

        /// <summary>
        /// Радиус окрестностей точки
        /// </summary>
        public static int R=2;



    }
}
