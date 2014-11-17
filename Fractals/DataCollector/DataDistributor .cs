using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractals.DataCollector
{
    abstract class DataDistributor
    {
        public abstract void GetData(Color[,] data);
    }
}
