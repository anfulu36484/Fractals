namespace MusicGenerator.Patterns
{
    class SoundPattern :SoundPatternBase
    {
        public SoundPattern(uint playingTime) : base(playingTime) {}

        private SoundPatternBase[] _soundPatternsBase;

        public SoundPatternBase[] SoundPatternsBase
        {
            get { return _soundPatternsBase; }
        }



    }
}
