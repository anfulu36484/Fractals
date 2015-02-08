using System.Collections.Generic;
using System.Linq;
using Fractals.Model.DrawFractal;

namespace Fractals.Model
{
    class FractalPopulation
    {

        List<Fractal> _fractals = new List<Fractal>();
        List<Fractal> _tempFractalsCollection = new List<Fractal>();

        public List<Fractal> Fractals { get { return _fractals; } } 

        private FractalModel _fractalModel;

        public FractalPopulation(FractalModel fractalModel)
        {
            _fractalModel = fractalModel;
        }


        private int _initialCountOfFractals = Settings.InitialCountOfFractals;

        

        public void GenerateInitialFractals()
        {
            _fractals = Enumerable.Range(0, _initialCountOfFractals)
                .Select(n => new Fractal(_fractalModel,
                                         _fractalModel.FieldGenerator,
                                        new Vector(_fractalModel.Rand.Next(_fractalModel.FieldGenerator.DimensionField),
                                             _fractalModel.Rand.Next(_fractalModel.FieldGenerator.DimensionField)),
                                        Settings.RandomColor.GenerateColor(_fractalModel.Rand),
                                        this))
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

        public void AddFractal(Fractal fractal)
        {
            _tempFractalsCollection.Add(fractal);
        }

        /// <summary>
        /// Добавить новые и удалить мертвые фракталы из коллекции
        /// </summary>
        public void AddAndRemoveFractalsFromCollection()
        {
            //удаление мертвых фракталов
            _fractals = _fractals.Where(fractal => fractal.StateOfFractal != StateOfFractal.Dead)
                                 .Concat(_tempFractalsCollection)
                                 .ToList();
            _tempFractalsCollection = new List<Fractal>();
        }

        public bool CheckStopCondition()
        {
            return !_fractals.All(n => n.StateOfFractal == StateOfFractal.Dead);
        }
    }
}
