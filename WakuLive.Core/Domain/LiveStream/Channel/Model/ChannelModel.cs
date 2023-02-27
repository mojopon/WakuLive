using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public  class ChannelModel
    {
        public string Id { get; private set; }
        public IObservable<ChannelInformationModel> ChannelInformationObservable { get; private set; }

        public ChannelModel(string id, IObservable<ChannelInformationModel> observable) 
        {
            Id = id;
            ChannelInformationObservable = observable;
        }
    }
}
