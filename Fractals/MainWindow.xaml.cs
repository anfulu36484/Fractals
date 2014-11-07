using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Cache;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
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

    public delegate void GetResult(System.Drawing.Color[,] field);


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private BMPGenerator _bmpGenerator;
        private Statistics _statistics;
        private FieldGenerator _fieldGenerator;

        private int _sizeOfField = 300;

        public MainWindow()
        {
            InitializeComponent();
            _bmpGenerator = new BMPGenerator();
            _fieldGenerator = new FieldGenerator(_sizeOfField);
            _statistics = new Statistics(_fieldGenerator,this);
        }


        void DrawImage(Bitmap bitmap)
        {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new System.Windows.Threading.DispatcherOperationCallback(delegate
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {

                    bitmap.Save(memoryStream, ImageFormat.Bmp);

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.DecodePixelHeight = _sizeOfField;
                    bitmapImage.DecodePixelWidth = _sizeOfField;
                    bitmapImage.StreamSource = memoryStream;

                    bitmapImage.EndInit();

                    ImageField.Source = bitmapImage;

                    return null;
                }
            }), null);
        }

        /// <summary>
        /// Вывести на экран конечный вариант картинки
        /// </summary>
        void Run1()
        {
            System.Drawing.Color[,] field = _fieldGenerator.Generate();
            _bmpGenerator.CreateBMPImage(field);

            Bitmap bitmap = _bmpGenerator.ImageBitmap;
            DrawImage(bitmap);
        }


        void GetResultHandler(System.Drawing.Color[,] field)
        {

            _bmpGenerator.CreateBMPImage(field);

            Bitmap bitmap = _bmpGenerator.ImageBitmap;
            DrawImage(bitmap);
            _statistics.ShowStatistics();
            //Thread.Sleep(100);
        }

        /// <summary>
        /// Выводить на экран картинку при кождой итерации алгоритма
        /// </summary>
        void Run2()
        {
            _fieldGenerator.Generate(GetResultHandler);
        }


        #region Сохранение всех картинок в папку

        void GetResultHandler_SaveImage(System.Drawing.Color[,] field)
        {

            _bmpGenerator.CreateBMPImage(field);

            _bmpGenerator.SaveImageWithEnumerator(@"D:\С_2013\Fractals\Data");
            
        }

        void Run3()
        {
            _fieldGenerator.Generate(GetResultHandler_SaveImage);
            BMPGenerator.CountOfImage = 0;
        }


        #endregion

        private Thread _thread;

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            _thread = new Thread(Run2);
            _thread.Start();
        }

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            _thread.Abort();
        }



    }


}

