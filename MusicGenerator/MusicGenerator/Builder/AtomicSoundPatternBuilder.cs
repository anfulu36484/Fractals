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

        private UInt16 _playingTime;

        private Random _random;

        private Octave _octave;

        public AtomicSoundPatternBuilder(
            UInt16 playingTime,
            Random random,
            Octave octave)
        {
            _playingTime = playingTime;
            _random = random;
            _octave = octave;
        }

        int GetAbsoluteContent(byte numberOfOctave, float relativeContent)
        {
            return (int)(_playingTime*_octave.CountOfNotesInOctave(numberOfOctave)*relativeContent);
        }


        int GetAbsoluteCountOfNotes(int absoluteContent, float relativeCountOfNotes)
        {
            return (int)(absoluteContent*relativeCountOfNotes);
        }

 



        /// <param name="numberOfOctave">Номер октавы</param>
        /// <param name="relativeContent">Относительное содержание - доля использованного  пространства, выделенного под
        /// паттерн AtomicSound. Содержание, соответсвующее единице, соответвует площади пространства равного 
        /// времени звучания паттерна * число нот в октаве
        /// </param>
        /// <param name="relativeCountOfNotes">Относительное количество нот в паттерне AtomicSound</param>
        public AtomicSoundPattern Build(byte numberOfOctave, float relativeContent, float relativeCountOfNotes)
        {
            int absoluteContent = GetAbsoluteContent(numberOfOctave, relativeContent);
            int absoluteCountOfNotes = GetAbsoluteCountOfNotes(absoluteContent, relativeCountOfNotes);

            AtomicSoundPattern atomicSoundPattern = new AtomicSoundPattern(numberOfOctave, _playingTime);






            

            return atomicSoundPattern;
        }
    }
}
