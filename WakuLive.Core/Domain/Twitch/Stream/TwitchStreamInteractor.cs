using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchStreamInteractor : ITwitchStreamInteractor
    {
        private ITwitchStreamRepository _repository;
        public TwitchStreamInteractor(ITwitchStreamRepository repository) 
        {
            _repository = repository;
        }

        public ConnectTwitchStreamOutput ConnectTwitchStream(ConnectTwitchStreamInput input)
        {
            var entity = _repository.ConnectStream(input.ChannelName, input.AccessToken);
            var translator = new TwitchStreamEntityTranslator();
            var model = translator.Translate(entity);
            var output = new ConnectTwitchStreamOutput(model);
            return output;
        }

        public DisconnectTwitchStreamOutput DisconnectTwitchStream(DisconnectTwitchStreamInput input)
        {
            _repository.DisconnectStream(input.Id);
            return new DisconnectTwitchStreamOutput();
        }
    }
}
