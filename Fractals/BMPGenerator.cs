using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
             _image= new Bitmap(field.GetLength(0),field.GetLength(1));
            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    _image.SetPixel(x, y, field[x, y]);
                }
            }
        }

    }
}
