using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Dictionary<string, bool> enableSpeechDic = new Dictionary<string, bool>();
        public void StartSpeech(string id, ChatModel model) 
        {
            if (speechDisposableDic.ContainsKey(id)) 
            {
                Debug.Print("the chat id is already used.");
                return;
            }

            enableSpeechDic.Add(id, true);


            var disposable = model.ChatMessageObservable
                                  .Subscribe(x => 
                                  {
                                      if (enableSpeechDic[id])
                                      {
                                          _interactor.AddSpeech(VoiceActorType.BouyomiChan, x.Message);
                                      }
                                  });

            speechDisposableDic.Add(id, disposable);
        }

        public void ToggleSpeech(string id, bool flag)
        {
            if (string.IsNullOrEmpty(id)) { return; }

            if (enableSpeechDic.ContainsKey(id))
            {
                enableSpeechDic[id] = flag;
            }
        }

        public void StopSpeech(string id) 
        {
            if (speechDisposableDic.ContainsKey(id))
            {
                speechDisposableDic[id].Dispose();
                speechDisposableDic.Remove(id);
                enableSpeechDic.Remove(id);
            }
        }
    }
}
