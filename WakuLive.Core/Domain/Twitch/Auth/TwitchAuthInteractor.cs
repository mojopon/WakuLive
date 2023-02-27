using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Domain
{
    public class TwitchAuthInteractor : ITwitchAuthInteractor
    {
        private ITwitchAuthRepository _repository;

        public TwitchAuthInteractor(ITwitchAuthRepository repository) 
        {
            _repository = repository;
        }

        public GetTwitchAccessTokenOutput GetTwitchAccessToken(GetTwitchAccessTokenInput input)
        {
            var observable = _repository.GetAccessToken();
            var output = new GetTwitchAccessTokenOutput(observable);
            return output;
        }
    }
}
