/*Использованные литературные источники:
 * http://www.electronics.dit.ie/staff/tscarff/Music_technology/midi/midi_note_numbers_for_octaves.htm
 * http://www.instructables.com/id/What-is-MIDI/
 * */

using System;

namespace MusicGenerator.Patterns
{
    class Note
    {
        /// <summary>
        /// Номер ноты от 0 до 127
        /// </summary>
        private byte _numberOfNote;

        /// <summary>
        /// Продолжительность звучания
        /// </summary>
        private uint _playingTime;

        /// <summary>
        /// Скорость нажатия клавиши 0 до 127 (Параметр, определяющий усиление ноты, чем он больше тем звук громче)
        /// </summary>
        private byte _velocity;


        /// <summary>
        /// Номер ноты от 0 до 127
        /// </summary>
        public byte NumberOfNote
        {
            get { return _numberOfNote; }
            private set
            {
                if (value > 127)
                    throw new Exception("Номер ноты не может быть больше 127");
                _numberOfNote = value;
            }
        }

        /// <summary>
        /// Продолжительность звучания
        /// </summary>
        public uint PlayingTime
        {
            get { return _playingTime; }
            private set { _playingTime = value; }
        }

        /// <summary>
        /// Скорость нажатия клавиши 0 до 127 (Параметр, определяющий усиление ноты, чем он больше тем звук громче)
        /// </summary>
        public byte Velocity
        {
            get { return _velocity; }
            private set
            {
                if (value > 127)
                    throw new Exception("Значение не может быть больше 127");
                _velocity = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberOfNote">Номер ноты от 0 до 127</param>
        /// <param name="playingTime">Продолжительность звучания</param>
        /// <param name="velocity">Скорость нажатия клавиши 0 до 127</param>
        public Note(byte numberOfNote, uint playingTime, byte velocity)
        {
            NumberOfNote = numberOfNote;
            PlayingTime = playingTime;
            Velocity = velocity;
        }
    }
}
