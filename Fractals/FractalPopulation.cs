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

        public List<Fractal> Fractals { get { return _fractals; } } 

        private FieldGenerator _fieldGenerator;

        public FractalPopulation(FieldGenerator fieldGenerator)
        {
            _fieldGenerator = fieldGenerator;
        }


        private int _initialCountOfFractals = 3000;

        public void GenerateInitialFractals()
        {
            _fractals = Enumerable.Range(0, _initialCountOfFractals)
                .Select(n => new Fractal(_fieldGenerator,
                                        new Vector(_fieldGenerator.Rand.Next(_fieldGenerator.DimensionField),
                                             _fieldGenerator.Rand.Next(_fieldGenerator.DimensionField)),
                                        Color.FromArgb(_fieldGenerator.Rand.Next(255), _fieldGenerator.Rand.Next(255),
                                             _fieldGenerator.Rand.Next(255))))
                 .ToList();

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
            return !_fractals.All(fractal => fractal.Dead);
        }
    }
}
