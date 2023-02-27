using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WakuLive.Controller;
using WakuLive.Core.Data.Stub;
using WakuLive.Core.Domain;
using WakuLive.Interface;
using WakuLive.View;
using WakuLive.VM;
using WakuLive.Configuration;
using System.Diagnostics;

namespace WakuLive
{
    public class WakuLiveClient : IWakuLiveClient
    {
        private WakuLiveClientMainWindow _mainWindow;
        private ConfigurationWindow _configurationWindow;

        private WakuLiveConfiguration _configuration;

        public WakuLiveClientMainWindow GetMainWindow()
        {
            return _mainWindow;
        }

        public ConfigurationWindow GetConfigurationWindow() 
        {
            return _configurationWindow;
        }

        public WakuLiveClient(WakuLiveConfiguration config) 
        {
            _configuration = config;
            Initialize();
        }

        private void Initialize() 
        {
            var serviceCollection = new WakuLiveServiceCollection();
            serviceCollection.Initialize();
            serviceCollection.Build(this, _configuration);
            var provider = serviceCollection.GetProvider();
            CreateWindows(provider);
        }

        private void CreateWindows(ServiceProvider provider)
        {
            _mainWindow = new WakuLiveClientMainWindow();
            _mainWindow.DataContext = provider.GetRequiredService<WakuLiveClientMainWindowViewModel>();
            _mainWindow.Closing += (e, args) => { ExitApplication(); };

            _configurationWindow = new ConfigurationWindow();
            _configurationWindow.HideOnClose = true;
            _configurationWindow.DataContext = provider.GetRequiredService<ConfigurationWindowViewModel>();
        }

        public void OpenMainWindow()
        {
            _mainWindow.Show();
        }

        public void OpenConfigurationWindow()
        {
            _configurationWindow.ShowDialog();
        }

        public void ExitApplication()
        {
            _configurationWindow.HideOnClose = false;
            _configurationWindow.Close();

            if (!_mainWindow.IsClosed) _mainWindow.Close();
        }

        public void AddTwitchAccessToken(string accessToken)
        {
            _configuration.Account.TwitchAccessToken.Value = accessToken;
            WakuLiveConfiguration.Save(_configuration);
            Debug.Print("WakuLiveClient: Add TwitchAccessToken");
        }

        public void RemoveTwitchAccessToken()
        {
            _configuration.Account.TwitchAccessToken.Value = "";
            WakuLiveConfiguration.Save(_configuration);
            Debug.Print("WakuLiveClient: Remove TwitchAccessToken");
        }
    }
}
