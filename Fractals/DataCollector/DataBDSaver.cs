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
        private BinaryFormatter binFormat;

        public DataBDSaver()
        {
            deleteTheOldBDFile();
            binFormat = new BinaryFormatter();
            _sqLiteConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3", Settings.NameOfBDFile));
            _sQLiteCommand =new SQLiteCommand(_sqLiteConnection);
            ConnectionOpen();
            CreateDataTable();
            _sQLiteCommand.CommandText = "INSERT INTO FRAMES (I, J, R, G, B) VALUES ( @I, @J, @R, @G, @B)";
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
            _sQLiteCommand.CommandText = "CREATE TABLE FRAMES(ID INTEGER PRIMARY KEY AUTOINCREMENT, I INTEGER, J INTEGER, R INTEGER, G INTEGER, B INTEGER)";
            _sQLiteCommand.ExecuteNonQuery();
        }


        void ConnectionClose()
        {
            _sqLiteConnection.Close();
        }



        private int x = 0;

        public override void GetData(Color[,] data)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    _sQLiteCommand.Parameters.Add("@I", DbType.Int16).Value = i;
                    _sQLiteCommand.Parameters.Add("@J", DbType.Int16).Value = j;
                    _sQLiteCommand.Parameters.Add("@R", DbType.Int16).Value = data[i,j].R;
                    _sQLiteCommand.Parameters.Add("@G", DbType.Int16).Value = data[i, j].G;
                    _sQLiteCommand.Parameters.Add("@B", DbType.Int16).Value = data[i, j].B;
                    _sQLiteCommand.ExecuteNonQuery();
                }
            }
            
            x++;
            if (x > 10)
                ConnectionClose();
        }
    }
}
