using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Fractals
{
    class BMPGenerator
    {

        private Bitmap _image;
        public Bitmap ImageBitmap
        {
            get { return _image; }
        }
        public void CreateBMPImage(Color[,] field)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
             _image= new Bitmap(field.GetLength(0),field.GetLength(1));
            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    _image.SetPixel(x, y, field[x, y]);
                }
            }
            sw.Stop();
            Debug.WriteLine("Создание Bitmap картинки   {0} секунд",(float)sw.ElapsedMilliseconds/1000);
        }





        public void SaveImage()
        {
            _image.Save("test.bmp",ImageFormat.Bmp);
        }

        public static int CountOfImage = 0;

        public void SaveImageWithEnumerator(string directory)
        {
            _image.Save(string.Format(@"{0}\{1}.bmp",directory, CountOfImage), ImageFormat.Bmp);
            CountOfImage++;
        }
    }
}
