using System;

namespace MusicGenerator.Patterns
{
    class AtomicSoundPattern 
    {
        public byte NumberOfOctave;
        public UInt16 PlayingTime;
        public NoteSpace[] NoteSpaces;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberOfOctave">Номер октавы</param>
        /// <param name="playingTime">Время проигрывания паттерна</param>
        public AtomicSoundPattern(byte numberOfOctave, UInt16 playingTime)
        {
            NumberOfOctave = numberOfOctave;
            PlayingTime = playingTime;
            NoteSpaces = new NoteSpace[NumberOfOctave];
        }

    }
}
