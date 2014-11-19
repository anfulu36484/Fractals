//http://en.code-bude.net/2013/04/17/how-to-create-video-files-in-c-from-single-images/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.IO;
using System.Drawing;
using AForge.Video.FFMPEG;
using System.Data.SQLite;



namespace VideoGenerator
{
    class Program
    {

        static byte[] GetBytes(SQLiteDataReader reader)
        {
            const int CHUNK_SIZE = 2 * 1024;
            byte[] buffer = new byte[CHUNK_SIZE];
            long bytesRead;
            long fieldOffset = 0;
            using (MemoryStream stream = new MemoryStream())
            {
                while ((bytesRead = reader.GetBytes(0, fieldOffset, buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, (int)bytesRead);
                    fieldOffset += bytesRead;
                }
                return stream.ToArray();
            }
        }

        static void Main(string[] args)
        {



            using (var connection = new SQLiteConnection(string.Format("Data Source={0};Version=3", @"D:\С_2013\Fractals\Data\picBD.db3")))
            using (var command = new SQLiteCommand(connection))
            {
                connection.Open();


                command.CommandText = "SELECT Frame FROM FRAMES WHERE ID = 1";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        byte[] buffer = GetBytes(reader);
                        Console.Write(buffer);
                    }
                }

            }



            /*int width = 600;
            int height = 600;

            VideoFileWriter writer = new VideoFileWriter();
            

            string directory = @"D:\С_2013\Fractals\Data\";


            DirectoryInfo dir = new DirectoryInfo(directory);

            String [] bmpFiles = dir.GetFiles("*.bmp")
                .Select(n => Convert.ToInt32(n.Name.Split('.')
                .ElementAt(0)))
                .OrderBy(n => n)
                .Select(n =>string.Format(@"{0}{1}.bmp",dir.FullName,n))
                .ToArray();

            writer.Open("fractals.avi", width, height, 25, VideoCodec.MPEG4, 100000000);

            for (int i = 0; i < bmpFiles.Length; i++)
            {
                Bitmap bitmap = new Bitmap(Bitmap.FromFile(bmpFiles[i]));
                writer.WriteVideoFrame(bitmap);
            }

            writer.Close();*/
        }
    }
}
