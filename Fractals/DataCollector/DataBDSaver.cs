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
            _sQLiteCommand.CommandText = "INSERT INTO FRAMES (FRAME) VALUES (@frame)";
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
            _sQLiteCommand.CommandText = "CREATE TABLE FRAMES(ID INTEGER PRIMARY KEY AUTOINCREMENT, FRAME BLOB)";
            _sQLiteCommand.ExecuteNonQuery();
        }


        void ConnectionClose()
        {
            _sqLiteConnection.Close();
        }




        byte[] SerializableDataFrame(DataFrame dataFrame)
        {
            MemoryStream memoryStream;
            using (memoryStream= new MemoryStream())
            {
                binFormat.Serialize(memoryStream, dataFrame);
            }
            return memoryStream.ToArray();
        }


        private int x = 0;

        public override void GetData(Color[,] data)
        {
            byte[] dataFrame = SerializableDataFrame(new DataFrame(data));
            
            _sQLiteCommand.Parameters.Add("@frame", DbType.Binary, 20).Value = dataFrame;
            _sQLiteCommand.ExecuteNonQuery();
            x++;
            if (x > 10)
                ConnectionClose();
        }
    }
}
