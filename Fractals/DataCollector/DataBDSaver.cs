//На основе:
//http://stackoverflow.com/questions/625029/how-do-i-store-and-retrieve-a-blob-from-sqlite

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Fractals.DataCollector
{
    class DataBDSaver:DataDistributor
    {

        private SQLiteConnection _sqLiteConnection;
        private SQLiteCommand _sQLiteCommand;
        private ImageGenerator _imageGenerator;

        public DataBDSaver()
        {

            deleteTheOldBDFile();
            _sqLiteConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3", Settings.NameOfBDFile));
            _sQLiteCommand =new SQLiteCommand(_sqLiteConnection);
            _imageGenerator = new ImageGenerator();
            ConnectionOpen();
            CreateDataTable();
            _sQLiteCommand.CommandText = "INSERT INTO FRAMES (Frame) VALUES ( @Frame)";
        }

        void deleteTheOldBDFile()
        {
            if (File.Exists(Settings.NameOfBDFile))
            {
                File.Delete(Settings.NameOfBDFile);
            }
        }

        void ConnectionOpen()
        {
            _sqLiteConnection.Open();
        }

        void CreateDataTable()
        {
            _sQLiteCommand.CommandText = "CREATE TABLE FRAMES(ID INTEGER PRIMARY KEY AUTOINCREMENT, Frame BLOB)";
            _sQLiteCommand.ExecuteNonQuery();
        }


        void ConnectionClose()
        {
            _sqLiteConnection.Close();
        }



        private int x = 0;

        public override void GetData(Color[,] data)
        {
            _sQLiteCommand.Parameters.Add("@Frame",DbType.Binary).Value=_imageGenerator.GenerateImage(data);
            _sQLiteCommand.ExecuteNonQuery();


            x++;
            if (x > 10)
                ConnectionClose();
        }
    }
}
