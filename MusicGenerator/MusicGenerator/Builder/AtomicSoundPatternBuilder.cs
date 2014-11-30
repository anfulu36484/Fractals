using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicGenerator.Patterns;

namespace MusicGenerator.Builder
{
    class AtomicSoundPatternBuilder
    {

        private Range _countOfNotesRange;

        private Range _plaingTimeRange;

        private Range _distanceBetweenTheNotesRange;

        private Random _random;

        private Octave _octave;

        public AtomicSoundPatternBuilder(
            Range countOfNotesRange,
            Range plaingTimeRange,
            Range distanceBetweenTheNotesRange,
            Random random,
            Octave octave)
        {
            _countOfNotesRange = countOfNotesRange;
            _plaingTimeRange = plaingTimeRange;
            _distanceBetweenTheNotesRange = distanceBetweenTheNotesRange;
            _random = random;
            _octave = octave;
        }


        int GetCountOfNote()
        {
            return _random.Next(_countOfNotesRange.MinValue,_countOfNotesRange.MaxValue);
        }

        uint GetPlaingTime()
        {
            return (uint)_random.Next(_plaingTimeRange.MinValue, _plaingTimeRange.MaxValue);
        }



        public AtomicSoundPattern Build(byte numberOfOctave)
        {
            int countOFNote = GetCountOfNote();
            uint plaingTimeOfPattern = GetPlaingTime();

            AtomicSoundPattern atomicSoundPattern = new AtomicSoundPattern(countOFNote, plaingTimeOfPattern);


            // генерируем первую ноту
            byte numberOfNote = _octave.ChooseRandomNumberOfNoteFromOctave(numberOfOctave);

            int playingTimeOfNote = _random.Next(1, (int)plaingTimeOfPattern);



            return atomicSoundPattern;
        }
    }
}
