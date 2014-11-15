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

        private DataManager _dataManager;

        public MainWindow()
        {
            InitializeComponent();
            _dataManager = new DataManager(this);
        }


        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            _dataManager.Start();
        }

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            _dataManager.Stop();
        }

        private void OpenWindowSettings(object sender, RoutedEventArgs e)
        {
            SettingsWindow sw  = new SettingsWindow();
            sw.Show();
        }



    }


}

