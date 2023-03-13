using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Command;
using WakuLive.Configuration;
using WakuLive.Controller;
using WakuLive.Core;
using WakuLive.Core.Data;
using WakuLive.Core.Data.DataStore;
using WakuLive.Core.Data.Stub;
using WakuLive.Core.Data.Twitch.Interface;
using WakuLive.Core.Domain;
using WakuLive.Core.Domain.TextToSpeech.Interface;
using WakuLive.Interface;
using WakuLive.Presenter;
using WakuLive.VM;

namespace WakuLive
{
    public class WakuLiveServiceCollection
    {
        private ServiceCollection _services;
        private ServiceProvider _provider;

        public WakuLiveServiceCollection() { }

        public void Initialize()
        {
            _services = new ServiceCollection();
        }

        public void Build(IWakuLiveClient client, WakuLiveConfiguration config) 
        {
            AddEssentials(client, config);
            RegisterDomainServices();
            RegisterViewModels();
            RegisterPresenters();
            RegisterControllers();
            RegisterCommands();

            _provider = _services.BuildServiceProvider();

            BindCommands();
        }

        public ServiceProvider GetProvider() 
        {
            return _provider;
        }

        private void AddEssentials(IWakuLiveClient client, IWakuLiveConfiguration config) 
        {
            _services.AddSingleton(client);
            _services.AddSingleton(config);
        }

        private void RegisterDomainServices() 
        {
#if DEBUG
            RegisterDebugDomainServices();
#else
            RegisterReleaseDomainServices();
#endif
        }

        private void RegisterReleaseDomainServices()
        {
            _services.AddSingleton<ITwitchChatDataStore, TwitchChatDataStore>();
            _services.AddSingleton<ITwitchChannelDataStore, TwitchChannelDataStore>();

            _services.AddSingleton<ITwitchAuthRepository, TwitchAuthRepository>();
            _services.AddSingleton<ITwitchAuthInteractor, TwitchAuthInteractor>();
            _services.AddSingleton<ITwitchChatRepository, TwitchChatRepository>();
            _services.AddSingleton<ITwitchChatInteractor, TwitchChatInteractor>();
            _services.AddSingleton<ITwitchChannelRepository, TwitchChannelRepository>();
            _services.AddSingleton<ITwitchChannelInteractor, TwitchChannelInteractor>();

            _services.AddSingleton<ITextToSpeechByBouyomiChanService, TextToSpeechByBouyomiChanService>();
            _services.AddSingleton<ITextToSpeechInteractor, TextToSpeechInteractor>();
        }

        private void RegisterDebugDomainServices() 
        {
            _services.AddSingleton<ITwitchChatDataStore, InMemoryTwitchChatDataStore>();
            _services.AddSingleton<ITwitchChannelDataStore, InMemoryTwitchChannelDataStore>();

            _services.AddSingleton<ITwitchAuthRepository, InMemoryTwitchAuthRepository>();
            _services.AddSingleton<ITwitchAuthInteractor, TwitchAuthInteractor>();
            _services.AddSingleton<ITwitchChatRepository, TwitchChatRepository>();
            _services.AddSingleton<ITwitchChatInteractor, TwitchChatInteractor>();
            _services.AddSingleton<ITwitchChannelRepository, TwitchChannelRepository>();
            _services.AddSingleton<ITwitchChannelInteractor, TwitchChannelInteractor>();

            _services.AddSingleton<ITextToSpeechByBouyomiChanService, TextToSpeechByBouyomiChanService>();
            _services.AddSingleton<ITextToSpeechInteractor, TextToSpeechInteractor>();
        }

        private void RegisterViewModels() 
        {
            // MainWindow ViewModel Groups
            _services.AddSingleton<StatusBarViewModel>();
            _services.AddSingleton<MenuBarViewModel>();
            _services.AddSingleton<LiveStreamAddressBarViewModel>();
            _services.AddSingleton<LiveStreamChannelTabViewModel>();
            _services.AddSingleton<WakuLiveClientMainWindowViewModel>();

            // ConfigurationWindow ViewModel Groups
            _services.AddSingleton<TwitchConfigurationTabItemViewModel>();
            _services.AddSingleton<ConfigurationTabViewModel>();
            _services.AddSingleton<ConfigurationWindowViewModel>();
        }

        private void RegisterPresenters() 
        {
            _services.AddSingleton<StatusBarPresenter>();

            _services.AddSingleton<MenuBarPresenter>();
            _services.AddSingleton<ICommandBindable>(x => x.GetRequiredService<MenuBarPresenter>());

            _services.AddSingleton<TwitchConfigurationTabItemPresenter>();
            _services.AddSingleton<ICommandBindable>(x => x.GetRequiredService<TwitchConfigurationTabItemPresenter>());

            _services.AddSingleton<LiveStreamAddressBarPresenter>();
            _services.AddSingleton<ICommandBindable>(x => x.GetRequiredService<LiveStreamAddressBarPresenter>());

            _services.AddSingleton<LiveStreamChannelInformationsPresenter>();
            _services.AddSingleton<LiveStreamChatListBoxesPresenter>();
            _services.AddSingleton<LiveStreamChannelTabItemsPresenter>();
            _services.AddSingleton<LiveStreamChannelTabPresenter>();

            _services.AddSingleton<LiveStreamPresenter>();
            _services.AddSingleton<ILiveStreamPresenter>(x => x.GetRequiredService<LiveStreamPresenter>());
            _services.AddSingleton<ICommandBindable>(x => x.GetRequiredService<LiveStreamPresenter>());
        }

        private void RegisterControllers() 
        {
            _services.AddSingleton<TwitchConfigurationController>();
            _services.AddSingleton<TextToSpeechController>();
            _services.AddSingleton<StatusBarController>();
            _services.AddSingleton<LiveStreamController>();
            _services.AddSingleton<WakuLiveClientController>();
        }

        private void RegisterCommands() 
        {
            _services.AddSingleton<CommandFactory>();
            _services.AddSingleton<ICommandFactory>(x => x.GetRequiredService<CommandFactory>());
        }

        private void BindCommands() 
        {
            var services = _provider.GetServices<ICommandBindable>();
            foreach (var service in services)
            {
                service.Bind(_provider);
            }
        }
    }
}
