using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals
{
    class Vector 
    {

        public int x, y;
        public Vector(int y, int x)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Плотность цвета k = 1/(R+G+B)
        /// </summary>
        public float k;

        public Vector(int y, int x, float k)
        {
            this.x = x;
            this.y = y;
            this.k = k;
        }

    }
}
