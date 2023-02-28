using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Channels.GetChannelInformation;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;
using TwitchLib.Api.Helix.Models.Users.GetUsers;
using WakuLive.Core.Domain;
using WakuLive.Core.Domain.Twitch.Utility;

namespace WakuLive.Core.Data.DataStore
{
    public class TwitchStreamDataStore : IDisposable
    {
        private readonly int _getInterval = 15; 
        private TwitchAPI _api = new TwitchAPI();
        private CompositeDisposable _disposables = new CompositeDisposable();
        public TwitchStreamDataStore()
        {
            _api.Settings.ClientId = "4wimbgsvg8axlcriswl3g7cv8120vc";
        }

        public TwitchStreamEntity ConnectStream(string id, string channelName, string accessToken) 
        {
            var subject = new Subject<TwitchStreamInformationEntity>();
            var intervalDiposable = Observable.Interval(TimeSpan.FromSeconds(_getInterval))
                                              .StartWith(0)
                                              .Select(x => GetChannelInformationAsync(channelName, accessToken, ex =>
                                              {
                                                  if (!subject.IsDisposed)
                                                  {
                                                      subject.OnError(ex);
                                                  }
                                              }).ToObservable())
                                              .Merge()
                                              .Subscribe(x =>
                                              {
                                                  subject.OnNext(x);
                                              });

            var completeOnDispose = Disposable.Create(() =>
                                              {
                                                  subject.OnCompleted();
                                              });

            _disposables.Add(intervalDiposable);
            _disposables.Add(completeOnDispose);
            _disposables.Add(subject);

            var entity = new TwitchStreamEntity(id, subject);
            return entity;
        }

        private string CreateId(string channelName) 
        {
            return TwitchEntityId.Create(channelName);
        }

        /// <summary>
        /// Channelインフォメーションを取得
        /// まずGetStreamsAsyncでStream情報を取るが、チャンネルがOfflineだと取得できないのでその場合はGetUsersからGetChannelInformationを叩いて情報を取得（こちらはオフラインでも取得できる）
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="accessToken"></param>
        /// <param name="onError"></param>
        /// <returns></returns>
        private async Task<TwitchStreamInformationEntity> GetChannelInformationAsync(string channelName, string accessToken, Action<Exception> onError)
        {
            var getStreamsResponse = await _api.Helix.Streams.GetStreamsAsync(null, 20, null, null, null, new List<string> { channelName }, accessToken);

            if (getStreamsResponse.Streams.Length > 0)
            {
                return CreateTwitchStreamInformationEntity(getStreamsResponse);
            }
            else
            {
                var getUsersResponse = await _api.Helix.Users.GetUsersAsync(null, new List<string> { channelName }, accessToken);

                if (getUsersResponse.Users.Length > 0)
                {
                    var getChannelInformationsResponse = await _api.Helix.Channels.GetChannelInformationAsync(getUsersResponse.Users[0].Id, accessToken);

                    if (getChannelInformationsResponse.Data.Length > 0)
                    {
                        return CreateTwitchStreamInformationEntity(getUsersResponse, getChannelInformationsResponse);
                    }
                    else
                    {
                        onError(new Exception("no channel information found."));
                        return null;
                    }
                }
                else
                {
                    onError(new Exception("no user found."));
                    return null;
                }
            }
        }

        // GetStreamsResponseでStream状態が取得できた際のメソッド（TwitchチャンネルがOnline・配信中）
        private TwitchStreamInformationEntity CreateTwitchStreamInformationEntity(GetStreamsResponse getStreamsResponse) 
        {
            var stream = getStreamsResponse.Streams[0];
            var data = new TwitchStreamInformationEntityData()
            {
                ViewerCount = stream.ViewerCount,
                BroadcasterName = stream.UserName,
                BroadcasterId = stream.UserId,
                Title = stream.Title,
                GameId = stream.GameId,
                GameName = stream.GameName,
                ThumbnailUrl = stream.ThumbnailUrl,
                IsStreaming = true,
            };
            return new TwitchStreamInformationEntity(data);
        }

        // GetStreamsResponseでStream状態が取れなかった際のメソッド（TwitchチャンネルがOffline）
        private TwitchStreamInformationEntity CreateTwitchStreamInformationEntity(GetUsersResponse getUsersResponse,GetChannelInformationResponse getChannelInformationResponse) 
        {
            var user = getUsersResponse.Users[0];
            var channelInformation = getChannelInformationResponse.Data[0];
            var data = new TwitchStreamInformationEntityData()
            {
                ViewerCount = (int)user.ViewCount,
                BroadcasterName = channelInformation.BroadcasterName,
                BroadcasterId = channelInformation.BroadcasterId,
                Title = channelInformation.Title,
                GameId = channelInformation.GameId,
                GameName = channelInformation.GameName,
                ThumbnailUrl = user.OfflineImageUrl,
                IsStreaming = false,
            };
            return new TwitchStreamInformationEntity(data);
        }

        public void Dispose()
        {
            Debug.Print("TwitchStreamDataStore: Disposed.");
            _disposables?.Dispose();
        }
    }
}
