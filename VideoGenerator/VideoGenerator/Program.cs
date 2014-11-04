//http://en.code-bude.net/2013/04/17/how-to-create-video-files-in-c-from-single-images/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
//using AForge.Video.FFMPEG;
using System.IO;
using System.Drawing;
using AForge.Video.FFMPEG;

namespace VideoGenerator
{
    class Program
    {


        static void Main(string[] args)
        {
            int width = 500;
            int height = 500;

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

            writer.Close();
        }
    }
}
