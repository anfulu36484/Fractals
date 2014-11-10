
/*
 * Цель: Необходимо нарисовать окрестности точки таким образом, чтобы плотность цвета при приближении к точке увеличивалась,
 *при этом окрестности точки должны представлять собой окружность с радиусом R.
 *
 *Возьмем уравнение сферы в прямогульной системе координат:
 * 
 *                  (x-xo)^2+(y-yo)^2+(z-zo)^2 = R^2
 * 
 *где xo, yo, zo - координаты центра сферы, R - ее радиус.
 *Допустим, что z - плотность цвета, тогда плотность цвета можно выразить:
 * 
 *                  z = sqrt(R^2 - x^2 - y^2)
 * 
 *Посколку плотность цвета не должна превышать плотность точки и должна лежать в диапазоне от 1/3 до 1/(255+255+255),
 *представим значение z в виде константы k, определяющей во сколько раз будет увеличена плотность окрестностей точки 
 *относительно ее начальной плотности.
 * 
 *                  к=1+доля прироста
 * 
 *Долю прироста можно выразить через отношение 
 *
 *                  z/zmax
 *                 
 *где z max - z в точке.
 * 
 * Таким образом, к можно представить в виде выражения
 * 
 *                  k = 1+ z/zmax
 * */


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals.DrawFractal
{

    static class DeterminantOfCircularNeighborhoods
    {
        /// <summary>
        /// Расчет Z c использованием уравнения сферы
        /// z = sqrt(R^2 - x^2 - y^2)
        /// </summary>
        /// <param name="R">Радиус окрестностей точки</param>
        static float CalcZ_EquationOfSphere(int x, int y)
        {
            //
            float sigma = 1f;
            
            return (float) ((1/(2*Math.PI*sigma))*Math.Exp(-(x*x + y*y)/2*sigma));
        }

        /// <summary>
        /// Расчет Z c использованем двухмерного уравнения Гаусса
        /// z = exp(-(x^2+y^2)/(2*sigma^2))/(2*Pi*sigma^2)
        /// </summary>
        /// <param name="R">Радиус окрестностей точки</param>
        static float Calc2_EquestionOfSauss(int x, int y)
        {
            return (float)((1 / (2 * Math.PI * Settings.Sigma)) * Math.Exp(-(x * x + y * y) / 2 * Settings.Sigma));
        }

        public static float[,] k_array;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="R">Радиус окрестности точки</param>
        static void Calc_K_Array(int R)
        {
            float zmax = Calc2_EquestionOfSauss(0, 0);

            k_array = new float[2*(R-1),2*(R-1)];

            for (int i = 0; i < 2*(R-1); i++)
            {
                for (int j = 0; j < 2*(R-1); j++)
                {
                    float z = Calc2_EquestionOfSauss(i - (R - 1), j - (R - 1));

                    //Если обращаемся к значению, лежащему за пределами круговых окрестностей, то
                    //константу к приравниваем к единице
                    if (Single.IsNaN(z))
                        k_array[i, j] = 1;
                    else
                        k_array[i, j] = 1 + z;
                }
            }

            Debug.WriteLine("K array");
            for (int i = 0; i < k_array.GetLength(0); i++)
            {
                for (int j = 0; j < k_array.GetLength(1); j++)
                {
                    Debug.Write(k_array[i,j]+" ");
                }
                Debug.WriteLine("");
            }
        }


        /// <summary>
        /// Условие выхода координат за пределы поля
        /// </summary>
        /// <returns></returns>
        static bool CoordinatesLieOutsideOfField(int x, int y, FieldGenerator fieldGenerator)
        {
            return x < 0
                   || y < 0
                   || x > fieldGenerator.DimensionField - 1
                   || y > fieldGenerator.DimensionField - 1;
        }

        /// <summary>
        /// Получить координаты всех ячеек в окрестности точки 
        /// </summary>
        /// <param name="p">Координаты точки</param>
        static Vector[,] GetCoordinatеsAllTheCells(Vector p, Fractal fractal, int R, FieldGenerator fieldGenerator)
        {
            Vector[,] output = new Vector[2 * (R - 1), 2 * (R - 1)];

            for (int i = 0; i < 2*(R - 1); i++)
            {
                for (int j = 0; j < 2*(R - 1); j++)
                {
                    int x = p.x + i - (R - 1);
                    int y = p.y + j - (R - 1);
                    if(CoordinatesLieOutsideOfField(x, y, fieldGenerator))//Координаты, лежашие за пределами поля отфильтровываются
                        output[i,j]=null;
                    else
                        output[i, j] = new Vector(x,y);
                }
            }




            return output;
        }

        /// <summary>
        /// Определить плотность для всех ячеек, лежащих в круговых окрестностях точки
        /// </summary>
        /// <returns></returns>
        public static Vector[,] DetermineDensityForEachCell(Vector p, Fractal fractal, int R, FieldGenerator fieldGenerator)
        {
            if (k_array == null)
                Calc_K_Array(R);
            Vector[,] output = GetCoordinatеsAllTheCells(p, fractal, R, fieldGenerator);
            for (int i = 0; i < output.GetLength(0); i++)
            {
                for (int j = 0; j < output.GetLength(1); j++)
                {
                    if (output[i, j] != null)
                    {
                        if (k_array[i, j] == 1)
                            output[i, j] = null;
                        else
                            output[i, j].k = k_array[i, j];
                    }
                }
            }
            return output;

        }


    }
}
