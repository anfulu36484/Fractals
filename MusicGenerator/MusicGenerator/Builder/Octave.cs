using System;

namespace MusicGenerator.Builder
{
    class Octave
    {
        private Random _random;

        public Octave(Random random)
        {
            _random = random;
        }

        public byte CountOfNotesInOctave(byte numberOfOctave)
        {
            if (numberOfOctave > 10)
                throw new Exception("Количество октав не может превышать десяти");
            if (numberOfOctave == 10)
                return 8;
            return 12;
        }


        /// <summary>
        /// Выбрать случайный номер ноты из октавы
        /// </summary>
        /// <param name="numberOfOctave">Номер октавы от 0 до 10 (0 соответсвует -1 октаве, 10 соотв. 9 октаве "http://www.instructables.com/id/What-is-MIDI/")</param>
        /// <returns></returns>
        public byte ChooseRandomNumberOfNoteFromOctave(byte numberOfOctave)
        {
            if (numberOfOctave>10)
                throw new Exception("Количество октав не может превышать десяти");
            if (numberOfOctave==10)
                return (byte)_random.Next(numberOfOctave * 12, numberOfOctave * 12 + 7);
            return (byte) _random.Next(numberOfOctave*12, numberOfOctave*12 + 11);
        }
    }
}
