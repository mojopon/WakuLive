using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;

namespace WakuLive.Controller
{
    public class TextToSpeechController
    {
        private ITextToSpeechInteractor _interactor;
        public TextToSpeechController(ITextToSpeechInteractor interactor) 
        {
            _interactor = interactor;
        }

        private Dictionary<string, IDisposable> speechDisposableDic = new Dictionary<string, IDisposable>();
        public void StartSpeech(string id, ChatModel model) 
        {
            if (speechDisposableDic.ContainsKey(id)) 
            {
                throw new Exception();
            }

            var disposable = model.ChatMessageObservable
                                  .Subscribe(x => _interactor.AddSpeech(VoiceActorType.BouyomiChan, x.Message));

            speechDisposableDic.Add(id, disposable);
        }

        public void StopSpeech(string id) 
        {
            if (speechDisposableDic.ContainsKey(id))
            {
                speechDisposableDic[id].Dispose();
                speechDisposableDic.Remove(id);
            }
        }
    }
}
