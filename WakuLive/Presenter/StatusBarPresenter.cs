using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;
using WakuLive.VM;

namespace WakuLive.Presenter
{
    public class StatusBarPresenter
    {
        private StatusBarViewModel _viewModel;
        public StatusBarPresenter(StatusBarViewModel viewModel) 
        {
            _viewModel = viewModel;
        }

        private Dictionary<string, ChannelModel> channelModelDic = new Dictionary<string, ChannelModel>();
        private Dictionary<string, IDisposable> disposableDic = new Dictionary<string, IDisposable>();
        public void AddChannelModel(string id, ChannelModel channelModel) 
        {
            if (!channelModelDic.ContainsKey(id)) 
            {
                channelModelDic.Add(id, channelModel);
                var disposable = channelModel.ChannelInformationObservable
                                             .Subscribe(x => 
                                             {
                                                 _viewModel.SetStatusText(x.Title + "に接続しました");
                                             },
                                             ex => 
                                             {
                                                 _viewModel.SetStatusText("接続に失敗しました");
                                             });
                disposableDic.Add(id, disposable);
            }
        }

        public void DeleteChannelModel(string id) 
        {
            if (channelModelDic.ContainsKey(id)) 
            {
                disposableDic[id].Dispose();
                disposableDic.Remove(id);
                var channelModel = channelModelDic[id];
                channelModelDic.Remove(id);

                _viewModel.SetStatusText("切断しました");
            }
        }

        public void OnError(string message) 
        {
            _viewModel.SetStatusText(message);
        }
    }
}
