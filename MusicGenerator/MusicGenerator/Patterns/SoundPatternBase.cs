namespace MusicGenerator.Patterns
{
    class SoundPatternBase
    {

        /// <summary>
        /// Продолжительность звучания
        /// </summary>
        private uint _playingTime;

        
        private double _density;


        /// <summary>
        /// Продолжительность звучания
        /// </summary>
        public uint PlayingTime
        {
            get { return _playingTime; }
            private set { _playingTime = value; }
        }


        /// <summary>
        /// Значение этого параметра зависит от продолжительности звучания паттерна и количества паттернов, входящих в данный паттерн
        /// </summary>
        public double Density
        {
            get { return _density; }
            private set { _density = value; }
        }

        protected SoundPatternBase(uint playingTime)
        {
            PlayingTime = playingTime;

        }

    }
}
