using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals
{
    class Program
    {
        static void Main(string[] args)
        {
            FieldGenerator fieldGenerator = new FieldGenerator(1000);
            bool[,] field = fieldGenerator.GenerateFractals();
            BMPGenerator bmpGenerator = new BMPGenerator();
            bmpGenerator.CreateBMPImage(field);
            bmpGenerator.SaveImage();
        }
    }
}
