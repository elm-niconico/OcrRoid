using System;

namespace OcrRoid.Talks
{
    internal class TalkFactory
    {
        private TalkFactory(){}

        public static ITalk FactTalkFromSettings()
        {
            var talkType = Settings.Default.talkType;

            return talkType switch
            {
                0 => new BouyomiChan(),  
                1 => new VoiceRoid(),
                _ => throw new NotImplementedException()
            };

        }

        public static ITalk FactTalk(TalkType type)
        {
            return type switch
            {
                TalkType.BouyomiChan => new BouyomiChan(),
                TalkType.VoiceRoid => new VoiceRoid(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
