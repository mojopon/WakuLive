using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchChannelInteractor : ITwitchChannelInteractor
    {
        private ITwitchChannelRepository _repository;
        public TwitchChannelInteractor(ITwitchChannelRepository repository) 
        {
            _repository = repository;
        }

        public ConnectTwitchChannelOutput ConnectTwitchChannel(ConnectTwitchChannelInput input)
        {
            var entity = _repository.ConnectChannel(input.ChannelName, input.AccessToken);
            var translator = new TwitchChannelEntityTranslator();
            var model = translator.Translate(entity);
            var output = new ConnectTwitchChannelOutput(model);
            return output;
        }

        public DisconnectTwitchChannelOutput DisconnectTwitchChannel(DisconnectTwitchChannelInput input)
        {
            _repository.DisconnectChannel(input.Id);
            return new DisconnectTwitchChannelOutput();
        }
    }
}
