/*
 * Цель: Использовать для сохранения картинки собственный формат.
 * Описание формата:
 * Первые 4 байта - высота изображения.
 * Вторые 4 байта - ширина изображения.
 * Далее идет последоваетельность байтов, определеющих цвет каждого пикселя. Данная последовательность состоит
 * из последоваетельностей трех байтов, кодирующих RGB.
 * 
 * ◻◻◻◻ ◻◻◻◻ [R][G][B][R][G][B][R][G][B][R][G][B] ...
 * 
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace Fractals.DataCollector
{
    class ImageGenerator
    {


        byte[] ConvertColorArrayToProprietaryFormat(Color[,] data)
        {
            byte[] wigth = BitConverter.GetBytes(data.GetLength(0));
            byte[] height = BitConverter.GetBytes(data.GetLength(1));
            byte[] image = new byte[wigth.Length + height.Length + data.GetLength(0) * data.GetLength(1) * 3];

            for (int i = 0; i < wigth.Length; i++)
            {
                image[i] = wigth[i];
                image[i + wigth.Length] = height[i];
            }

            int index = 8;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    image[index] = data[i, j].R;
                    index++;
                    image[index] = data[i, j].G;
                    index++;
                    image[index] = data[i, j].B;
                    index++;
                }
            }
            return image;
        }

        byte[] ZipImage(byte[] input)
        {
            byte[] output;
            using (MemoryStream stream = new MemoryStream())
            {
                using (ZipFile zipFile = new ZipFile())
                {
                    zipFile.AddEntry("image", input);
                    zipFile.Save(stream);
                }
                output = stream.ToArray();
            }
            return output;
        }

        public byte[] GenerateImage(Color[,] data)
        {
            byte[] image = ConvertColorArrayToProprietaryFormat(data);
            //Сжимаем картинку с использованием zip
            return ZipImage(image);
        }
    }
}
