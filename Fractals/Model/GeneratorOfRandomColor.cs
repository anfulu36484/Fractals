using System;
using System.Drawing;

namespace Fractals.Model
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
