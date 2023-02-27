using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchUserInteractor
    {
        private ITwitchUserRepository _repository;
        public TwitchUserInteractor(ITwitchUserRepository repository) 
        {
            _repository = repository;
        }

        public void GetChannel(string userName, string accessToken) 
        {
        }
    }
}
