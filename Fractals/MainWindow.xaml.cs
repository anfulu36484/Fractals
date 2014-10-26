using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Cache;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fractals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        void DrawImage(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Bmp);

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.DecodePixelHeight = 300;
                bitmapImage.DecodePixelWidth = 300;
                bitmapImage.StreamSource = memoryStream;

                bitmapImage.EndInit();

                ImageField.Source = bitmapImage;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            FieldGenerator fieldGenerator = new FieldGenerator(300);
            bool[,] field = fieldGenerator.GenerateFractals();
            BMPGenerator bmpGenerator = new BMPGenerator();
            bmpGenerator.CreateBMPImage(field);
 
            Bitmap bitmap = bmpGenerator.ImageBitmap;
            DrawImage(bitmap);


        }


    }

}

