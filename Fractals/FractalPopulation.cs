using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Fractals
{
    class FractalPopulation
    {

        List<Fractal> _fractals = new List<Fractal>(); 

        private FieldGenerator _fieldGenerator;

        public FractalPopulation(FieldGenerator fieldGenerator)
        {
            _fieldGenerator = fieldGenerator;
        }


        public void GenerateInitialFractals()
        {
            _fractals = new List<Fractal>
            {
                new Fractal(_fieldGenerator,
                            new Vector(_fieldGenerator.DimensionField/2,_fieldGenerator.DimensionField/2),
                           Color.FromArgb(1,10,250)),
                new Fractal(_fieldGenerator,
                            new Vector(_fieldGenerator.DimensionField/3,_fieldGenerator.DimensionField/4),
                            Color.FromArgb(100,10,10)),
                new Fractal(_fieldGenerator,
                            new Vector(_fieldGenerator.DimensionField/4,_fieldGenerator.DimensionField/4),
                            Color.FromArgb(10,100,10))
            };
        }

        public void GenerateInitialPoints()
        {
            _fractals.ForEach(n=>n.GenerateInitialPoint());
        }

        public void GenerateNextPoints()
        {
            _fractals.ForEach(n=>n.GenerateNextPoint());
        }

        public bool WhetherThereAreLivingFractals()
        {
            _fractals.ForEach(n=>Debug.Write(n.Dead+"   "));
            Debug.Write("\n");
            return !_fractals.All(fractal => fractal.Dead);
        }
    }
}
