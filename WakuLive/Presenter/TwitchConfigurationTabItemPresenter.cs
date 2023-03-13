using Microsoft.Extensions.DependencyInjection;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Command;
using WakuLive.Configuration;
using WakuLive.Interface;
using WakuLive.VM;

namespace WakuLive.Presenter
{
    public class TwitchConfigurationTabItemPresenter : ICommandBindable
    {
        private CompositeDisposable disposables = new CompositeDisposable();
        private IWakuLiveConfiguration _configuration;
        private TwitchConfigurationTabItemViewModel _viewModel;
        public TwitchConfigurationTabItemPresenter(IWakuLiveConfiguration configuration, TwitchConfigurationTabItemViewModel viewModel) 
        {
            _configuration = configuration;
            _viewModel = viewModel;

            _configuration.Account.TwitchAccessToken
                          .Subscribe(x => _viewModel.SetTwitchButtonLinked(!string.IsNullOrEmpty(x)))
                          .AddTo(disposables);    
        }

        public void AddTwitchAccessToken(string accessToken) 
        {
            Debug.Print("TwitchConfigurationTabItemPresenter : Add Twitch AccessToken:" + accessToken);
        }

        public void Bind(ServiceProvider provider)
        {
            var commandFactory = provider.GetRequiredService<ICommandFactory>();

            var linkTwitchCommand = commandFactory.GetLinkTwitchAccountCommand();
            _viewModel.LinkTwitchCommand = _configuration.Account.TwitchAccessToken
                                                         .Select(x => string.IsNullOrEmpty(x))
                                                         .ToReactiveCommand()
                                                         .WithSubscribe(() => linkTwitchCommand.Execute(null))
                                                         .AddTo(disposables);

            var unlinkTwitchCommand = commandFactory.GetUnlinkTwitchAccountCommand();
            _viewModel.UnlinkTwitchCommand = _configuration.Account.TwitchAccessToken
                                                           .Select(x => !string.IsNullOrEmpty(x))
                                                           .ToReactiveCommand()
                                                           .WithSubscribe(() => unlinkTwitchCommand.Execute(null))
                                                           .AddTo(disposables);
        }
    }
}
