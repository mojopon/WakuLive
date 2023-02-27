using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;
using WakuLive.VM;

namespace WakuLive.Presenter
{
    public class LiveStreamChannelInformationPresenter
    {
        private IDisposable _diposables;
        private LiveStreamChannelInformationViewModel _viewModel;
        public LiveStreamChannelInformationPresenter(LiveStreamChannelInformationViewModel viewModel) 
        {
            _viewModel = viewModel;
        }

        public void AddChannelModel(ChannelModel model) 
        {
            _diposables?.Dispose();
            _diposables = model.ChannelInformationObservable
                               .Subscribe(x => _viewModel.AddChannelInformation(x));
        }

        public void DeleteChannelModel()
        { 
            _diposables?.Dispose();
            _viewModel.Dispose();
        }
    }
}
