using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals
{
    class GeneratorOfRandomColor
    {
        public int MinRed;
        public int MaxRed;

        public int MinGreen;
        public int MaxGreen;

        public int MinBlue;
        public int MaxBlue;

        public Color GenerateColor(Random random)
        {
            return Color.FromArgb(random.Next(MinRed, MaxRed),
                                  random.Next(MinGreen, MaxGreen),
                                  random.Next(MinBlue, MaxBlue));
        }

    }
}
