using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGenerator.Builder
{
    class MusicBuilder
    {


        /// <summary>
        /// Конвертировать абсолютные значения последовательности в относительные ( значения, лежашие в интервале от 0 до 1)
        /// </summary>
        /// <param name="sequence">Последовательность на основе которой будет строиться трек</param>
        public double[] ConvertInRelative(int[] sequence)
        {
            double max = sequence.Max();
            double min = sequence.Min();
            double[] output = new double[sequence.Length];
            for (int i = 0; i < sequence.Length; i++)
            {
                if (sequence[i] == 0)
                    output[i] = 0;
                else
                    output[i] = (sequence[i] - min)/(max-min);
            }
            return output;
        }


    }
}
