using System;

namespace MusicGenerator.Patterns
{
    class AtomicSoundPattern : SoundPatternBase
    {

        private int _countOfNotes;
        private Note[] _notes;
        private int _position;

        /// <summary>
        /// Время начала проигрывания ноты относительно начала проигрывания звукового паттерна
        /// </summary>
        private uint[] _timesOfInitialPlayingOfNotes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="countOfNotes">Количество нот, образующих паттерн</param>
        /// <param name="playingTime">Время проигрывания паттерна</param>
        public AtomicSoundPattern(int countOfNotes, uint playingTime)
            : base(playingTime)
        {
            _countOfNotes = countOfNotes;
            _notes = new Note[countOfNotes];
            _timesOfInitialPlayingOfNotes = new uint[countOfNotes];
        }

        void ValidateData(Note note, uint timeOfInitialPlayingOfNote)
        {
            if (_notes.Length > _countOfNotes)
                throw new Exception(string.Format("Нот не может быть больше {0}.",_countOfNotes));
            if(timeOfInitialPlayingOfNote+note.PlayingTime -1 >PlayingTime)
                throw new Exception("Временной интервал проигрывания ноты превышает интервал проигрывания паттерна.");
        }

        public void AddNote(Note note, uint timeOfInitialPlayingOfNote)
        {
            ValidateData(note, timeOfInitialPlayingOfNote);
            _notes[_position] = note;
            _timesOfInitialPlayingOfNotes[_position] = timeOfInitialPlayingOfNote;
            _position++;
        }
    }
}
