using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchChatInteractor : ITwitchChatInteractor
    {
        private ITwitchChatRepository _repository;

        public TwitchChatInteractor(ITwitchChatRepository repository) 
        {
            _repository = repository;
        }

        public ConnectTwitchChatOutput ConnectTwitchChat(ConnectTwitchChatInput input)
        {
            var entity = _repository.ConnectChat(input.UserName, input.ChannelName, input.AccessToken);
            var translator = new TwitchChatClientEntityTranslator();
            var model = translator.Translate(entity);
            var output = new ConnectTwitchChatOutput(model);
            return output;
        }

        public DisconnectTwitchChatOutput DisconnectTwitchChat(DisconnectTwitchChatInput input)
        {
            _repository.DisconnectChat(input.Id);

            var output = new DisconnectTwitchChatOutput();
            return output;
        }
    }
}
