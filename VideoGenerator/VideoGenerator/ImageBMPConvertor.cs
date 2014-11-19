using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace VideoGenerator
{
    class ImageBMPConvertor
    {

        private static void GetWigthAndHeigth(byte[] image, out int wigth, out int heigth)
        {
            byte[] widthByteArray = new byte[4];
            byte[] heigthByteArray = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                widthByteArray[i] = image[i];
                heigthByteArray[i] = image[i + 4];
            }

            wigth = BitConverter.ToInt32(widthByteArray, 0);
            heigth = BitConverter.ToInt32(heigthByteArray, 0);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">Картинка в собственном формате</param>
        /// <returns></returns>
        public Bitmap Convert(byte[] image)
        {
            int wigth;
            int heigth;
            GetWigthAndHeigth(image, out wigth, out heigth);

            Bitmap bitmap = new Bitmap(wigth,heigth);

            int index = 8;
            for (int i = 0; i < wigth; i++)
            {
                for (int j = 0; j < heigth; j++)
                {
                    int R = image[index];
                    index++;
                    int G = image[index];
                    index++;
                    int B = image[index];
                    index++;
                    bitmap.SetPixel(i,j,Color.FromArgb(R,G,B));
                }
            }
            bitmap.Save("test.bmp");
            return bitmap;
        }

        
    }
}
