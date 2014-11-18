using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals.DataCollector
{
    [Serializable]
    public class DataFrame
    {
        private Color[,] _frame;

        public Color[,] Frame
        {
            get { return _frame; }
        }

        public DataFrame(Color[,] frame)
        {
            _frame = frame;
        }

    }
}
