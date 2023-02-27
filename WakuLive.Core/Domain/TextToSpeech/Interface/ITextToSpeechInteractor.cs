using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public interface ITextToSpeechInteractor
    {
        void AddSpeech(VoiceActorType voiceActorType, string message);
        bool IsReady(VoiceActorType voiceActorType);
    }
}
