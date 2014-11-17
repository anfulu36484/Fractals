namespace Fractals.Model
{
    class Vector 
    {

        public int x, y;
        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Плотность цвета k = 1/(R+G+B)
        /// </summary>
        public float k;

        public Vector(int x, int y, float k)
        {
            this.x = x;
            this.y = y;
            this.k = k;
        }

    }
}
