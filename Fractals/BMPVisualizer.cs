using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace Fractals
{
    class BMPVisualizer
    {
        private MainWindow _mainWindow;

        public BMPVisualizer(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }


        Bitmap CreateBMPImage(Color[,] field)
        {
            Bitmap image= new Bitmap(field.GetLength(0),field.GetLength(1));
            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    image.SetPixel(x, y, field[x, y]);
                }
            }
            return image;
        }

        void DrawImage(Bitmap bitmap)
        {
            _mainWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background,
                new System.Windows.Threading.DispatcherOperationCallback(delegate
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {

                        bitmap.Save(memoryStream, ImageFormat.Bmp);

                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.DecodePixelHeight = Settings.SizeOfField;
                        bitmapImage.DecodePixelWidth = Settings.SizeOfField;
                        bitmapImage.StreamSource = memoryStream;

                        bitmapImage.EndInit();

                        _mainWindow.ImageField.Source = bitmapImage;

                        return null;
                    }
                }), null);
        }

        public void DisplayImage(Color[,] field)
        {
            Bitmap image = CreateBMPImage(field);
            DrawImage(image);
        }

    }
}
