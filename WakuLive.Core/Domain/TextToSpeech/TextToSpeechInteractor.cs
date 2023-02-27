using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain.TextToSpeech.Interface;

namespace WakuLive.Core.Domain
{
    public class TextToSpeechInteractor : ITextToSpeechInteractor
    {
        private ITextToSpeechByBouyomiChanService _bouyomiChan;

        public TextToSpeechInteractor(ITextToSpeechByBouyomiChanService bouyomiChan) 
        {
            _bouyomiChan = bouyomiChan;
        }

        public void AddSpeech(VoiceActorType voiceActorType, string message)
        {
            switch(voiceActorType)
            {
                case VoiceActorType.BouyomiChan: 
                    {
                        _bouyomiChan.AddSpeech(message);
                        break;
                    }
                default: 
                    {
                        break;
                    }
            }
        }

        public bool IsReady(VoiceActorType voiceActorType)
        {
            switch (voiceActorType)
            {
                case VoiceActorType.BouyomiChan:
                    {
                        return _bouyomiChan.IsReady();
                    }
                default:
                    {
                        return false;
                    }
            }
        }
    }
}
